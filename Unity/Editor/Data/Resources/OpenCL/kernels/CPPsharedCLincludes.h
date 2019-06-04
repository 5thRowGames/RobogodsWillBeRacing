// This file is Shared between C++ and openCL code.

#ifndef __UNITY_CPP_AND_OPENCL_SHARED_HEADER_H__
#define __UNITY_CPP_AND_OPENCL_SHARED_HEADER_H__

// IMPORTANT --------------------------------------------------------------------------
// Bump the version below when changing OpenCL include files.
// Use current date in "YYYYMMDD0_18_3" (last digit normally zero, bump it up if you want to
// change the version more than once per day) format for the version.
// If conflicts while merging, just enter current date + '0' as the version.
//
// Details:
// The Nvidia OpenCL driver is caching compiled kernels on the harddisc, unfortunately
// it does not take include files into account. However it does hash macro sent to the
// preprocessor. So we do that in LoadOpenCLProgramInternal() to force a recompilation
// when the version number below changes.
// see https://stackoverflow.com/questions/31338520/opencl-clbuildprogram-caches-source-and-does-not-recompile-if-included-source
#define UNITY_KERNEL_INCLUDES_VERSION "201801180_18_3"

#if defined(UNITY_EDITOR)
#include "Runtime/Math/Vector2.h"
#include "Runtime/Math/Vector4.h"
#define SHARED_float3(name) float name[3]
#define SHARED_float4(name) float name[4]
#else
#define SHARED_float3(name) float3 name
#define SHARED_float4(name) float4 name
#endif // UNITY_EDITOR

#define INTERSECT_BVH_WORKGROUPSIZE 64
#define INTERSECT_BVH_MAXSTACKSIZE 48

#define LIGHTMAPPER_FALLOFF_TEXTURE_WIDTH (1024)

typedef enum OpenCLKernelAssertReason
{
    kOpenCLKernelAssertReason_None = 0,
    kOpenCLKernelAssertReason_BufferAccessedOutOfBound,
    kOpenCLKernelAssertReason_AssertionFailed,
    kOpenCLKernelAssertReason_Count
} OpenCLKernelAssertReason;

