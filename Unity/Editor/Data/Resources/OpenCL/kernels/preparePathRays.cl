#include "commonCL.h"

__kernel void clearPathThoughputBuffer(
    /*00*/ __global float4*         const   pathThoughputBuffer
    KERNEL_VALIDATOR_BUFFERS_DEF
)
{
    int idx = get_global_id(0);
    INDEX_SAFE(pathThoughputBuffer, idx) = make_float4(1.0f, 1.0f, 1.0f, 1.0f);
}

// Used for multiple global count buffers, so can't use INDEX_SAFE. Buffer size equal to 1 checked before kernel invocation though.
__kernel void clearRayCount(
    /*00*/ __global uint*                   dynarg_countBuffer
    KERNEL_VALIDATOR_BUFFERS_DEF
)
{
    INDEX_SAFE(dynarg_countBuffer, 0) = 0;
}

__kernel void preparePathRays(
    //*** output ***
    /*00*/ __global ray*                       pathRaysBuffer_0,
    /*01*/ __global float*                     indirectSamplesBuffer,
    //*** input ***
    /*02*/ __global float4*            const   positionsWSBuffer,
    /*03*/ int                                 maxGISampleCount,
    /*04*/ int                                 numTexelsOrProbes,
    /*05*/ int                                 frame,
    /*06*/ int                                 bounce,
    /*07*/ __global uint* restrict             random_buffer,
    /*08*/ __global uint                const* sobol_buffer,
    /*09*/ __global float*                     goldenSample_buffer,
    /*10*/ int                                 numGoldenSample,
    //ray statistics
    /*11*/ __global uint*                      totalRayCastBuffer,
    /*12*/ __global uint*                      activePathCountBuffer_0,
#ifndef PROBES
    /*13*/ __global float4*            const   interpNormalsWSBuffer,
    /*14*/ __global float4*            const   planeNormalsWSBuffer,
    /*15*/ __global unsigned char*     const   cullingMapBuffer,
    /*16*/ int                                 shouldUseCullingMap,
    /*17*/ float                               pushOff,
    /*18*/ int                                 superSamplingMultiplier
#else
    /*13*/ __global float4*                    originalRaysBuffer
#endif
    KERNEL_VALIDATOR_BUFFERS_DEF
)
{
    ray r;  // prepare ray in private memory
    int idx = get_global_id(0), local_idx;

    __local uint numRayPreparedSharedMem;
    if (get_local_id(0) == 0)
        numRayPreparedSharedMem = 0;
    barrier(CLK_LOCAL_MEM_FENCE);

    bool shouldPrepareNewRay = true;

#ifndef PROBES
    if (shouldUseCullingMap && IsCulled(INDEX_SAFE(cullingMapBuffer, idx)))
        shouldPrepareNewRay = false;
#endif

#ifndef PROBES
    int ssIdx = GetSuperSampledIndex(idx, frame, superSamplingMultiplier);
#else
    int ssIdx = idx;
#endif

    float4 position = INDEX_SAFE(positionsWSBuffer, ssIdx);
    if (shouldPrepareNewRay && IsInvalidPosition(position)) // Reject texels that are invalid.
        shouldPrepareNewRay = false;

    //TODO(RadeonRays) use giSamplesSoFar instead of frame. Will not work in view prio.
    const int currentGISampleCount = INDEX_SAFE(indirectSamplesBuffer, idx);
    if (shouldPrepareNewRay && IsGIConverged(currentGISampleCount, maxGISampleCount))
        shouldPrepareNewRay = false;

    if (shouldPrepareNewRay)
    {
        const float kMaxt = 1000000.0f;

        INDEX_SAFE(indirectSamplesBuffer, idx) = currentGISampleCount + 1;

        //Random numbers
        int dimensionOffset = UNITY_SAMPLE_DIM_SURFACE_OFFSET + bounce * UNITY_SAMPLE_DIMS_PER_BOUNCE;
        uint scramble = GetScramble(idx, frame, numTexelsOrProbes, random_buffer KERNEL_VALIDATOR_BUFFERS);
        float2 sample2D = GetRandomSample2D(frame, dimensionOffset, scramble, sobol_buffer);

#ifdef PROBES
        float3 D = Sample_MapToSphere(sample2D);
        const float3 P = position.xyz;
#else
        const float3 interpNormal = INDEX_SAFE(interpNormalsWSBuffer, ssIdx).xyz;
        //Map to hemisphere directed toward normal
        float3 D = GetRandomDirectionOnHemisphere(sample2D, scramble, interpNormal, numGoldenSample, goldenSample_buffer);
        const float3 planeNormal = INDEX_SAFE(planeNormalsWSBuffer, ssIdx).xyz;
        const float3 P = position.xyz + planeNormal * pushOff;

        // if plane normal is too different from interpolated normal, the hemisphere orientation will be wrong and the sample could be under the surface.
        float dotVal = dot(D, planeNormal);
        if (dotVal <= 0.0 || isnan(dotVal))
        {
            shouldPrepareNewRay = false;
        }
        else
#endif
        {
            Ray_Init(&r, P, D, kMaxt, 0.f, 0xFFFFFFFF);
            Ray_SetIndex(&r, idx);

            local_idx = atomic_inc(&numRayPreparedSharedMem);
        }
    }

    barrier(CLK_LOCAL_MEM_FENCE);
    if (get_local_id(0) == 0)
    {
        atomic_add(GET_PTR_SAFE(totalRayCastBuffer, 0), numRayPreparedSharedMem);
        numRayPreparedSharedMem = atomic_add(GET_PTR_SAFE(activePathCountBuffer_0, 0), numRayPreparedSharedMem);
    }
    barrier(CLK_LOCAL_MEM_FENCE);

    // Write the ray out to memory
    if (shouldPrepareNewRay)
    {
#ifdef PROBES
        INDEX_SAFE(originalRaysBuffer, idx) = (float4)(r.d.x, r.d.y, r.d.z, 0);
#endif
        INDEX_SAFE(pathRaysBuffer_0, numRayPreparedSharedMem + local_idx) = r;
    }
}

