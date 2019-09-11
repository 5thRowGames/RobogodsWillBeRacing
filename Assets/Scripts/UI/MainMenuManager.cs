﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public RectTransform raceButton;
    public RectTransform settingsButton;
    public RectTransform creditsButton;
    public RectTransform exitButton;
    public RectTransform mainMenuTitle;
    public RectTransform infoPanel;
    public RectTransform metalSheet;

    [Header("Tween positions")]
    //Se van a utilizar los valores de "X" e "Y" por separado según convenga.
    public Vector2 buttonPosition;
    public Vector2 metalSheetPosition;
    public Vector2 infoTitlePosition;

    public Vector2 raceButtonSelectedPosition;
    public Vector2 settingsButtonSelectedPosition;
    public Vector2 creditsButtonSelectedPosition;
    public Vector2 exitButtonSelectedPosition;


    [Space(5)] 
    public float infoPanelPositionY;
    public float movementDuration;
    public float selectDuration;

    private bool isInfoPanelHidden;

    private void Awake()
    {
        isInfoPanelHidden = true;
    }

    private void OnEnable()
    {
        ResetMenu();
        BuildMainMenu();
    }

    private void OnDisable()
    {
        ResetMenu();
    }

    #region Functionality
    
    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion
    
    #region Tweens

    private void ResetMenu()
    {
        raceButton.anchoredPosition = new Vector2(buttonPosition.x, 0);
        settingsButton.anchoredPosition = new Vector2(buttonPosition.x, 0);
        creditsButton.anchoredPosition = new Vector2(buttonPosition.x, 0);
        exitButton.anchoredPosition = new Vector2(buttonPosition.x, 0);
        mainMenuTitle.anchoredPosition = new Vector2(infoTitlePosition.x, 0);
        metalSheet.anchoredPosition = new Vector2(metalSheetPosition.x, 0);
        infoPanel.anchoredPosition = new Vector2(-infoTitlePosition.x, 0);
    }
    
    public void BuildMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = raceButton.gameObject;

        Sequence sequence = DOTween.Sequence();
            sequence.Insert(0f, raceButton.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f, creditsButton.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f, exitButton.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f, settingsButton.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f,mainMenuTitle.DOAnchorPosX(0,movementDuration,true))
            .Insert(0f,metalSheet.DOAnchorPosX(0,movementDuration,true))
            .Insert(0.1f, infoPanel.DOAnchorPosX(0, movementDuration, true))
            .OnComplete(() =>
            {
                UIEventManager.Instance.inControlInputModule.enabled = true;
                isInfoPanelHidden = false;
                EventSystem.current.SetSelectedGameObject(raceButton.gameObject);
            });
    }

    //TODO
    //Habrá que hacer el sistema de apagado de luces y eso
    public void ReturnTitleScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0f, raceButton.DOAnchorPosX(buttonPosition.x, movementDuration, true))
            .Insert(0f,settingsButton.DOAnchorPosX(buttonPosition.x, movementDuration, true))
            .Insert(0f, creditsButton.DOAnchorPosX(buttonPosition.x, movementDuration, true))
            .Insert(0f, exitButton.DOAnchorPosX(buttonPosition.x, movementDuration, true))
            .Insert(0f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
            .Insert(0f,metalSheet.DOAnchorPosX(metalSheetPosition.x,movementDuration,true))
            .Insert(0f, infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true)).OnComplete(() =>
            {
                UIEventManager.Instance.ChangeScreen(MenuType.Menu.TitleScreen);
                gameObject.SetActive(false);
                isInfoPanelHidden = true;
            });
    }

    public void HideMenuRaceSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(raceButton.DOAnchorPos(raceButtonSelectedPosition, selectDuration, true))
            .Insert(0.6f, settingsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f, creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.3f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
            .Insert(0.3f,infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true))
            .Insert(0.6f,metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true))
            .Insert(0.6f, exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
                {
                    isInfoPanelHidden = true;
                    UIEventManager.Instance.ChangeScreen(MenuType.Menu.CharacterSelection);
                    gameObject.SetActive(false);
                });
    }
    
    public void HideMenuSettingsSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsButton.DOAnchorPos(settingsButtonSelectedPosition, selectDuration, true))
            .Insert(0.6f, raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f, creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f,metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true))
            .Insert(0.3f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
            .Insert(0.3f,infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true))
            .Insert(0.6f, exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
            {
                UIEventManager.Instance.ChangeScreen(MenuType.Menu.Settings);
                gameObject.SetActive(false);
            });
    }
    
    public void HideMenuCreditsSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(creditsButton.DOAnchorPos(creditsButtonSelectedPosition, selectDuration, true))
            .Insert(0.6f, raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f, settingsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f,metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true))
            .Insert(0.3f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
            .Insert(0.3f,infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true))
            .Insert(0.6f, exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
            {
                UIEventManager.Instance.ChangeScreen(MenuType.Menu.Credits);
                gameObject.SetActive(false);
            });
    }
    
    //TODO
    //Añadir el sistema de luces como si se volviese al title screen
    public void HideMenuExitSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsButton.DOAnchorPos(exitButtonSelectedPosition, selectDuration, true))
            .Insert(0.6f, raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f, creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f,metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true))
            .Insert(0.3f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
            .Insert(0.3f,infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true))
            .Insert(0.6f, exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
    
    #endregion
}