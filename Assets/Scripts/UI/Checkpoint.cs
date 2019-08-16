using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int lastCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MinimapControl>().LastCheckpoint = lastCheckpoint;
        other.GetComponent<MinimapControl>().UpdateCurrentDistance();
    }
    
    
}
