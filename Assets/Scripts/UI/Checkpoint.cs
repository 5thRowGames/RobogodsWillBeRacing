using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int index;

    /// <summary>
    /// Indica si el checkpoint es el que hay justo después de atravesar un portal o no. True = justo a la salida del portal
    /// </summary>
    public bool exitPortal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MyCarController>() != null)
        {
            //MinimapControl.Instance.UpdateMinimapControl(other.tag, index); // Uncomment after tests
            //LapsManager.Instance.UpdateCheckPoint(other.tag, index); // Uncomment after tests
            // ERASE_ME
            var collisionsHelper = other.GetComponent<CollisionsHelper>();
            if(collisionsHelper != null)
            {
                if (collisionsHelper.currentCheckpoint == index)
                    collisionsHelper.currentCheckpoint++;
                else collisionsHelper.currentCheckpoint = index;
                Debug.Log($"currentCheckpoint = {collisionsHelper.currentCheckpoint}");
            }
            // ERASE_ME
        }

    }
    
    
}
