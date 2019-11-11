using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public bool turboBooster;

    private void OnTriggerEnter(Collider other)
    {
        var myCarController = other.GetComponent<MyCarController>();

        // Si ha pasado un coche por encima, se le activa el turbo o la desaceleración
        if(myCarController != null)
        {
            if (turboBooster)
            {
                myCarController.StartTurbo(0.2f);
            }
            else
            {
                myCarController.StartSlowDown();
            }
        }
    }
}
