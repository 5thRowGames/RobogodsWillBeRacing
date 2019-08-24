using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramToNormal : MonoBehaviour
{
    public Renderer meshRenderer;
    private Material material;

    public float transformationTime;
    public float min;
    public float max;

    private float progressionPerTick;
    private float progression;

    private void Start()
    {
        material = meshRenderer.material;
        Debug.Log(material.name);
        progression = max;
        progressionPerTick = (max - min) / transformationTime;
    }

    public void TransformIntoHologram()
    {
        StopAllCoroutines();
        StartCoroutine(IncreaseMaterial());
    }

    IEnumerator IncreaseMaterial()
    {
        while (progression < max)
        {
            progression += progressionPerTick * Time.deltaTime;
            material.SetFloat("_LerpController",progression);
            yield return null;
        }
    }

    public void TransformIntoNormal()
    {
        StopAllCoroutines();
        StartCoroutine(DecreaseMaterial());

    }
    
    IEnumerator DecreaseMaterial()
    {
        while (progression > min)
        {
            progression -= progressionPerTick * Time.deltaTime;
            material.SetFloat("_LerpController",progression);
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
