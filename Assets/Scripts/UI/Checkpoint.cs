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
        //if (other.transform.parent.GetComponent<MyCarController>() != null) // Es un coche/dios
        //{
        Debug.Log(other.transform.parent.name);
            other.transform.parent.GetComponent<MinimapControl>().LastCheckpoint = index;
            other.transform.parent.GetComponent<MinimapControl>().UpdateCurrentDistance();
            LapsManager.Instance.UpdateCheckPoint(other.gameObject, index);
        //}
    }
    
    
}
