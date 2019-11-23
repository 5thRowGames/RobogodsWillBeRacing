using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PlanoSalirPortal : MonoBehaviour
{
    public GameObject kart1;
    public GameObject kart2;
    public Transform spawnPostion1;
    public Transform spawnPostion2;
    public CompuertaPasiva compuertaPasiva;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(RunAnimation());
    }

    IEnumerator RunAnimation()
    {
        compuertaPasiva.OpenDoor();
        yield return new WaitForSeconds(0.5f);
        Instantiate(kart1, spawnPostion1.position, spawnPostion1.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(kart2, spawnPostion2.position, spawnPostion2.rotation);
        yield return new WaitForSeconds(0.5f);
        compuertaPasiva.CloseDoor();
    }
}
