using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsManager : MonoBehaviour
{
    public RectTransform volumeSlider;
    public RectTransform soundEffectsSlider;
    public RectTransform languageSlider;
    public RectTransform settingsTitle;
    public RectTransform metalSheet;
    public RectTransform infoPanel;

    public Vector2 metalSheetPosition;
    public Vector2 sliderPosition;
    public Vector2 settingsTitlePosition;
    public float movementDuration;

    private void OnEnable()
    {
        ResetSettingsMenu();
        BuildSettings();
    }

    private void ResetSettingsMenu()
    {
        metalSheet.anchoredPosition = new Vector2(metalSheetPosition.x, 0);
        volumeSlider.anchoredPosition = new Vector2(metalSheetPosition.x, 0);
        soundEffectsSlider.anchoredPosition = new Vector2(metalSheetPosition.x, 0);
        languageSlider.anchoredPosition = new Vector2(metalSheetPosition.x, 0);
        settingsTitle.anchoredPosition = new Vector2(settingsTitlePosition.x, 0);
        infoPanel.anchoredPosition = new Vector2(-settingsTitlePosition.x, 0);
    }
    
    #region Tweens

    private void BuildSettings()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = volumeSlider.gameObject;

        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Panel_In);
        
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0f, volumeSlider.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f, soundEffectsSlider.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f, languageSlider.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f, settingsTitle.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f, metalSheet.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f, infoPanel.DOAnchorPosX(0, movementDuration, true))
            .OnComplete(() =>
            {
                EventSystem.current.SetSelectedGameObject(volumeSlider.gameObject);
                UIEventManager.Instance.inControlInputModule.enabled = true;
            });
    }

    public void HideSettings()
    {
        EventSystem.current.SetSelectedGameObject(null);
        
        SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Panel_In);
        
        Sequence sequence = DOTween.Sequence();
            sequence.Insert(0.3f, volumeSlider.DOAnchorPosY(metalSheetPosition.y, movementDuration, true))
            .Insert(0.3f, soundEffectsSlider.DOAnchorPosY(metalSheetPosition.y, movementDuration, true))
            .Insert(0.3f, metalSheet.DOAnchorPosY(metalSheetPosition.y,movementDuration,true))
            .Insert(0f, settingsTitle.DOAnchorPosX(settingsTitlePosition.x,movementDuration,true))
            .Insert(0f, infoPanel.DOAnchorPosX(-settingsTitlePosition.x,movementDuration,true))
            .Insert(0.3f, languageSlider.DOAnchorPosY(metalSheetPosition.y, movementDuration, true)).OnComplete(() =>
            {
                UIEventManager.Instance.inControlInputModule.enabled = false;
                UIEventManager.Instance.ChangeScreen(MenuType.Menu.MainMenu);
                gameObject.SetActive(false);
            });
    }
    
    #endregion
}
