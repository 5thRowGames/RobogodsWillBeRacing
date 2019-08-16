using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<MinimapControl>().Reset();
    }
}
