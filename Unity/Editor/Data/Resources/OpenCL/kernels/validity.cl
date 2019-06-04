#include "commonCL.h"

__kernel void lightmapValidity(
    //input
    /*00*/ __global ray*            const   pathRaysBuffer_0,
    /*01*/ __global float4*         const   pathLastNormalBuffer,
    /*02*/ __global uint*           const   activePathCountBuffer_0,
    //output
    /*03*/ __global float*                  outputValidityBuffer
    KERNEL_VALIDATOR_BUFFERS_DEF
)
{
    int idx = get_global_id(0);

    if (idx >= INDEX_SAFE(activePathCountBuffer_0, 0))
        return;

    // .w is 0 if the ray hasn't hit anything, see getNormalFromLastBounce()
    if (INDEX_SAFE(pathLastNormalBuffer, idx).w == 1)
    {
        float3 surfaceNormal = INDEX_SAFE(pathLastNormalBuffer, idx).xyz;
        const bool frontFacing = dot(surfaceNormal, INDEX_SAFE(pathRaysBuffer_0, idx).d.xyz) <= 0;
        if (!frontFacing)
        {
            const int texelIdx = Ray_GetIndex(GET_PTR_SAFE(pathRaysBuffer_0, idx));
            INDEX_SAFE(outputValidityBuffer, texelIdx) += 1.0f;
        }
    }
}