typedef enum _RadeonRaysBufferID
{
    // From OpenCLRenderBuffer
    kRRBuf_invalid = 0,
    kRRBuf_lightSamples,
    kRRBuf_outputShadowmaskFromDirectBuffer,
    kRRBuf_pathRaysBuffer_0,
    kRRBuf_pathRaysBuffer_1,
    kRRBuf_pathIntersectionsBuffer,
    kRRBuf_pathThoughputBuffer,
    kRRBuf_pathLastNormalBuffer,
    kRRBuf_directSamplesBuffer,
    kRRBuf_indirectSamplesBuffer,
    kRRBuf_activePathCountBuffer_0,
    kRRBuf_activePathCountBuffer_1,
    kRRBuf_indexRemapBuffer,
    kRRBuf_totalRayCastBuffer,
    kRRBuf_lightRaysCountBuffer,
    kRRBuf_lightRaysBuffer,
    kRRBuf_lightOcclusionBuffer,
    kRRBuf_positionsWSBuffer,
    kRRBuf_kernelDebugHelperBuffer,
    kRRBuf_kernelAssertHelperBuffer,
    kRRBuf_bufferIDToBufferSizeBuffer,
    // From OpenCLRenderLightmapBuffers
    kRRBuf_outputDirectLightingBuffer,
    kRRBuf_outputIndirectLightingBuffer,
    kRRBuf_outputDirectionalFromDirectBuffer,
    kRRBuf_outputDirectionalFromGiBuffer,
    kRRBuf_outputAoBuffer,
    kRRBuf_outputValidityBuffer,
    kRRBuf_cullingMapBuffer,
    kRRBuf_visibleTexelCountBuffer,
    kRRBuf_convergenceOutputDataBuffer,
    kRRBuf_interpNormalsWSBuffer,
    kRRBuf_planeNormalsWSBuffer,
    kRRBuf_chartIndexBuffer,
    kRRBuf_occupancyBuffer,
    // From OpenCLRenderLightProbeBuffers
    kRRBuf_outputProbeSHData,
    kRRBuf_outputProbeOcclusion,
    kRRBuf_inputLightIndices,
    kRRBuf_originalRaysBuffer,

    // From OpenCLCommonBuffer
    kRRBuf_bvhStackBuffer,
    kRRBuf_random_buffer,
    kRRBuf_goldenSample_buffer,
    kRRBuf_sobol_buffer,
    kRRBuf_distanceFalloffs_buffer,
    kRRBuf_angularFalloffLUT_buffer,
    kRRBuf_albedoTextures_buffer,
    kRRBuf_emissiveTextures_buffer,
    kRRBuf_transmissionTextures_buffer,

    // From RadeonRaysLightGrid
    kRRBuf_directLightsOffsetBuffer,
    kRRBuf_directLightsBuffer,
    kRRBuf_directLightsCountPerCellBuffer,
    kRRBuf_indirectLightsOffsetBuffer,
    kRRBuf_indirectLightsBuffer,
    kRRBuf_indirectLightDistributionOffsetBuffer,
    kRRBuf_indirectLightsDistribution,
    kRRBuf_usePowerSamplingBuffer,

    // From RadeonRaysMeshManager
    kRRBuf_instanceIdToAlbedoTextureProperties,
    kRRBuf_instanceIdToEmissiveTextureProperties,
    kRRBuf_instanceIdToTransmissionTextureProperties,
    kRRBuf_instanceIdToTransmissionTextureSTs,
    kRRBuf_geometryUV0sBuffer,
    kRRBuf_geometryUV1sBuffer,
    kRRBuf_geometryVerticesBuffer,
    kRRBuf_geometryIndicesBuffer,
    kRRBuf_instanceIdToMeshDataOffsets,
    kRRBuf_instanceIdToInvTransposedMatrices,

    // From OpenCLEnvironmentBuffers
    kRRBuf_env_mipped_cube_texels_buffer,
    kRRBuf_env_mip_offsets_buffer,

    // Dynamic argument buffers, e.g. an argument that alternates between two buffers declared above
    kRRBuf_dynarg_begin,
    kRRBuf_dynarg_countBuffer,
    kRRBuf_dynarg_directionalityBuffer,     // shared between kRRBuf_outputDirectionalFromDirectBuffer and kRRBuf_outputDirectionalFromGiBuffer
    kRRBuf_dynarg_filterWeights,
    kRRBuf_w_dynarg_dstImage,
    kRRBuf_h_dynarg_dstImage,
    kRRBuf_w_dynarg_srcImage,
    kRRBuf_h_dynarg_srcImage,
    kRRBuf_w_dynarg_dstTile,
    kRRBuf_h_dynarg_dstTile,
    kRRBuf_w_dynarg_dstTile0,
    kRRBuf_h_dynarg_dstTile0,
    kRRBuf_w_dynarg_dstTile1,
    kRRBuf_h_dynarg_dstTile1,
    kRRBuf_w_dynarg_srcTile,
    kRRBuf_h_dynarg_srcTile,
    kRRBuf_w_dynarg_srcTile0,
    kRRBuf_h_dynarg_srcTile0,
    kRRBuf_w_dynarg_srcTile1,
    kRRBuf_h_dynarg_srcTile1,
    kRRBuf_w_dynarg_srcTile2,
    kRRBuf_h_dynarg_srcTile2,
    kRRBuf_w_dynarg_srcTile3,
    kRRBuf_h_dynarg_srcTile3,

    // Count
    kRRBuf_Count
} RadeonRaysBufferID;

typedef struct _OpenCLKernelAssert
{
    int assertionValue;
    int lineNumber;
    int index;
    int bufferSize;
    RadeonRaysBufferID bufferID;
    int dibs;
    int padding0;
    int padding1;
} OpenCLKernelAssert;

typedef struct _EnvironmentLightingInputData
{
    float       aoMaxDistance;
    int         envDim;
    int         numMips;
    float       sMipOffs;
    int         lightmapWidth;
} EnvironmentLightingInputData;

typedef struct _AreaLightData
{
    float areaHeight;
    float areaWidth;
    float pad0;
    float pad1;
    SHARED_float4(Normal);
    SHARED_float4(Tangent);
    SHARED_float4(Bitangent);
} AreaLightData;

typedef struct _DiscLightData
{
    float radius;
    float pad0;
    float pad1;
    float pad2;
    SHARED_float4(Normal);
    SHARED_float4(Tangent);
} DiscLightData;

typedef struct _SpotLightData
{
    int LightFalloffIndex;
    float cosineConeAngle;
    float inverseCosineConeAngle;
    float cotanConeAngle;
} SpotLightData;

