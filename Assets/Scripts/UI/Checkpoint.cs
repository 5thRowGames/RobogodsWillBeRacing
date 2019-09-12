using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int Index { set; get; }
    public bool lastCheckpoint = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MyCarController>() != null) // Es un coche/dios
        {
            other.GetComponent<MinimapControl>().LastCheckpoint = Index;
            other.GetComponent<MinimapControl>().UpdateCurrentDistance();
            LapsManager.Instance.UpdateCheckPoint(other.gameObject, Index);
        }
    }
    
    
}
