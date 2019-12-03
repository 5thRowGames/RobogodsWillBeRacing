using UnityEngine;

public class PlayerCarSoundManager : MonoBehaviour, IControllable
{
    public IncontrolProvider incontrolProvider;
    public string unsharedSoundStart;
    public Rigidbody carRigidbody;

    private bool canSound;

    private MyCarController myCarController;

    private void OnEnable()
    {
        canSound = true;
        myCarController = GetComponent<MyCarController>();
        
        ConnectDisconnectManager.ConnectCarSoundManager += ConnectSound;
        ConnectDisconnectManager.DisconnectCarSoundManagerDelegate += DisconnectSound;
    }

    private void OnDisable()
    {
        ConnectDisconnectManager.ConnectCarSoundManager -= ConnectSound;
        ConnectDisconnectManager.DisconnectCarSoundManagerDelegate -= DisconnectSound;
    }

    public void Control(IDevice device)
    {
        if (canSound)
        {
            if (device.State.Jump.IsPressed && myCarController.canTurbo)
                AkSoundEngine.PostEvent("Turbo_In", gameObject);

            if (device.State.Jump.IsReleased && myCarController.canTurbo)
                AkSoundEngine.PostEvent("Turbo_Out", gameObject);

            if (device.State.RightBumper.IsPressed)
                AkSoundEngine.PostEvent("Freno_In", gameObject);

            if (device.State.RightBumper.IsReleased)
                AkSoundEngine.PostEvent("Freno_Out", gameObject);

            /*if(device.State.Horizontal.IsReleased)
                AkSoundEngine.PostEvent("Bascula_Compresor_Out", gameObject);*/

            if (device.State.RightTrigger.IsPressed)
                AkSoundEngine.PostEvent(unsharedSoundStart + "_Acelerar_In", gameObject);

            if (device.State.RightTrigger.IsReleased)
                AkSoundEngine.PostEvent(unsharedSoundStart + "_Acelerar_Out", gameObject);

            AkSoundEngine.SetRTPCValue("Player_Velocidad", carRigidbody.velocity.magnitude * 1f);
        }

    }

    public void ConnectSound()
    {
        canSound = true;
        Core.Input.AssignControllable(incontrolProvider, this);
    }
    
    public void DisconnectSound()
    {
        canSound = false;
        Core.Input.UnassignControllable(incontrolProvider,this);
    }
}
