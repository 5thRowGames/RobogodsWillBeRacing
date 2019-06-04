#include "commonCL.h"
#include "colorSpace.h"
#include "directLighting.h"
#include "emissiveLighting.h"

__constant sampler_t linear2DSampler = CLK_NORMALIZED_COORDS_TRUE | CLK_ADDRESS_CLAMP_TO_EDGE | CLK_FILTER_LINEAR;

void AccumulateLightFromBounce(float3 albedo, float3 directLightingAtHit, int idx, __global float4* outputIndirectLightingBuffer, int lightmapMode,
    __global float4* outputDirectionalFromGiBuffer, float3 direction KERNEL_VALIDATOR_BUFFERS_DEF)
{
    //Purely diffuse surface reflect the unabsorbed light evenly on the hemisphere.
    float3 energyFromHit = albedo * directLightingAtHit;
    INDEX_SAFE(outputIndirectLightingBuffer, idx).xyz += energyFromHit;

    //compute directionality from indirect
    if (lightmapMode == LIGHTMAPMODE_DIRECTIONAL)
    {
        float lum = Luminance(energyFromHit);

        INDEX_SAFE(outputDirectionalFromGiBuffer, idx).xyz += direction * lum;
        INDEX_SAFE(outputDirectionalFromGiBuffer, idx).w += lum;
    }
}

__kernel void processLightRaysFromBounce(
    //*** input ***
    //lighting
    /*00*/ __global LightBuffer*    const   indirectLightsBuffer,
    /*01*/ __global LightSample*    const   lightSamples,
    /*02*/__global uint*                    usePowerSamplingBuffer,
    /*03*/ __global float*          const   angularFalloffLUT_buffer,
    /*04*/ __global float* restrict const   distanceFalloffs_buffer,
    //ray
    /*05*/ __global ray*            const   pathRaysBuffer_0,
    /*06*/ __global Intersection*   const   pathIntersectionsBuffer,
    /*07*/ __global ray*            const   lightRaysBuffer,
    /*08*/ __global float4*         const   lightOcclusionBuffer,
    /*09*/ __global float4*         const   pathLastNormalBuffer,
    /*10*/ __global float4*         const   pathThoughputBuffer,
    /*11*/ __global uint*           const   indexRemapBuffer,
    /*12*/ __global uint*           const   lightRaysCountBuffer,
#ifdef PROBES
    /*13*/ int                              numProbes,
    /*14*/ int                              totalSampleCount,
    /*15*/ __global float4*         const   originalRaysBuffer,
    /*16*/ __global float4*                 outputProbeSHData
#else
    //directional lightmap
    /*13*/          int                     lightmapMode,
    //*** output ***
    /*14*/ __global float4*                 outputIndirectLightingBuffer,
    /*15*/ __global float4*                 outputDirectionalFromGiBuffer
#endif
    KERNEL_VALIDATOR_BUFFERS_DEF
    )
{
    int idx = get_global_id(0);

    if (idx >= INDEX_SAFE(lightRaysCountBuffer, 0) || Ray_IsInactive(GET_PTR_SAFE(lightRaysBuffer, idx)))
        return;

    const int prev_idx = INDEX_SAFE(indexRemapBuffer, idx);

    const bool  hit = INDEX_SAFE(pathIntersectionsBuffer, prev_idx).shapeid > 0;
    const float4 occlusions4 = INDEX_SAFE(lightOcclusionBuffer, idx);
    const bool  isLightOccludedFromBounce = occlusions4.w < TRANSMISSION_THRESHOLD;

    if (hit && !isLightOccludedFromBounce)
    {
        const int texelOrProbeIdx = Ray_GetIndex(GET_PTR_SAFE(lightRaysBuffer, idx));
        LightSample lightSample = INDEX_SAFE(lightSamples, texelOrProbeIdx);
        LightBuffer light = INDEX_SAFE(indirectLightsBuffer, lightSample.lightIdx);
        const float t = INDEX_SAFE(pathIntersectionsBuffer, prev_idx).uvwt.w;
        //We need to compute direct lighting on the fly
        float3 surfacePosition = INDEX_SAFE(pathRaysBuffer_0, prev_idx).o.xyz + INDEX_SAFE(pathRaysBuffer_0, prev_idx).d.xyz * t;
        float3 albedoAttenuation = INDEX_SAFE(pathThoughputBuffer, texelOrProbeIdx).xyz;

        float3 directLightingAtHit = occlusions4.xyz * ShadeLight(light, INDEX_SAFE(lightRaysBuffer, idx), surfacePosition, angularFalloffLUT_buffer, distanceFalloffs_buffer KERNEL_VALIDATOR_BUFFERS) / lightSample.lightPdf;
#ifdef PROBES
        float3 L = albedoAttenuation * directLightingAtHit;

        // The original direction from which the rays was shot from the probe position
        float4 originalRayDirection = INDEX_SAFE(originalRaysBuffer, texelOrProbeIdx);
        float weight = 4.0 / totalSampleCount;

        accumulateSH(L, originalRayDirection, weight, outputProbeSHData, texelOrProbeIdx, numProbes KERNEL_VALIDATOR_BUFFERS);
#else
        AccumulateLightFromBounce(albedoAttenuation, directLightingAtHit, texelOrProbeIdx, outputIndirectLightingBuffer, lightmapMode, outputDirectionalFromGiBuffer, -INDEX_SAFE(lightRaysBuffer, idx).d.xyz KERNEL_VALIDATOR_BUFFERS);
#endif

        // Increment light hit counter
        ++INDEX_SAFE(usePowerSamplingBuffer, UsePowerSamplingBufferSlot_LightHitCount);
    }

    // If we hit very few power sampled lights (less than 20% of the time), fall back to uniform sampling
    if (++INDEX_SAFE(usePowerSamplingBuffer, UsePowerSamplingBufferSlot_LightRayCount) > 0 && (float)INDEX_SAFE(usePowerSamplingBuffer, UsePowerSamplingBufferSlot_LightHitCount) / (float)INDEX_SAFE(usePowerSamplingBuffer, UsePowerSamplingBufferSlot_LightRayCount) < 0.2f)
        INDEX_SAFE(usePowerSamplingBuffer, UsePowerSamplingBufferSlot_PowerSampleEnabled) = 0;
}

