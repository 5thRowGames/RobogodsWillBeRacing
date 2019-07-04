using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPruebas : MonoBehaviour
{
    public Material material;
    public static float blurAmount = 0;

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_EffectAmount", blurAmount);
        Graphics.Blit(source, destination, material);
    }
}
