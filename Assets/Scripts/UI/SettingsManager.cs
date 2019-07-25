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

    public Vector2 sliderPosition;
    public Vector2 settingsTitlePosition;
    public float movementDuration;

    private void OnEnable()
    {
        BuildSettings();
    }

    private void OnDisable()
    {
        ResetSettingsMenu();
    }

    public void RemoveSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void ResetSettingsMenu()
    {
        volumeSlider.anchoredPosition = new Vector2(sliderPosition.x, 0);
        soundEffectsSlider.anchoredPosition = new Vector2(sliderPosition.x, 0);
        languageSlider.anchoredPosition = new Vector2(sliderPosition.x, 0);
        settingsTitle.anchoredPosition = new Vector2(settingsTitlePosition.x, 0);
    }
    
    #region Tweens

    private void BuildSettings()
    {
        RemoveSelected();
        
        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0f, volumeSlider.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f, soundEffectsSlider.DOAnchorPosX(0, movementDuration, true))
            .Insert(0f,settingsTitle.DOAnchorPosX(0,movementDuration,true))
            .Insert(0f, languageSlider.DOAnchorPosX(0, movementDuration, true))
            .OnComplete(() =>
            {
                EventSystem.current.SetSelectedGameObject(volumeSlider.gameObject);
                UIManager.Instance.inControlInputModule.enabled = true;
            });
    }

    public void HideSettings()
    {
        RemoveSelected();
        
        Sequence sequence = DOTween.Sequence();
            sequence.Insert(0f, volumeSlider.DOAnchorPosY(sliderPosition.y, movementDuration, true))
            .Insert(0f, soundEffectsSlider.DOAnchorPosY(sliderPosition.y, movementDuration, true))
            .Insert(0f, settingsTitle.DOAnchorPosY(settingsTitlePosition.y,movementDuration,true))
            .Insert(0f, languageSlider.DOAnchorPosY(sliderPosition.y, movementDuration, true)).OnComplete(() =>
            {
                UIManager.Instance.inControlInputModule.enabled = false;
                UIManager.Instance.ChangeScreen(MenuType.Menu.MainMenu);
                gameObject.SetActive(false);
            });
    }
    
    #endregion
}
