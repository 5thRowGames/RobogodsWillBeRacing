using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public Sprite keyboardControl;
    public Sprite gamepadControl;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public Image controlBackground;
    
    public RectTransform title;
    public RectTransform infoPanel;
    public RectTransform control;
    public float topBotInitialX;
    public Vector2 controlPosition;
    public float movementDuration;
    public float infoTweenDuration;
    public float titleTweenDuration;

    private bool controlSubmit;
    private bool canPressLeft;

    private void OnEnable()
    {
        controlSubmit = false;
        ResetControl();
        BuildControl();
    }

    private void Update()
    {

        if (!controlSubmit && UIEventManager.Instance.inControlInputModule.CancelAction.WasPressed)
        {
            controlSubmit = true;
            HideControl();
        }
        else if (!controlSubmit && UIEventManager.Instance.inControlInputModule.MoveAction.Left.WasPressed && leftArrow.activeInHierarchy)
        {
            controlSubmit = true;
            ChangeControlBackground(false);
        }
        else if(!controlSubmit && UIEventManager.Instance.inControlInputModule.MoveAction.Right.WasPressed && rightArrow.activeInHierarchy)
        {
            controlSubmit = true;
            ChangeControlBackground(true);
        }
    }

    private void ResetControl()
    {
        leftArrow.SetActive(false);
        rightArrow.SetActive(true);
        controlBackground.sprite = gamepadControl;
        title.anchoredPosition = new Vector2(-topBotInitialX, 0);
        infoPanel.anchoredPosition = new Vector2(topBotInitialX, 0);
        control.anchoredPosition = new Vector2(controlPosition.x, 0);
    }

    #region Tweens

    private void BuildControl()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //UIEventManager.Instance.inControlInputModule.enabled = false;
        
        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Panel_In);

        Sequence seq = DOTween.Sequence();
        seq.Insert(0f, title.DOAnchorPosX(0f,titleTweenDuration))
            .Insert(0f, infoPanel.DOAnchorPosX(0f, infoTweenDuration))
            .Insert(0f,control.DOAnchorPosX(0, movementDuration, true).OnComplete(() =>
            {
                UIEventManager.Instance.inControlInputModule.enabled = true;
            }));
    }

    private void HideControl()
    {
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Panel_In);

        Sequence seq = DOTween.Sequence();
        seq.Insert(0f, title.DOAnchorPosX(-topBotInitialX,titleTweenDuration))
            .Insert(0f, infoPanel.DOAnchorPosX(topBotInitialX, infoTweenDuration))
            .Insert(0.5f,control.DOAnchorPosY(controlPosition.y, movementDuration, true).OnComplete(() =>
            {
                UIEventManager.Instance.inControlInputModule.enabled = false;
                UIEventManager.Instance.ChangeScreen(MenuType.Menu.MainMenu);
                gameObject.SetActive(false);
            }));
    }

    private void ChangeControlBackground(bool right)
    {
        if (right)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
            controlBackground.sprite = keyboardControl;
        }
        else
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
            controlBackground.sprite = gamepadControl;
        }

        controlSubmit = false;


    }
    
    #endregion
}