__kernel void preparePathRaysFromBounce(
    //*** input ***
    /*00*/ __global ray*                       pathRaysBuffer_0,
    /*01*/ __global Intersection* const        pathIntersectionsBuffer,
    /*02*/ __global uint*              const   activePathCountBuffer_0,
    /*03*/ __global float4*            const   pathLastNormalBuffer,
    //randomization
    /*04*/ int                                 lightmapSize,
    /*05*/ int                                 frame,
    /*06*/ int                                 bounce,
    /*07*/ __global uint* restrict             random_buffer,
    /*08*/ __global uint* const                sobol_buffer,
    /*09*/ __global float*                     goldenSample_buffer,
    /*10*/ int                                 numGoldenSample,
    /*11*/ float                               pushOff,
    //*** output ***
    /*12*/ __global ray*                       pathRaysBuffer_1,
    /*13*/ __global uint*                      totalRayCastBuffer,
    /*14*/ __global uint*                      activePathCountBuffer_1,
    //*** in/output ***
    /*15*/ __global float4*            const   pathThoughputBuffer
    KERNEL_VALIDATOR_BUFFERS_DEF
)
{
    ray r;// prepare ray in private memory
    int idx = get_global_id(0), local_idx;
    int texelIdx = Ray_GetIndex(GET_PTR_SAFE(pathRaysBuffer_0, idx));

    __local uint numRayPreparedSharedMem;
    if (get_local_id(0) == 0)
        numRayPreparedSharedMem = 0;
    barrier(CLK_LOCAL_MEM_FENCE);

    bool shouldPrepareNewRay = idx < INDEX_SAFE(activePathCountBuffer_0, 0) && !Ray_IsInactive(GET_PTR_SAFE(pathRaysBuffer_0, idx));

    const bool hit = INDEX_SAFE(pathIntersectionsBuffer, idx).shapeid > 0;
    if (!hit)
        shouldPrepareNewRay = false;

    int dimensionOffset;
    uint scramble;

    const bool doRussianRoulette = (bounce >= 1) && shouldPrepareNewRay;
    if (doRussianRoulette || shouldPrepareNewRay)
    {
        dimensionOffset = UNITY_SAMPLE_DIM_SURFACE_OFFSET + bounce * UNITY_SAMPLE_DIMS_PER_BOUNCE;
        scramble = GetScramble(texelIdx, frame, lightmapSize, random_buffer KERNEL_VALIDATOR_BUFFERS);
    }

    if (doRussianRoulette)
    {
        float4 pathThroughput = INDEX_SAFE(pathThoughputBuffer, texelIdx);
        float p = max(max(pathThroughput.x, pathThroughput.y), pathThroughput.z);
        float rand = GetRandomSample1D(frame, dimensionOffset++, scramble, sobol_buffer);

        if (p < rand)
            shouldPrepareNewRay = false;
        else
            INDEX_SAFE(pathThoughputBuffer, texelIdx).xyz *= (1 / p);
    }

    if (shouldPrepareNewRay)
    {
        const float t = INDEX_SAFE(pathIntersectionsBuffer, idx).uvwt.w;
        float3 position = INDEX_SAFE(pathRaysBuffer_0,idx).o.xyz + INDEX_SAFE(pathRaysBuffer_0, idx).d.xyz * t;
        float3 normal = INDEX_SAFE(pathLastNormalBuffer, idx).xyz;

        const float kMaxt = 1000000.0f;

        //Random numbers
        float2 sample2D = GetRandomSample2D(frame, dimensionOffset, scramble, sobol_buffer);

        //Map to hemisphere directed toward normal
        float3 D = GetRandomDirectionOnHemisphere(sample2D, scramble, normal, numGoldenSample, goldenSample_buffer);

        if (any(isnan(D)))  // TODO(RadeonRays) gboisse: we're generating some NaN directions somehow, fix it!!
            shouldPrepareNewRay = false;
        else
        {
            const float3 P = position.xyz + normal * pushOff;
            Ray_Init(&r, P, D, kMaxt, 0.f, 0xFFFFFFFF);
            Ray_SetIndex(&r, texelIdx);

            local_idx = atomic_inc(&numRayPreparedSharedMem);
        }
    }

    barrier(CLK_LOCAL_MEM_FENCE);
    if (get_local_id(0) == 0)
    {
        atomic_add(GET_PTR_SAFE(totalRayCastBuffer, 0), numRayPreparedSharedMem);
        numRayPreparedSharedMem = atomic_add(GET_PTR_SAFE(activePathCountBuffer_1, 0), numRayPreparedSharedMem);
    }
    barrier(CLK_LOCAL_MEM_FENCE);

    // Write the ray out to memory
    if (shouldPrepareNewRay)
        INDEX_SAFE(pathRaysBuffer_1, numRayPreparedSharedMem + local_idx) = r;
}
