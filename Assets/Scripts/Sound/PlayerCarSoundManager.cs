using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarSoundManager : MonoBehaviour, IControllable
{
    public IncontrolProvider incontrolProvider;
    public string unsharedSoundStart;
    public Rigidbody carRigidbody;

    public bool Eduardo;

    private void OnEnable()
    {
        if (Eduardo)
        {
            Core.Input.AssignControllable(incontrolProvider, this);
        }
        else
        {
            ConnectDisconnectManager.ConnectCarSoundManager += ConnectSound;
            ConnectDisconnectManager.DisconnectCarSoundManagerDelegate += DisconnectSound;
        }
    }

    private void OnDisable()
    {
        ConnectDisconnectManager.ConnectCarSoundManager -= ConnectSound;
        ConnectDisconnectManager.DisconnectCarSoundManagerDelegate -= DisconnectSound;
    }

    public void Control(IDevice device)
    {
        if (device.State.Jump.IsPressed)
            AkSoundEngine.PostEvent("Turbo_In", gameObject);

        if (device.State.Jump.IsReleased)
            AkSoundEngine.PostEvent("Turbo_Out", gameObject);

        if (device.State.RightBumper.IsPressed)
            AkSoundEngine.PostEvent("Freno_In", gameObject);

        if (device.State.RightBumper.IsReleased)
            AkSoundEngine.PostEvent("Freno_Out", gameObject);
        
        if (device.State.Horizontal.IsPressed)
            //AkSoundEngine.PostEvent("Bascula_Compresor_In", gameObject);
        
        if(device.State.Horizontal.IsReleased)
            AkSoundEngine.PostEvent("Bascula_Compresor_Out", gameObject);

        if (device.State.RightTrigger.IsPressed)
            AkSoundEngine.PostEvent(unsharedSoundStart + "_Acelerar_In", gameObject);

        if (device.State.RightTrigger.IsReleased)
            AkSoundEngine.PostEvent(unsharedSoundStart + "_Acelerar_Out", gameObject);

        AkSoundEngine.SetRTPCValue("Player_Velocidad", carRigidbody.velocity.magnitude);

    }

    public void ConnectSound()
    {
        Core.Input.AssignControllable(incontrolProvider, this);
    }
    
    public void DisconnectSound()
    {
        Core.Input.UnassignControllable(incontrolProvider,this);
    }
}
