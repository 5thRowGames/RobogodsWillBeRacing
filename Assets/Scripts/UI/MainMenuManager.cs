using System.Collections.Generic;
using DG.Tweening;
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
    public List<RectTransform> industrialArmRace; //0: Brazo padre, 1 pinza superior, 2 pinza inferior
    public List<RectTransform> industrialArmSettings; //0: Brazo padre, 1 pinza superior, 2 pinza inferior
    public List<RectTransform> industrialArmCredits; //0: Brazo padre, 1 pinza superior, 2 pinza inferior
    public List<RectTransform> industrialArmExit; //0: Brazo padre, 1 pinza superior, 2 pinza inferior

    [Header("Tween positions")]
    //Se van a utilizar los valores de "X" e "Y" por separado según convenga.
    public Vector2 buttonPosition;
    public Vector2 metalSheetPosition;
    public Vector2 infoTitlePosition;


    [Space(5)]
    public float movementDuration;

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
        
        SoundManager.Instance.PlayFx((int) SoundManager.Fx.UI_Panel_In);

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
        
        SoundManager.Instance.PlayFx((int) SoundManager.Fx.UI_Panel_In);
        
        Sequence sequence = DOTween.Sequence();
        sequence.Insert((movementDuration/2f), raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert((movementDuration/2f),settingsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert((movementDuration/2f), creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert((movementDuration/2f), exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
            .Insert((movementDuration/2f),metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true))
            .Insert(0f, infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true)).OnComplete(() =>
            {
                UIEventManager.Instance.ChangeScreen(MenuType.Menu.TitleScreen);
                gameObject.SetActive(false);
                isInfoPanelHidden = true;
            });
    }

    public void HideMenuWithoutRaceButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Servos_In);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(industrialArmRace[0].DOAnchorPosX(0, 0.5f))
            .Insert(0.5f, industrialArmRace[1].DORotate(new Vector3(0, 0, -60), 0.3f))
            .Insert(0.5f, industrialArmRace[2].DORotate(new Vector3(0, 0, -112), 0.3f).OnComplete(HideMenuNoRace))
            .Insert(0.8f,raceButton.DOAnchorPosX(-1000,0.8f))
            .Insert(0.8f, industrialArmRace[0].DOAnchorPosX(-1000, 0.8f));
    }
    
    public void HideMenuWithoutSettingsButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Servos_In);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(industrialArmSettings[0].DOAnchorPosX(0, 0.5f))
            .Insert(0.5f, industrialArmSettings[1].DORotate(new Vector3(0, 0, -60), 0.3f))
            .Insert(0.5f, industrialArmSettings[2].DORotate(new Vector3(0, 0, -112), 0.3f).OnComplete(HideMenuNoSettings))
            .Insert(0.8f,settingsButton.DOAnchorPosX(-1000,0.8f))
            .Insert(0.8f, industrialArmSettings[0].DOAnchorPosX(-1000, 0.8f));
    }
    
    public void HideMenuWithoutCreditsButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Servos_In);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(industrialArmCredits[0].DOAnchorPosX(0, 0.5f))
            .Insert(0.5f, industrialArmCredits[1].DORotate(new Vector3(0, 0, -60), 0.3f))
            .Insert(0.5f, industrialArmCredits[2].DORotate(new Vector3(0, 0, -112), 0.3f).OnComplete(HideMenuNoCredits))
            .Insert(0.8f,creditsButton.DOAnchorPosX(-1000,0.8f))
            .Insert(0.8f, industrialArmCredits[0].DOAnchorPosX(-1000, 0.8f));
    }
    
    public void HideMenuWithoutExitButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIEventManager.Instance.inControlInputModule.enabled = false;
        
        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Servos_In);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(industrialArmExit[0].DOAnchorPosX(0, 0.5f))
            .Insert(0.5f, industrialArmExit[1].DORotate(new Vector3(0, 0, -60), 0.3f))
            .Insert(0.5f, industrialArmExit[2].DORotate(new Vector3(0, 0, -112), 0.3f).OnComplete(HideMenuNoExit))
            .Insert(0.8f,exitButton.DOAnchorPosX(-1000,0.8f))
            .Insert(0.8f, industrialArmExit[0].DOAnchorPosX(-1000, 0.8f));
    }

    private void HideMenuNoRace()
    {
        SoundManager.Instance.PlayFx((int) SoundManager.Fx.UI_Panel_In);
        
        Sequence sequence = DOTween.Sequence();
         sequence.Insert((movementDuration/2f),settingsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert((movementDuration/2f), creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
            .Insert(0f,infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true))
            .Insert((movementDuration/2f),metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true))
            .Insert((movementDuration/2f), exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
                {
                    isInfoPanelHidden = true;
                    UIEventManager.Instance.ChangeScreen(MenuType.Menu.CharacterSelection);
                    gameObject.SetActive(false);
                });
    }
    
    private void HideMenuNoSettings()
    {
        SoundManager.Instance.PlayFx((int) SoundManager.Fx.UI_Panel_In);
        
        Sequence sequence = DOTween.Sequence();
            sequence.Insert((movementDuration/2f), raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
                .Insert((movementDuration/2f), creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
                .Insert(0f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
                .Insert(0f,infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true))
                .Insert((movementDuration/2f),metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true))
                .Insert((movementDuration/2f), exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
                {
                    isInfoPanelHidden = true;
                    UIEventManager.Instance.ChangeScreen(MenuType.Menu.Settings);
                    gameObject.SetActive(false);
                });
    }
    
    private void HideMenuNoCredits()
    {
        SoundManager.Instance.PlayFx((int) SoundManager.Fx.UI_Panel_In);
        
        Sequence sequence = DOTween.Sequence();
        sequence.Insert((movementDuration/2f),raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert((movementDuration/2f), settingsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
            .Insert(0f,infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true))
            .Insert((movementDuration/2f),metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true))
            .Insert((movementDuration/2f), exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
            {
                isInfoPanelHidden = true;
                UIEventManager.Instance.ChangeScreen(MenuType.Menu.Credits);
                gameObject.SetActive(false);
            });
    }
    
    private void HideMenuNoExit()
    {
        SoundManager.Instance.PlayFx((int) SoundManager.Fx.UI_Panel_In);
        
        Sequence sequence = DOTween.Sequence();
        sequence.Insert((movementDuration/2f),raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert((movementDuration/2f),settingsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert((movementDuration/2f), creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0f,mainMenuTitle.DOAnchorPosX(infoTitlePosition.x,movementDuration,true))
            .Insert(0f,infoPanel.DOAnchorPosX(-infoTitlePosition.x,movementDuration,true))
            .Insert((movementDuration/2f),metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true)).OnComplete(() =>
            {
                isInfoPanelHidden = true;
                gameObject.SetActive(false);
                ExitGame();
            });
    }

    #endregion
}
