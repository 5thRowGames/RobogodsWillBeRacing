using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.GetMask("Portal"))
        {
            //AkSoundEngine.PostEvent("Impactos_In", gameObject);
        }
    }
}
