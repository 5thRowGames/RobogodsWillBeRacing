using System;
using UnityEngine;

public class RotateGlowShield : MonoBehaviour
{
    public Renderer meshRenderer;
    public float min;
    public float max;
    public float smoothTransition;
    
    private float current;
    private Material material;

    private void Awake()
    {
        material = meshRenderer.material;
        current = min;
        max *= 2;
    }

    private void Update()
    {
        current += smoothTransition;
        material.SetFloat("_GlowPositionController",current);

        if (current > max)
            current = min;
    }
}
