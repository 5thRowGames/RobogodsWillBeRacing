using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidroCannonBehaviour : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Preguntar si lo metemos con layers para que en las físicas quitemos su colisión con otras cosas
        if (other.CompareTag("Anubis"))
        {
            //Hacer lo que sea para la poseidon skill
        }
    }
}
