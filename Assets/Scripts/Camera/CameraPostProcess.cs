using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPostProcess : MonoBehaviour
{
    public Material material;
    public int index;

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_EffectAmount", CameraPostProcessManager.Instance.speed[index]/200f);
        Graphics.Blit(source, destination, material);
    }
}
