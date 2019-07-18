using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class SettingsBase : MonoBehaviour
{

    public InControlInputModule inputModule;
    public float timeBetweenInput;
    public bool canInput;

    private void Start()
    {
        canInput = true;
    }

    public virtual void Activate()
    {
        enabled = true;
    }

    public virtual void Deactivate()
    {
        enabled = false;
    }
}
