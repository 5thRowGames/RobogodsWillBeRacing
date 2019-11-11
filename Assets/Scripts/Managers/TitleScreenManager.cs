using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public RectTransform pressToPlay;
    public Image fade;

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
        fade.DOFade(1f, 1f);
        
        //Sonido de la entrada de "Pulsa cualquier tecla"
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
        InputDevice device = InputManager.ActiveDevice;
        
        if (!controlSubmit && (device.AnyButton.IsPressed || Input.anyKeyDown))
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

        fade.DOFade(0, 1f);
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
