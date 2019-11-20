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
                Debug.Log("Turbo!");
                myCarController.StartTurbo(0.2f);
                //myCarController.SimpleTurbo(0.2f);
            }
            else
            {
                Debug.Log("Slowdown!");
                myCarController.StartSlowDown();
                //myCarController.SimpleSlowDown();
            }
        }
    }
}
