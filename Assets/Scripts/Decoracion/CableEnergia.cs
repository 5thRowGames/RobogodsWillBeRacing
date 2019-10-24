using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class CableEnergia : MonoBehaviour
{
    private Material material;
    public float min = -2f, max = 2f;
    
    private float speedY;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void Start()
    {
        InvokeRepeating("ChangeSpeed", 0f, Random.Range(10,30f));
    }

    private void ChangeSpeed()
    {
        speedY = Random.Range(min, max);

        if (Math.Abs(speedY) < 0.1f)
            speedY += 0.5f;
        
        material.SetVector("_Speed", new Vector4(0, speedY, 0, 0));
    }

}
