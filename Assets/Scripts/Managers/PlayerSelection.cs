using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

//Clase que permite a los jugadores unirse a un juego, tienen que pulsar un botón específico para ello
public class PlayerSelection : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public List<Transform> spawnPosition;

    private int spawnId;

    public static List<GameObject> players = new List<GameObject>();
    private List<IncontrolProvider> devices = new List<IncontrolProvider>();

    private MyPlayerActions keyboardListener;
    private MyPlayerActions joystickListener;

    private bool keyboardUsed = false;

    private void Awake()
    {
        keyboardListener = MyPlayerActions.BindKeyboard();
        joystickListener = MyPlayerActions.BindControls();
        
    }

    //Se diferencia entre un teclado o un mando para crear un jugador (solo puede haber un teclado activo)
    void Update ()
    {
        if (JoinButtonWasPressed(joystickListener))
        {   
            var inputDevice = InputManager.ActiveDevice;

            if (FindPlayerUsingDevice(inputDevice) == null)
            {
                CreatePlayer(inputDevice);
                spawnId++;
            }
        }

        if (JoinButtonWasPressed(keyboardListener))
        {
            if (!FindPlayerUsingKeyboard())
            {
                keyboardUsed = true;
                CreatePlayer(null);
                spawnId++;
            }
        }
    }

    //Crea un jugador (spawnea un prefab en la escena) y le asigna el control correspondiente, además actualiza los dispositivos conectados
    private void CreatePlayer(InputDevice inputDevice)
    {
        GameObject player = Instantiate(prefabToInstantiate, spawnPosition[spawnId].position, Quaternion.identity);

        var actions = inputDevice == null ? MyPlayerActions.BindKeyboard() : MyPlayerActions.BindControls();

        var device = player.GetComponent<IncontrolProvider>();
        
        device.myPlayerActions = actions;
        device.InputDevice = inputDevice;
        
        //Posible borrar ya que no hace falta saber si se ha conectado el mando correcto
        if(inputDevice != null)
            device.Meta = inputDevice.Meta;

        Core.Input.AssignPlayer(player.GetComponent<PlayerManager>(), device);
        Core.Input.ConnectDevice(device);
        players.Add(player);
        devices.Add(device);
    }
    
    bool JoinButtonWasPressed( MyPlayerActions actions )
    {
        return actions.Gas.WasPressed;
    }

    IDevice FindPlayerUsingDevice( InputDevice inputDevice )
    {
        return devices.Find(x => x.InputDevice == inputDevice);
    }

    bool FindPlayerUsingKeyboard()
    {
        return keyboardUsed;
    }
}