__kernel void processEmissiveFromBounce(
    //input
    /*00*/ __global ray*            const   pathRaysBuffer_0,
    /*01*/ __global Intersection*   const   pathIntersectionsBuffer,
    /*02*/ __global MaterialTextureProperties* const instanceIdToEmissiveTextureProperties,
    /*03*/ __global float2*         const   geometryUV1sBuffer,
    /*04*/ __global float4*         const   emissiveTextures_buffer,
    /*05*/ __global MeshDataOffsets* const  instanceIdToMeshDataOffsets,
    /*06*/ __global int* const              geometryIndicesBuffer,
    /*07*/ __global float4*         const   pathThoughputBuffer,
    /*08*/ __global uint*           const   activePathCountBuffer_0,
#ifdef PROBES
    /*09*/ int                              numProbes,
    /*10*/ int                              totalSampleCount,
    /*11*/ __global float4*         const   originalRaysBuffer,
    //output
    /*12*/ __global float4*                 outputProbeSHData
#else
    /*09*/          int                     lightmapMode,
    //output
    /*10*/ __global float4*                 outputIndirectLightingBuffer,
    /*11*/ __global float4*                 outputDirectionalFromGiBuffer
#endif
    KERNEL_VALIDATOR_BUFFERS_DEF
)
{
    int idx = get_global_id(0);

    if (idx >= INDEX_SAFE(activePathCountBuffer_0, 0) || Ray_IsInactive(GET_PTR_SAFE(pathRaysBuffer_0, idx)))
        return;

    const int texelOrProbeIdx = Ray_GetIndex(GET_PTR_SAFE(pathRaysBuffer_0, idx));
    const bool  hit = INDEX_SAFE(pathIntersectionsBuffer, idx).shapeid > 0;
    if (hit)
    {
        AtlasInfo emissiveContribution = FetchMaterialTextureFromRayIntersection(idx,
            pathIntersectionsBuffer,
            instanceIdToEmissiveTextureProperties,
            instanceIdToMeshDataOffsets,
            geometryUV1sBuffer,
            geometryIndicesBuffer,
            emissiveTextures_buffer
            KERNEL_VALIDATOR_BUFFERS
        );

#ifdef PROBES
        float3 L = emissiveContribution.color.xyz * INDEX_SAFE(pathThoughputBuffer, texelOrProbeIdx).xyz;

        // the original direction from which the rays was shot from the probe position
        float4 originalRayDirection = INDEX_SAFE(originalRaysBuffer, texelOrProbeIdx);
        float weight = 4.0 / totalSampleCount;

        accumulateSH(L, originalRayDirection, weight, outputProbeSHData, texelOrProbeIdx, numProbes KERNEL_VALIDATOR_BUFFERS);
#else
        float3 output = emissiveContribution.color.xyz * INDEX_SAFE(pathThoughputBuffer, texelOrProbeIdx).xyz;

        //compute directionality from indirect
        if (lightmapMode == LIGHTMAPMODE_DIRECTIONAL)
        {
            float lum = Luminance(output);

            // TODO(RadeonRays) Directionality will be wrong with more than one bounce because
            // raysFromLastToCurrentHit is the current path direction used to accumulate directionality
            // instead of the direction of the first section of the path.
            // This is a problem in many places (probes etc. look for //compute directionality from indirect)
            INDEX_SAFE(outputDirectionalFromGiBuffer, texelOrProbeIdx).xyz += INDEX_SAFE(pathRaysBuffer_0, idx).d.xyz * lum;
            INDEX_SAFE(outputDirectionalFromGiBuffer, texelOrProbeIdx).w += lum;
        }

        // Write Result
        INDEX_SAFE(outputIndirectLightingBuffer, texelOrProbeIdx).xyz += output.xyz;
#endif
    }
}

