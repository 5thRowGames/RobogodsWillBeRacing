using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int Index { set; get; }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<MyCarController>() != null) // Es un coche/dios
        {
            Debug.Log($"Checkpoint {Index} passed!");
            LapsManager.instance.UpdateCheckPoint(other.gameObject, Index);
        }
    }
}
