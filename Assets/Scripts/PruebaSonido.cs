using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaSonido : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            StartCoroutine(llamarSonido());
        }
    }

    IEnumerator llamarSonido()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Me espero 5 segundos y llamo a Portal_Out");
        AkSoundEngine.PostEvent("Portal_Out", gameObject);
    }
}
