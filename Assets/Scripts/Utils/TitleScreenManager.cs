using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject raceButton;
    public GameObject titleScreenPanel;
    public InControlInputModule inControlInputModule;
    public RectTransform pressToPlay;

    private bool controlSubmit;
    private bool initialTweenComplete;
    
    private void OnEnable()
    {
        inControlInputModule.enabled = true;
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

    // Update is called once per frame
    void Update()
    {
        if (!controlSubmit && inControlInputModule.SubmitAction.WasPressed)
        {
            controlSubmit = true;
            StartCoroutine(ButtonPressedTween());
        }
    }

    IEnumerator ButtonPressedTween()
    {
        if (initialTweenComplete)
        {
            pressToPlay.DOScale(Vector3.zero, 0.3f);
            yield return new WaitForSeconds(0.3f);
        }
        
        UIManager.Instance.BuildMainMenu();
        yield return new WaitForSeconds(1.2f);
        titleScreenPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(raceButton);
        EventSystem.current.firstSelectedGameObject = raceButton;
        //sinControlInputModule.enabled = false;
    }

    private void TitleTween()
    {
        pressToPlay.DOAnchorPosY(150, 1f, true).OnComplete(() => { initialTweenComplete = true; });
    }
}
