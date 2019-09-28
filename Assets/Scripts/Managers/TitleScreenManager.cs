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

    private void Start()
    {
        SoundManager.Instance.PlayLoop(SoundManager.Music.UI);
    }

    private void OnEnable()
    {
        UIEventManager.Instance.inControlInputModule.enabled = true;
        EventSystem.current.SetSelectedGameObject(null);
        controlSubmit = false;
        initialTweenComplete = false;
        TitleTween();
        
        //Sonido de la entrada de "Pulsa A"
        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cortinilla_In);
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
        if (!controlSubmit && UIEventManager.Instance.inControlInputModule.SubmitAction.WasPressed)
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
        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Select);
        yield return new WaitForSeconds(0.3f);
        UIEventManager.Instance.ChangeScreen(MenuType.Menu.MainMenu);
        gameObject.SetActive(false);
        //EventSystem.current.firstSelectedGameObject = raceButton;
        //sinControlInputModule.enabled = false;
    }

    private void TitleTween()
    {
        firstTween = pressToPlay.DOAnchorPosY(150, 1f, true).OnComplete(() => { initialTweenComplete = true; });
    }
}
