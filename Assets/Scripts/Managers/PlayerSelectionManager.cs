using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class PlayerSelectionManager : Singleton<PlayerSelectionManager>
{
    public List<IncontrolProvider> playersInControl;
    public List<CharacterSelectionController> playerSelectionController;
    
    private int playerOrder;
    private MyPlayerActions keyboardListener;
    private MyPlayerActions joystickListener;
    private bool keyboardSelected;

    public bool canJoin;

    private void OnEnable()
    {
        canJoin = true;
        keyboardListener = MyPlayerActions.BindKeyboard();
        joystickListener = MyPlayerActions.BindControls();
        playerOrder = 0;
        keyboardSelected = false;
    }

    void Update()
    {
        if (canJoin)
        {
            if (JoinButtonWasPressed(joystickListener))
            {   
                var inputDevice = InputManager.ActiveDevice;

                if (FindPlayerUsingDevice(inputDevice) == null && StoreGodInfo.Instance.players < 4)
                {
                    StoreGodInfo.Instance.players++;
                    playersInControl[playerOrder].playerID = playerOrder;
                    playersInControl[playerOrder].controlType = IncontrolProvider.ControlType.Gamepad;
                    playersInControl[playerOrder].myPlayerActions = MyPlayerActions.BindControls();
                    playersInControl[playerOrder].InputDevice = inputDevice;
                    Core.Input.AssignControllable(playersInControl[playerOrder],playerSelectionController[playerOrder]);
                    playerSelectionController[playerOrder].JoinGamePressed();
                    SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Select);
                    playerOrder++;
                }
            }
            if (JoinButtonWasPressed(keyboardListener))
            {
                /*if (StoreGodInfo.Instance.players < 4 && !keyboardSelected)
                {
                    keyboardSelected = true;
                    StoreGodInfo.Instance.players++;
                    playersInControl[playerOrder].playerID = playerOrder;
                    playersInControl[playerOrder].controlType = IncontrolProvider.ControlType.Keyboard;
                    playersInControl[playerOrder].InputDevice = null;
                    playersInControl[playerOrder].myPlayerActions = keyboardListener;
                    Core.Input.AssignControllable(playersInControl[playerOrder],playerSelectionController[playerOrder]);
                    playerSelectionController[playerOrder].JoinGamePressed();
                    SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Select);
                    playerOrder++;
                }*/
                
                if (StoreGodInfo.Instance.players < 4 )
                {
                    if (playerOrder == 0)
                    {
                        keyboardSelected = true;
                        StoreGodInfo.Instance.players++;
                        playersInControl[playerOrder].playerID = playerOrder;
                        playersInControl[playerOrder].controlType = IncontrolProvider.ControlType.Keyboard;
                        playersInControl[playerOrder].InputDevice = null;
                        playersInControl[playerOrder].myPlayerActions = MyPlayerActions.Eduardo1();
                        Core.Input.AssignControllable(playersInControl[playerOrder],playerSelectionController[playerOrder]);
                        playerSelectionController[playerOrder].JoinGamePressed();
                        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Select);
                        playerOrder++;
                    }
                    else if(playerOrder == 1)
                    {
                        keyboardSelected = true;
                        StoreGodInfo.Instance.players++;
                        playersInControl[playerOrder].playerID = playerOrder;
                        playersInControl[playerOrder].controlType = IncontrolProvider.ControlType.Keyboard;
                        playersInControl[playerOrder].InputDevice = null;
                        playersInControl[playerOrder].myPlayerActions = MyPlayerActions.Eduardo2();
                        Core.Input.AssignControllable(playersInControl[playerOrder],playerSelectionController[playerOrder]);
                        playerSelectionController[playerOrder].JoinGamePressed();
                        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Select);
                        playerOrder++;
                    }
                    else if (playerOrder == 2)
                    {
                        keyboardSelected = true;
                        StoreGodInfo.Instance.players++;
                        playersInControl[playerOrder].playerID = playerOrder;
                        playersInControl[playerOrder].controlType = IncontrolProvider.ControlType.Keyboard;
                        playersInControl[playerOrder].InputDevice = null;
                        playersInControl[playerOrder].myPlayerActions = MyPlayerActions.Eduardo3();
                        Core.Input.AssignControllable(playersInControl[playerOrder],playerSelectionController[playerOrder]);
                        playerSelectionController[playerOrder].JoinGamePressed();
                        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Select);
                        playerOrder++;
                    }
                    else if (playerOrder == 3)
                    {
                        keyboardSelected = true;
                        StoreGodInfo.Instance.players++;
                        playersInControl[playerOrder].playerID = playerOrder;
                        playersInControl[playerOrder].controlType = IncontrolProvider.ControlType.Keyboard;
                        playersInControl[playerOrder].InputDevice = null;
                        playersInControl[playerOrder].myPlayerActions = MyPlayerActions.Eduardo4();
                        Core.Input.AssignControllable(playersInControl[playerOrder],playerSelectionController[playerOrder]);
                        playerSelectionController[playerOrder].JoinGamePressed();
                        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Select);
                        playerOrder++;
                    }
                }
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
