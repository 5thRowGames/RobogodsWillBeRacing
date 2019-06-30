using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceController : MonoBehaviour, IControllable
{
    public IncontrolProvider inControl;
    public IDevice device;
    public bool playable;

    public void Control(IDevice controller)
    {
        device = controller;
    }

    private void OnEnable()
    {
        Core.Input.AssignControllable(inControl, this);
        playable = true;
    }

    private void OnDisable()
    {
        Core.Input.UnassignControllable(inControl, this);
        playable = false;
    }
}
