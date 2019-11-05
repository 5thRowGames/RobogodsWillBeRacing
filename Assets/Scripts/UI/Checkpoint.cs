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
        MinimapControl.Instance.UpdateMinimapControl(other.tag, index);
        LapsManager.Instance.UpdateCheckPoint(other.tag, index);
    }
    
    
}
