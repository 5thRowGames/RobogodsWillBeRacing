using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int index;
    public bool lastCheckpoint = false;

    private void OnTriggerEnter(Collider other)
    { 
        MinimapControl.Instance.UpdateMinimapControl(other.tag, index);
        LapsManager.Instance.UpdateCheckPoint(other.gameObject, index);
    }
    
    
}
