using InControl;
using UnityEngine;

//Posible clase para controlar el tema de desconectar y conectar un mando nuevamente (solo que aparezca un mensaje en pantalla y pare el juego si eso).
public class ControllerManager : MonoBehaviour
{

    /*private void OnEnable()
    {
        InputManager.OnDeviceAttached += OnDeviceAttached;
        InputManager.OnDeviceDetached += OnDeviceDetached;
        
    }

    private void OnDisable()
    {
        InputManager.OnDeviceAttached -= OnDeviceAttached;
        InputManager.OnDeviceDetached -= OnDeviceDetached;
    }
    
    private void OnDeviceAttached(InputDevice inputDevice)
    {
        var device = Core.Input.GetDeviceState(inputDevice.Meta);

        if (device != null)
        {
            
        }
    }

    private void OnDeviceDetached(InputDevice inputDevice)
    {
        var device = devices.Find(x => x.InputDevice == inputDevice);
        devices.Remove(device);
        Core.Input.DisconnectDevice(device);
    }*/
}
