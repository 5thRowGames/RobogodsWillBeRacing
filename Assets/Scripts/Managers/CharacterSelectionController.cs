using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour,IControllable
{
    public List<Image> images;
    public int playerID;

    private int position;
    private bool confirm;
    private bool confirmed;
    private bool canChooseGod;
    
    

    public God.Type robogodPicked;

    private void Awake()
    {
        robogodPicked = God.Type.None;
        position = 0;
        confirm = false;
        confirmed = false;
        canChooseGod = false;
    }

    private void OnEnable()
    {
        ConnectDisconnectManager.ConnectCharacterSelectionControllerDelegate += ConnectCharacterSelection;
        ConnectDisconnectManager.DisconnectCarControllerDelegate += DisconnectCharacterSelection;
    }

    private void OnDisable()
    {
        ConnectDisconnectManager.ConnectCharacterSelectionControllerDelegate -= ConnectCharacterSelection;
        ConnectDisconnectManager.DisconnectCarControllerDelegate -= DisconnectCharacterSelection;
    }

    public void Control(IDevice controller)
    {

        if (confirm)
        {
            if (controller.State.Submit.IsPressed && !confirmed)
            {
                CharacterSelectionManager.Instance.ConfirmConfirmation(playerID);
            }
            else if (controller.State.Cancel.IsPressed)
            {
                CharacterSelectionManager.Instance.DenyConfirmation();
                CharacterSelectionManager.Instance.DeselectCharacter(robogodPicked);
                robogodPicked = God.Type.None;
                canChooseGod = true;
            }
        }
        else
        {
            if (canChooseGod)
            {
                if (controller.State.Horizontal.IsPressed)
                {
                    if (controller.State.Horizontal.Value < -0.3)
                    {
                        MoveMark(true);
                    }
                    else if(controller.State.Horizontal.Value > 0.3)
                    {
                        MoveMark(false);
                    }
                }
            
                if(controller.State.Submit.IsPressed)
                    ChooseGod();
            }
            else
            {
                if(controller.State.Cancel.IsPressed)
                    DeselectGod();
            }
        }
    }

    private void MoveMark(bool moveLeft)
    {
        images[position].enabled = false;
        if (!moveLeft)
        {
            position++;

            if (position > 3)
            {
                position = 0;

                images[position].enabled = true;
            }
            else
            {
                images[position].enabled = true;
            }
        }
        else
        {
            position--;

            if (position < 0)
            {
                position = 3;

                images[position].enabled = true;
            }
            else
            {
                images[position].enabled = true;
            }
        }
    }

    private void ChooseGod()
    {
        switch (position)
        {
            case 0:

                if (!CharacterSelectionManager.Instance.poseidonChosen)
                {
                    CharacterSelectionManager.Instance.ChooseCharacter(God.Type.Poseidon);
                    robogodPicked = God.Type.Poseidon;
                    canChooseGod = false;
                }
                break;
            
            case 1:
                    
                if (!CharacterSelectionManager.Instance.kaliChosen)
                {
                    CharacterSelectionManager.Instance.ChooseCharacter(God.Type.Kali);
                    robogodPicked = God.Type.Kali;
                    canChooseGod = false;
                }
                break;
            
            case 2:

                if (!CharacterSelectionManager.Instance.thorChosen)
                {
                    CharacterSelectionManager.Instance.ChooseCharacter(God.Type.Thor);
                    robogodPicked = God.Type.Thor;
                    canChooseGod = false;
                }
                break;
            
            case 3:

                if (!CharacterSelectionManager.Instance.anubisChosen)
                {
                    CharacterSelectionManager.Instance.ChooseCharacter(God.Type.Anubis);
                    robogodPicked = God.Type.Anubis;
                    canChooseGod = false;
                }
                break;
        }
    }
    
    private void DeselectGod()
    {
        if (robogodPicked != God.Type.None)
        {

            robogodPicked = God.Type.None;
            canChooseGod = true;
            
            switch (position)
            {
                case 0:
                    CharacterSelectionManager.Instance.DeselectCharacter(God.Type.Poseidon);
                    break;
            
                case 1:
                    CharacterSelectionManager.Instance.DeselectCharacter(God.Type.Kali);
                    break;
            
                case 2:
                    CharacterSelectionManager.Instance.DeselectCharacter(God.Type.Thor);
                    break;
            
                case 3:
                    CharacterSelectionManager.Instance.DeselectCharacter(God.Type.Anubis);
                    break;
            }
        }
    }

    public void Confirm()
    {
        confirmed = false;
        confirm = true;
    }

    public void Disconfirm()
    {
        confirmed = false;
        confirm = false;
    }

    public void JoinGamePressed()
    {
        images[0].enabled = true;
        StartCoroutine(DelayBetweenEnterAndSelect());
    }

    IEnumerator DelayBetweenEnterAndSelect()
    {
        yield return new WaitForSeconds(0.2f);
        canChooseGod = true;
    }

    public void ConnectCharacterSelection()
    {
        Core.Input.AssignControllable(GetComponent<IncontrolProvider>(),this);
    }

    public void DisconnectCharacterSelection()
    {
        Core.Input.AssignControllable(GetComponent<IncontrolProvider>(),this);
    }
    
}
