using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanoRasSuelo : MonoBehaviour
{
    public GameObject kart1;
    public GameObject kart2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        kart1.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        kart2.SetActive(true);
    }
}
