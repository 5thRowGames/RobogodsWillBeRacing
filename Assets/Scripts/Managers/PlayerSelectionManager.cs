using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class PlayerSelectionManager : MonoBehaviour
{
    public List<IncontrolProvider> playersInControl;
    public List<CharacterSelectionController> playerSelectionController;
    
    private int playerOrder;
    private MyPlayerActions keyboardListener;
    private MyPlayerActions joystickListener;
    private bool keyboardSelected;

    private void OnEnable()
    {
        keyboardListener = MyPlayerActions.BindKeyboard();
        joystickListener = MyPlayerActions.BindControls();
        playerOrder = 0;
        keyboardSelected = false;
    }

    void Update()
    {
        if (JoinButtonWasPressed(joystickListener))
        {   
            var inputDevice = InputManager.ActiveDevice;

            if (FindPlayerUsingDevice(inputDevice) == null && RaceManager.Instance.players < 4)
            {
                RaceManager.Instance.players++;
                playersInControl[playerOrder].controlType = IncontrolProvider.ControlType.Gamepad;
                playersInControl[playerOrder].myPlayerActions = MyPlayerActions.BindControls();
                playersInControl[playerOrder].InputDevice = inputDevice;
                Core.Input.AssignControllable(playersInControl[playerOrder],playerSelectionController[playerOrder]);
                playerSelectionController[playerOrder].JoinGamePressed();
                playerOrder++;
            }
        }
        
        //Prueba
        if (JoinButtonWasPressed(keyboardListener))
        {

            if (RaceManager.Instance.players < 4 && !keyboardSelected)
            {
                keyboardSelected = true;
                
                RaceManager.Instance.players++;
                playersInControl[playerOrder].controlType = IncontrolProvider.ControlType.Keyboard;
                playersInControl[playerOrder].InputDevice = null;
                playersInControl[playerOrder].myPlayerActions = keyboardListener;
                Core.Input.AssignControllable(playersInControl[playerOrder],playerSelectionController[playerOrder]);
                playerSelectionController[playerOrder].JoinGamePressed();
                playerOrder++;
            }
        }

    }
    
    bool JoinButtonWasPressed( MyPlayerActions actions )
    {
        return actions.Submit.WasPressed;
    }

    IDevice FindPlayerUsingDevice( InputDevice inputDevice )
    {
        return playersInControl.Find(x => x.InputDevice == inputDevice);
    }
}
