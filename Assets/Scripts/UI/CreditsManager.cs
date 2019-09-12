using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsManager : MonoBehaviour
{
    public RectTransform title;
    public RectTransform infoPanel;
    public RectTransform credits;
    public float topBotInitialX;
    public Vector2 creditsPosition;
    public float movementDuration;
    public float infoTweenDuration;
    public float titleTweenDuration;
    
    private bool controlSubmit;

    private void Update()
    {
        if (!controlSubmit && UIEventManager.Instance.inControlInputModule.CancelAction.WasPressed)
        {
            controlSubmit = true;
            HideCredits();
        }
    }

    private void OnEnable()
    {
        controlSubmit = false;
        ResetCredits();
        BuildCredits();
    }

    private void ResetCredits()
    {
        title.anchoredPosition = new Vector2(-topBotInitialX, 0);
        infoPanel.anchoredPosition = new Vector2(topBotInitialX, 0);
        credits.anchoredPosition = new Vector2(creditsPosition.x, 0);
    }
    
    #region Tweens

    private void BuildCredits()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //UIEventManager.Instance.inControlInputModule.enabled = false;

        Sequence seq = DOTween.Sequence();
        seq.Insert(0f, title.DOAnchorPosX(0f,titleTweenDuration))
            .Insert(0f, infoPanel.DOAnchorPosX(0f, infoTweenDuration))
            .Insert(0f,credits.DOAnchorPosX(0, movementDuration, true).OnComplete(() =>
            {
                UIEventManager.Instance.inControlInputModule.enabled = true;
            }));
    }

    public void HideCredits()
    {
        UIEventManager.Instance.inControlInputModule.enabled = false;

        Sequence seq = DOTween.Sequence();
        seq.Insert(0f, title.DOAnchorPosX(-topBotInitialX,titleTweenDuration))
            .Insert(0f, infoPanel.DOAnchorPosX(topBotInitialX, infoTweenDuration))
            .Insert(0.5f,credits.DOAnchorPosY(creditsPosition.y, movementDuration, true).OnComplete(() =>
            {
                UIEventManager.Instance.inControlInputModule.enabled = false;
                UIEventManager.Instance.ChangeScreen(MenuType.Menu.MainMenu);
                gameObject.SetActive(false);
            }));
    }
    
    #endregion
}
