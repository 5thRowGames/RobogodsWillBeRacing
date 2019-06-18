using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class PlayerSelectionManager : MonoBehaviour
{
    public List<IncontrolProvider> players;
    public List<CharacterSelectionController> playerSelectionController;
    
    private int playerOrder;
    private MyPlayerActions keyboardListener;
    private MyPlayerActions joystickListener;
    private bool keyboardSelected;
    
    //Prueba
    private MyPlayerActions both;

    private void OnEnable()
    {
        keyboardListener = MyPlayerActions.BindKeyboard();
        joystickListener = MyPlayerActions.BindControls();
        both = MyPlayerActions.BindBoth();
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
                players[playerOrder].myPlayerActions = MyPlayerActions.BindControls();
                players[playerOrder].InputDevice = inputDevice;
                Core.Input.AssignControllable(players[playerOrder],playerSelectionController[playerOrder]);
                Core.Input.ConnectDevice(players[playerOrder]);
                playerSelectionController[playerOrder].JoinGamePressed();
                playerOrder++;
            }
        }
        
        //Prueba
        if (JoinButtonWasPressed(keyboardListener))
        {

            if (RaceManager.Instance.players < 4 && !keyboardSelected)
            {
                //Quitar
                keyboardSelected = true;
                
                RaceManager.Instance.players++;
                players[playerOrder].InputDevice = null;
                players[playerOrder].myPlayerActions = keyboardListener; //Prueba
                Core.Input.AssignControllable(players[playerOrder],playerSelectionController[playerOrder]);
                Core.Input.ConnectDevice(players[playerOrder]);
                playerSelectionController[playerOrder].JoinGamePressed();
                playerOrder++;
            }
        }

    }
    
    bool JoinButtonWasPressed( MyPlayerActions actions )
    {
        return actions.Gas.WasPressed;
    }

    IDevice FindPlayerUsingDevice( InputDevice inputDevice )
    {
        return players.Find(x => x.InputDevice == inputDevice);
    }
}
