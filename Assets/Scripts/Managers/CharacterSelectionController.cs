using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour,IControllable
{
    public int player;
    public List<Image> images;
    public Sprite lampOff;
    public Sprite lampOn;

    private int position;
    private bool confirm;
    private bool confirmed;
    private bool canChooseGod;

    public God.Type robogodPicked;
    
    private void OnEnable()
    {

        foreach (var image in images)
        {
            image.enabled = false;
        }
        
        robogodPicked = God.Type.None;
        position = 0;
        confirm = false;
        confirmed = false;
        canChooseGod = false;
    }

    private void OnDisable()
    {
        DisconnectCharacterSelection();
    }

    public void Control(IDevice controller)
    {
        if (confirm)
        {
            if (controller.State.Submit.IsPressed && !confirmed)
            {
                CharacterSelectionManager.Instance.ConfirmConfirmation(player);
            }
            else if (controller.State.Cancel.IsPressed)
            {
                SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
                
                CharacterSelectionManager.Instance.DenyConfirmation();
                CharacterSelectionManager.Instance.DeselectCharacter(robogodPicked);
                robogodPicked = God.Type.None;
                canChooseGod = true;
                images[position].sprite = lampOff;
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
                        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cursor_Holograma_In);
                    }
                    else if(controller.State.Horizontal.Value > 0.3)
                    {
                        MoveMark(false);
                        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cursor_Holograma_In);
                    }
                }
                
                if (controller.State.Submit.IsPressed)
                {
                    SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Select_Holograma_In);
                    images[position].sprite = lampOn;
                    ChooseGod();
                }
            }
            else
            {
                if (controller.State.Cancel.IsPressed)
                {
                    images[position].sprite = lampOff;
                    DeselectGod();
                }
                    
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
                
                if (!CharacterSelectionManager.Instance.anubisChosen)
                {
                    CharacterSelectionManager.Instance.ChooseCharacter(God.Type.Anubis);
                    robogodPicked = God.Type.Anubis;
                    canChooseGod = false;
                }
                break;
            
            case 1:
                
                if (!CharacterSelectionManager.Instance.poseidonChosen)
                {
                    CharacterSelectionManager.Instance.ChooseCharacter(God.Type.Poseidon);
                    robogodPicked = God.Type.Poseidon;
                    canChooseGod = false;
                }
                
                break;
            
            case 2:

                if (!CharacterSelectionManager.Instance.kaliChosen)
                {
                    CharacterSelectionManager.Instance.ChooseCharacter(God.Type.Kali);
                    robogodPicked = God.Type.Kali;
                    canChooseGod = false;
                }
                break;
            
            case 3:

                if (!CharacterSelectionManager.Instance.thorChosen)
                {
                    CharacterSelectionManager.Instance.ChooseCharacter(God.Type.Thor);
                    robogodPicked = God.Type.Thor;
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

    private void CancelCharacterSelection()
    {
        foreach (var img in images)
        {
            img.enabled = false;
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

    public void DisconnectCharacterSelection()
    {
        Core.Input.UnassignControllable(GetComponent<IncontrolProvider>(),this);
    }
    
}