typedef struct _PointLightData
{
    int LightFalloffIndex;
    float pad0;
    float pad1;
    float pad2;
} PointLightData;

typedef union
{
    AreaLightData areaLightData;
    SpotLightData spotLightData;
    PointLightData pointLightData;
    DiscLightData discLightData;
} LightDataUnion;

typedef struct LightSample
{
    int lightIdx;
    float lightPdf;
} LightSample;

typedef enum DirectBakeMode
{
    kDirectBakeMode_None = -3,
    kDirectBakeMode_Shaded = -2,
    kDirectBakeMode_Subtractive = -1,
    kDirectBakeMode_OcclusionChannel0 = 0,
    kDirectBakeMode_OcclusionChannel1 = 1,
    kDirectBakeMode_OcclusionChannel2 = 2,
    kDirectBakeMode_OcclusionChannel3 = 3
} DirectBakeMode;

typedef struct LightBuffer
{
#if defined(UNITY_EDITOR)
    LightBuffer()
    {
        memset(this, 0, sizeof(LightBuffer));
    }

    void SetPositionAndShadowAngle(float x, float y, float z, float shadowAngle)
    {
        pos[0] = x;
        pos[1] = y;
        pos[2] = z;
        pos[3] = shadowAngle;
    }

    void SetColor(float r, float g, float b, float a)
    {
        col[0] = r;
        col[1] = g;
        col[2] = b;
        col[3] = a;
    }

    void SetDirection(float x, float y, float z, float w)
    {
        dir[0] = x;
        dir[1] = y;
        dir[2] = z;
        dir[3] = w;
    }

#endif

    SHARED_float4(pos); // .rgb is position, .w is shadow angle
    SHARED_float4(col); // .rgb is color, .w is intensity
    SHARED_float4(dir); // .xyz is direction, .w is range

    int lightType;
    DirectBakeMode directBakeMode;
    int probeOcclusionLightIndex;
    int castShadow;

    LightDataUnion dataUnion;
} LightBuffer;

typedef struct _MeshDataOffsets
{
    int vertexOffset;
    int indexOffset;
} MeshDataOffsets;

typedef struct _MaterialTextureProperties
{
    int textureOffset;
    int textureWidth;
    int textureHeight;
    int materialProperties;
} MaterialTextureProperties;

typedef enum _MaterialInstanceProperties
{
    kMaterialInstanceProperties_UseTransmission = 0,
    kMaterialInstanceProperties_WrapModeU_Clamp = 1,//Repeat is the default
    kMaterialInstanceProperties_WrapModeV_Clamp = 2,//Repeat is the default
    kMaterialInstanceProperties_FilerMode_Point = 3,//Linear is the default
    kMaterialInstanceProperties_CastShadows = 4,
    kMaterialInstanceProperties_DoubleSidedGI = 5,
    kMaterialInstanceProperties_OddNegativeScale = 6
} MaterialInstanceProperties;

inline bool GetMaterialProperty(MaterialTextureProperties matTextureProperties, MaterialInstanceProperties selectedProperty)
{
    int materialProperties = matTextureProperties.materialProperties;
    return (bool)(materialProperties & 1 << selectedProperty);
}

inline void BuildMaterialProperties(MaterialTextureProperties* matTextureProperties, MaterialInstanceProperties selectedProperty, bool value)
{
    int* materialProperties = &(matTextureProperties->materialProperties);
    int mask = 1 << selectedProperty;

    //clear the corresponding MaterialInstanceProperties slot
    (*materialProperties) &= ~(mask);

    //set it to 1 if value is true
    if (value)
    {
        (*materialProperties) |= mask;
    }
}

inline int GetTextureFetchOffset(const MaterialTextureProperties matProperty, int x, int y, bool gBufferFiltering)
{
    //Clamp
    const int clampedWidth = clamp(x, 0, matProperty.textureWidth - 1);
    const int clampedHeight = clamp(y, 0, matProperty.textureHeight - 1);

    //Repeat
    int repeatedWidth = x % matProperty.textureWidth;
    int repeatedHeight = y % matProperty.textureHeight;
    repeatedWidth = (repeatedWidth >= 0) ? repeatedWidth : matProperty.textureWidth + repeatedWidth;
    repeatedHeight = (repeatedHeight >= 0) ? repeatedHeight : matProperty.textureHeight + repeatedHeight;

    //Select based on material properties
    const int usedWidth = (gBufferFiltering || GetMaterialProperty(matProperty, kMaterialInstanceProperties_WrapModeU_Clamp)) ? clampedWidth : repeatedWidth;
    const int usedHeight = (gBufferFiltering || GetMaterialProperty(matProperty, kMaterialInstanceProperties_WrapModeV_Clamp)) ? clampedHeight : repeatedHeight;
    const int fetchOffset = matProperty.textureOffset + (matProperty.textureWidth * usedHeight) + usedWidth;
    return fetchOffset;
}