__kernel void advanceInPathAndAdjustPathProperties(
    //input
    /*00*/ __global ray*            const   pathRaysBuffer_0,
    /*01*/ __global Intersection*   const   pathIntersectionsBuffer,
    /*02*/ __global int*            const   instanceIdToAlbedoTextureProperties,
    /*03*/ __global float4*         const   instanceIdToMeshDataOffsets,
    /*04*/ __global float2*         const   geometryUV1sBuffer,
    /*05*/ __global float2*         const   geometryIndicesBuffer,
    /*06*/ __global float4*         const   albedoTextures_buffer,
    /*07*/ __global uint*           const   activePathCountBuffer_0,
    //in/output
    /*08*/ __global float4*         const   pathThoughputBuffer
    KERNEL_VALIDATOR_BUFFERS_DEF
)
{
    int idx = get_global_id(0);

    if (idx >= INDEX_SAFE(activePathCountBuffer_0, 0) || Ray_IsInactive(GET_PTR_SAFE(pathRaysBuffer_0, idx)))
        return;

    const int texelIndex = Ray_GetIndex(GET_PTR_SAFE(pathRaysBuffer_0, idx));
    const bool  hit = INDEX_SAFE(pathIntersectionsBuffer, idx).shapeid > 0;
    if (!hit)
        return;

    AtlasInfo albedoAtHit = FetchMaterialTextureFromRayIntersection(idx,
        pathIntersectionsBuffer,
        instanceIdToAlbedoTextureProperties,
        instanceIdToMeshDataOffsets,
        geometryUV1sBuffer,
        geometryIndicesBuffer,
        albedoTextures_buffer
        KERNEL_VALIDATOR_BUFFERS);

    const float throughputAttenuation = dot(albedoAtHit.color.xyz, kAverageFactors);
    INDEX_SAFE(pathThoughputBuffer, texelIndex) *= (float4)(albedoAtHit.color.x, albedoAtHit.color.y, albedoAtHit.color.z, throughputAttenuation);
}

__kernel void getNormalFromLastBounce(
    //input
    /*00*/ __global const ray* restrict              pathRaysBuffer_0,              // rays from last to current hit
    /*01*/ __global const Intersection* restrict     pathIntersectionsBuffer,       // intersections from last to current hit
    /*02*/ __global const MeshDataOffsets* restrict  instanceIdToMeshDataOffsets,
    /*03*/ __global const Matrix4x4* restrict        instanceIdToInvTransposedMatrices,
    /*04*/ __global const Vector3f_storage* restrict geometryVerticesBuffer,
    /*05*/ __global const int* restrict              geometryIndicesBuffer,
    /*06*/ __global const uint* restrict             activePathCountBuffer,
    //output
    /*07*/ __global float4* restrict                 pathLastNormalBuffer
    KERNEL_VALIDATOR_BUFFERS_DEF
)
{
    int idx = get_global_id(0);

    if (idx >= *activePathCountBuffer)
        return;

    INDEX_SAFE(pathLastNormalBuffer, idx) = 0;

    if (Ray_IsInactive(GET_PTR_SAFE(pathRaysBuffer_0, idx)))
        return;

    const bool  hit = INDEX_SAFE(pathIntersectionsBuffer, idx).shapeid > 0;
    if (!hit)
        return;

    float3 surfaceNormal = GetNormalAtRayIntersection(idx,
        pathIntersectionsBuffer,
        instanceIdToMeshDataOffsets,
        instanceIdToInvTransposedMatrices,
        geometryVerticesBuffer,
        geometryIndicesBuffer
        KERNEL_VALIDATOR_BUFFERS);

    // store normal for various kernel to use latter
    INDEX_SAFE(pathLastNormalBuffer, idx).xyz = surfaceNormal;
    INDEX_SAFE(pathLastNormalBuffer, idx).w = 1;
}
