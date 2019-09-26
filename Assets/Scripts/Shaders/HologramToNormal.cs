using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramToNormal : MonoBehaviour
{
    [Header("0 Anubis - 1 Poseidon - 2 Kali - 3 Thor")]
    public int index;
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
        progression = max;
        progressionPerTick = (max - min) / transformationTime;
    }

    public void TransformIntoHologram()
    {
        StopAllCoroutines();
        SoundManager.Instance.StopFxHologram(index);
        SoundManager.Instance.PlayFxHologram(SoundManager.Fx.UI_Conversion_In,index);
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
        SoundManager.Instance.StopFxHologram(index);
    }

    public void TransformIntoNormal()
    {
        StopAllCoroutines();
        SoundManager.Instance.StopFxHologram(index);
        SoundManager.Instance.PlayFxHologram(SoundManager.Fx.UI_Conversion_In,index);
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
        SoundManager.Instance.StopFxHologram(index);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
