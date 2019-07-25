using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenManager : MonoBehaviour
{
    public RectTransform pressToPlay;

    private Tween firstTween;
    private bool controlSubmit;
    private bool initialTweenComplete;
    
    private void OnEnable()
    {
        UIManager.Instance.inControlInputModule.enabled = true;
        EventSystem.current.SetSelectedGameObject(null);
        controlSubmit = false;
        initialTweenComplete = false;
        TitleTween();
    }

    private void OnDisable()
    {
        pressToPlay.localScale = Vector3.one;
        pressToPlay.anchoredPosition = new Vector2(pressToPlay.anchoredPosition.x, -150);
        initialTweenComplete = false;
        controlSubmit = false;
    }
    
    void Update()
    {
        //No se si acceder así es eficiente (investigar)
        if (!controlSubmit && UIManager.Instance.inControlInputModule.SubmitAction.WasPressed)
        {
            controlSubmit = true;
            StartCoroutine(ButtonPressedTween());
        }
    }

    IEnumerator ButtonPressedTween()
    {
        if (!initialTweenComplete)
        {
            firstTween.Kill();
        }
        
        pressToPlay.DOScale(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(1.2f);
        UIManager.Instance.ChangeScreen(MenuType.Menu.MainMenu);
        gameObject.SetActive(false);
        //EventSystem.current.firstSelectedGameObject = raceButton;
        //sinControlInputModule.enabled = false;
    }

    private void TitleTween()
    {
        firstTween = pressToPlay.DOAnchorPosY(150, 1f, true).OnComplete(() => { initialTweenComplete = true; });
    }
}
