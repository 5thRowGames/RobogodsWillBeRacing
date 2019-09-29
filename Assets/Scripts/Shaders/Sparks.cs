using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void DecreaseParticleSizeCoroutine()
    {
        StartCoroutine(DecreaseParticleSize());
    }

    IEnumerator DecreaseParticleSize()
    {
        yield return new WaitForSeconds(0f);
    }
}
