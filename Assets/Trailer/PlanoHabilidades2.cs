using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlanoHabilidades2 : MonoBehaviour
{
    public GameObject shield;
    public ParticleSystem rayo1;
    public ParticleSystem rayo2;
    
    public GameObject kart1;
    public GameObject kart2;

    public ParticleSystem nube1;
    public ParticleSystem nube2;

    public float waitTimeNube;
    public float waitTimeThunder;
    public float waitTimeImpact;

    public ParticleSystem impact1;
    public ParticleSystem impact2;

    public GameObject volteretas1;
    public GameObject voleteras2;
    
    public GameObject volteretas11;
    public GameObject voleteras22;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(nameof(StartAnimation));

    }

    IEnumerator StartAnimation()
    {
        kart1.SetActive(true);
        kart2.SetActive(true);
        
        yield return new WaitForSeconds(waitTimeNube);
        
        nube1.Play();
        nube2.Play();

        yield return new WaitForSeconds(waitTimeThunder);
        
        rayo1.Play();
        rayo2.Play();

        yield return new WaitForSeconds(waitTimeImpact);
        
        impact1.Play();
        impact2.Play();

        kart1.GetComponent<Kartmovement>().speed = 25;
        kart2.GetComponent<Kartmovement>().speed = 25;
    }
}
