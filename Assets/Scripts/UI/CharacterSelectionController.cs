using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour,IControllable
{
    public int space;
    public RectTransform rectTransform;
    public Image mark;

    private int position;
    private bool confirm;
    private bool confirmed;
    private bool canChooseGod;

    public GodType.RobogodType robogodPicked;

    private void OnEnable()
    {
        robogodPicked = GodType.RobogodType.None;
        position = 0;
        confirm = false;
        confirmed = false;
        mark.enabled = false;
        canChooseGod = false;
    }

    public void Control(IDevice controller)
    {

        if (confirm)
        {
            if (controller.State.Jump.IsPressed && !confirmed)
            {
                UIManager.Instance.ConfirmConfirmation();
            }
            else if (controller.State.Action.IsPressed)
            {
                UIManager.Instance.DenyConfirmation();
                UIManager.Instance.DeselectCharacter(robogodPicked);
                robogodPicked = GodType.RobogodType.None;
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
            
                if(controller.State.Jump.IsPressed)
                    ChooseGod();
            }
            else
            {
                if(controller.State.Action.IsPressed)
                    DeselectGod();
            }
        }
    }

    private void MoveMark(bool moveLeft)
    {
        if (!moveLeft)
        {
            position++;

            if (position > 3)
            {
                position = 0;
                
                //Todo cambiar por sizeDelta o mirar de Ricardo
                rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x - space * 3, rectTransform.anchoredPosition.y);
            }
            else
            {
                //Todo cambiar por sizeDelta o mirar de Ricardo
                rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x + space, rectTransform.anchoredPosition.y);
            }
        }
        else
        {
            position--;

            if (position < 0)
            {
                position = 3;
                
                rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x + space * 3, rectTransform.anchoredPosition.y);
            }
            else
            {
                //Todo cambiar por sizeDelta o mirar de Ricardo
                rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x - space, rectTransform.anchoredPosition.y);
            }
        }
    }

    private void ChooseGod()
    {
        switch (position)
        {
            case 0:

                if (!UIManager.Instance.poseidonChosen)
                {
                    UIManager.Instance.ChooseCharacter(GodType.RobogodType.Poseidon);
                    robogodPicked = GodType.RobogodType.Poseidon;
                    canChooseGod = false;
                }
                break;
            
            case 1:
                    
                if (!UIManager.Instance.kaliChosen)
                {
                    UIManager.Instance.ChooseCharacter(GodType.RobogodType.Kali);
                    robogodPicked = GodType.RobogodType.Kali;
                    canChooseGod = false;
                }
                break;
            
            case 2:

                if (!UIManager.Instance.thorChosen)
                {
                    UIManager.Instance.ChooseCharacter(GodType.RobogodType.Thor);
                    robogodPicked = GodType.RobogodType.Thor;
                    canChooseGod = false;
                }
                break;
            
            case 3:

                if (!UIManager.Instance.anubisChosen)
                {
                    UIManager.Instance.ChooseCharacter(GodType.RobogodType.Anubis);
                    robogodPicked = GodType.RobogodType.Anubis;
                    canChooseGod = false;
                }
                break;
        }
    }
    
    private void DeselectGod()
    {
        if (robogodPicked != GodType.RobogodType.None)
        {

            robogodPicked = GodType.RobogodType.None;
            canChooseGod = true;
            
            switch (position)
            {
                case 0:
                    UIManager.Instance.DeselectCharacter(GodType.RobogodType.Poseidon);
                    break;
            
                case 1:
                    UIManager.Instance.DeselectCharacter(GodType.RobogodType.Kali);
                    break;
            
                case 2:
                    UIManager.Instance.DeselectCharacter(GodType.RobogodType.Thor);
                    break;
            
                case 3:
                    UIManager.Instance.DeselectCharacter(GodType.RobogodType.Anubis);
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
        mark.enabled = true;
        StartCoroutine(DelayBetweenEnterAndSelect());
    }

    IEnumerator DelayBetweenEnterAndSelect()
    {
        yield return new WaitForSeconds(0.2f);
        canChooseGod = true;
    }
    
}
