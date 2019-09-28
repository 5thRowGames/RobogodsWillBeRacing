using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int index;

    //Esta variable nos dirá si es el primer checkpoint en una portal así no sumará esta distancia
    public bool firstPortal;

    private void OnTriggerEnter(Collider other)
    { 
        MinimapControl.Instance.UpdateMinimapControl(other.tag, index);
        LapsManager.Instance.UpdateCheckPoint(other.tag, index);

        if (firstPortal && other.CompareTag("Dagger"))
        {
            other.GetComponent<DaggerBehaviourAlone>().SetClosestCheckpointIndex(index);
            other.transform.position = LapsManager.Instance.checkPoints[index + 1].transform.position;
        }
    }
    
    
}
