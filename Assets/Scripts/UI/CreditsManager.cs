using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsManager : MonoBehaviour
{
    public RectTransform credits;
    public Vector2 creditsPosition;
    public float movementDuration;
    
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
        credits.anchoredPosition = new Vector2(creditsPosition.x, 0);
    }
    
    #region Tweens

    private void BuildCredits()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //UIEventManager.Instance.inControlInputModule.enabled = false;
        
        credits.DOMoveX(0, movementDuration, true).OnComplete(() =>
        {
            UIEventManager.Instance.inControlInputModule.enabled = true;
            //TODO
            //Habrá que hacer algo aquí relacionado con el scroll
        });
    }

    public void HideCredits()
    {
        UIEventManager.Instance.inControlInputModule.enabled = false;

        credits.DOMoveY(creditsPosition.y, movementDuration, true).OnComplete(() =>
        {
            UIEventManager.Instance.inControlInputModule.enabled = false;
            UIEventManager.Instance.ChangeScreen(MenuType.Menu.MainMenu);
            gameObject.SetActive(false);
        });
    }
    
    #endregion
}
