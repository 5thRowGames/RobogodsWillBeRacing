﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPostProcess : MonoBehaviour
{
    public Material material;
    private int index;

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_EffectAmount", CameraPostProcessManager.Instance.speed[index] / 100f);
        Graphics.Blit(source, destination, material);
    }

    public void AssignIndex(int _index)
    {
        index = _index;
    }
}