#if defined(UNITY_EDITOR)
inline Vector4f GetNearestPixelColor(
    const          Vector4f* const           tex,
    const          Vector2f                  textureUVs,
    const          MaterialTextureProperties matProperty,
    const          bool                      gBufferFiltering)
#else
inline float4 GetNearestPixelColor(
    const __global float4* const             tex,
    const          float2                    textureUVs,
    const          MaterialTextureProperties matProperty,
    const          bool                      gBufferFiltering)
#endif
{
    const int texelX = (int)(textureUVs.x * (float)matProperty.textureWidth);
    const int texelY = (int)(textureUVs.y * (float)matProperty.textureHeight);
    const int textureOffset = GetTextureFetchOffset(matProperty, texelX, texelY, gBufferFiltering);
    return tex[textureOffset];
}

#if defined(UNITY_EDITOR)
inline Vector4f GetBilinearFilteredPixelColor(
    const          Vector4f* const           tex,
    const          Vector2f                  textureUVs,
    const          MaterialTextureProperties matProperty,
    const          bool                      gBufferFiltering)
#else
inline float4 GetBilinearFilteredPixelColor(
    const __global float4* const             tex,
    const          float2                    textureUVs,
    const          MaterialTextureProperties matProperty,
    const          bool                      gBufferFiltering)
#endif
{
    //Code adapted from https://en.wikipedia.org/wiki/Bilinear_filtering
    const float u = textureUVs.x * matProperty.textureWidth - 0.5f;
    const float v = textureUVs.y * matProperty.textureHeight - 0.5f;
    const float x = floor(u);
    const float y = floor(v);
    const float u_ratio = u - x;
    const float v_ratio = v - y;
    const float u_opposite = 1.0f - u_ratio;
    const float v_opposite = 1.0f - v_ratio;

    const int iX = (int)x;
    const int iY = (int)y;

    const int X0Y0 = GetTextureFetchOffset(matProperty, iX + 0, iY + 0, gBufferFiltering);
    const int X1Y0 = GetTextureFetchOffset(matProperty, iX + 1, iY + 0, gBufferFiltering);
    const int X0Y1 = GetTextureFetchOffset(matProperty, iX + 0, iY + 1, gBufferFiltering);
    const int X1Y1 = GetTextureFetchOffset(matProperty, iX + 1, iY + 1, gBufferFiltering);

#if defined(UNITY_EDITOR)
    Vector4f result;
#else
    float4 result;
#endif
    result = (tex[X0Y0] * u_opposite + tex[X1Y0] * u_ratio) * v_opposite +
        (tex[X0Y1] * u_opposite + tex[X1Y1] * u_ratio) * v_ratio;
    return result;
}

typedef struct _ConvergenceOutputData
{
    unsigned int occupiedTexelCount;
    unsigned int visibleConvergedDirectTexelCount;
    unsigned int visibleConvergedGITexelCount;
    unsigned int visibleTexelCount;
    unsigned int convergedDirectTexelCount;
    unsigned int convergedGITexelCount;
    unsigned int totalDirectSamples;
    unsigned int totalGISamples;
    int          minDirectSamples;
    int          minGISamples;
    int          maxDirectSamples;
    int          maxGISamples;
} ConvergenceOutputData;

enum UsePowerSamplingBufferSlot
{
    UsePowerSamplingBufferSlot_PowerSampleEnabled = 0,
    UsePowerSamplingBufferSlot_LightHitCount = 1,
    UsePowerSamplingBufferSlot_LightRayCount = 2,
    UsePowerSamplingBufferSlot_BufferSize = 3
};

#if defined(UNITY_EDITOR)
#undef SHARED_float3
#undef SHARED_float4
#endif // UNITY_EDITOR

#endif // __UNITY_CPP_AND_OPENCL_SHARED_HEADER_H__
