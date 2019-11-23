using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanoHabilidades : MonoBehaviour
{
    public ParticleSystem thunder;
    public List<GameObject> karts;

    public float waitTimeThunder;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        foreach (var kart in karts)
            kart.SetActive(true);

        yield return new WaitForSeconds(1f);

        thunder.Play();
    }
}
