#ifndef EMISSIVE_LIGHTING_H
#define EMISSIVE_LIGHTING_H

typedef struct _atlasInfo
{
    float4 color;
    float2 textureUVs;
} AtlasInfo;

AtlasInfo FetchMaterialTextureFromRayIntersection(
    int              const   rayIndex,
    __global Intersection*    const   pathIntersectionsBuffer,
    __global MaterialTextureProperties* const instanceIdToEmissiveTextureProperties,
    __global MeshDataOffsets* const   instanceIdToMeshDataOffsets,
    __global float2*          const   geometryUV1sBuffer,
    __global int*             const   geometryIndicesBuffer,
    __global float4*          const   emissiveTextures_buffer
    KERNEL_VALIDATOR_BUFFERS_DEF
)
{
    const int   instanceId = GetInstanceIdFromIntersection(GET_PTR_SAFE(pathIntersectionsBuffer, rayIndex));
    const MaterialTextureProperties matProperty = INDEX_SAFE(instanceIdToEmissiveTextureProperties, instanceId);

    float2 textureUVs = GetUVsAtRayIntersection(rayIndex,
        pathIntersectionsBuffer,
        instanceIdToMeshDataOffsets,
        geometryUV1sBuffer,
        geometryIndicesBuffer
        KERNEL_VALIDATOR_BUFFERS);

    AtlasInfo atlasInfo;
    atlasInfo.color = FetchTextureFromMaterialAndUVs(emissiveTextures_buffer, textureUVs, matProperty, true);
    atlasInfo.textureUVs = textureUVs;

    return atlasInfo;
}

#endif // EMISSIVE_LIGHTING_H
