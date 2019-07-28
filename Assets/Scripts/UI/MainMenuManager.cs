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

    [Header("Tween positions")]
    //Se van a utilizar los valores de "X" e "Y" por separado según convenga.
    public Vector2 buttonPosition;

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
        mainMenuTitle.anchoredPosition = new Vector2(buttonPosition.x, 0);
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
            .OnComplete(() =>
            {
                UIManager.Instance.inControlInputModule.enabled = true;
                isInfoPanelHidden = false;
                EventSystem.current.SetSelectedGameObject(raceButton.gameObject);
            });

            if (isInfoPanelHidden)
                sequence.Insert(0.1f, infoPanel.DOAnchorPosY(0, movementDuration, true));
    }

    //TODO
    //Habrá que hacer el sistema de apagado de luces y eso
    public void ReturnTitleScreen()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0f, raceButton.DOAnchorPosX(buttonPosition.x, movementDuration, true))
            .Insert(0f,settingsButton.DOAnchorPosX(buttonPosition.x, selectDuration, true))
            .Insert(0f, creditsButton.DOAnchorPosX(buttonPosition.x, movementDuration, true))
            .Insert(0f, exitButton.DOAnchorPosX(buttonPosition.x, movementDuration, true))
            .Insert(0f,mainMenuTitle.DOAnchorPosX(buttonPosition.x,movementDuration,true))
            .Insert(0f, infoPanel.DOAnchorPosY(infoPanelPositionY,movementDuration,true)).OnComplete(() =>
            {
                UIManager.Instance.ChangeScreen(MenuType.Menu.TitleScreen);
                gameObject.SetActive(false);
                isInfoPanelHidden = true;
            });
    }

    public void HideMenuRaceSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(raceButton.DOAnchorPos(raceButtonSelectedPosition, selectDuration, true))
            .Insert(0.6f, settingsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f, creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f,mainMenuTitle.DOAnchorPosX(buttonPosition.y,movementDuration,true))
            .Insert(0.6f, exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
                {
                    isInfoPanelHidden = true;
                    UIManager.Instance.ChangeScreen(MenuType.Menu.CharacterSelection);
                    gameObject.SetActive(false);
                });
    }
    
    public void HideMenuSettingsSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsButton.DOAnchorPos(settingsButtonSelectedPosition, selectDuration, true))
            .Insert(0.6f, raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f, creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f,mainMenuTitle.DOAnchorPosY(buttonPosition.y,movementDuration,true))
            .Insert(0.6f, exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
            {
                UIManager.Instance.ChangeScreen(MenuType.Menu.Settings);
                gameObject.SetActive(false);
            });
    }
    
    public void HideMenuCreditsSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsButton.DOAnchorPos(creditsButtonSelectedPosition, selectDuration, true))
            .Insert(0.6f, raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f, creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f,mainMenuTitle.DOAnchorPosY(buttonPosition.y,movementDuration,true))
            .Insert(0.6f, exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
            {
                UIManager.Instance.ChangeScreen(MenuType.Menu.Credits);
                gameObject.SetActive(false);
            });
    }
    
    //TODO
    //Añadir el sistema de luces como si se volviese al title screen
    public void HideMenuExitSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        UIManager.Instance.inControlInputModule.enabled = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsButton.DOAnchorPos(exitButtonSelectedPosition, selectDuration, true))
            .Insert(0.6f, raceButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f, creditsButton.DOAnchorPosY(buttonPosition.y, movementDuration, true))
            .Insert(0.6f,mainMenuTitle.DOAnchorPosY(buttonPosition.y,movementDuration,true))
            .Insert(0.6f, exitButton.DOAnchorPosY(buttonPosition.y, movementDuration, true)).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
    
    #endregion
}
