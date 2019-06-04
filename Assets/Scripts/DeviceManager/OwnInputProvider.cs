using System;
using System.Collections.Generic;
using UnityEngine;

public class OwnInputProvider : IInputService
{
    private List<IDevice> devices = new List<IDevice>();
    
    public void ConnectDevice(IDevice device)
    {
        devices.Add(device);
    }

    public void DisconnectDevice(IDevice device)
    {
        devices.Remove(device);
    }

    public IDevice GetDeviceState(Guid guid)
    {
        return devices.Find(x => x.Id == guid);
    }

    public List<IDevice> GetConnectedDevices()
    {
        return devices;
    }

    public void AssignPlayer(IPlayer player, IDevice device)
    {
        device.Owner = player;
    }

    public void RemovePlayer(IDevice device)
    {
        device.Owner = null;
    }

    public void AssignControllable(IDevice device, IControllable controllable)
    {
        device.Slaves.Add(controllable);
    }

    public void UnassignControllable(IDevice device, IControllable controllable)
    {
        device.Slaves.Remove(controllable);
    }
    
}
