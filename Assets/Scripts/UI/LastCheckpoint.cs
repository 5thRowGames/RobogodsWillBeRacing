using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCheckpoint : Checkpoint
{
    private void OnTriggerEnter(Collider other)
    {
        MinimapControl.Instance.Reset(other.tag);
    }
}
