using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Aqui hay que meter el efecto
        if (other.CompareTag("Algo"))
        {
            
        }
    }
}
