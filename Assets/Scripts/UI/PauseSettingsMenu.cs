using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseSettingsMenu : MonoBehaviour
{
    public GameObject volumeSlider;
    public RectTransform pauseSettingsPanel;
    public float scaleDuration;

    private void Awake()
    {
        pauseSettingsPanel.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        OpenSettingsMenu();
    }

    private void OpenSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = volumeSlider;
        RaceUIManager.Instance.inControlInputModule.enabled = false;

        pauseSettingsPanel.DOScale(new Vector3(1, 1, 1), scaleDuration).SetUpdate(true).OnComplete(() =>
        {
            RaceUIManager.Instance.inControlInputModule.enabled = true;
            EventSystem.current.SetSelectedGameObject(volumeSlider.gameObject);
        });
    }

    public void ReturnPauseMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RaceUIManager.Instance.inControlInputModule.enabled = false;

        pauseSettingsPanel.DOScale(new Vector3(0, 0, 0), scaleDuration).SetUpdate(true).OnComplete(() =>
        {
            RaceUIManager.Instance.ChangeRaceEvent(RaceEvents.Race.Pause);
            gameObject.SetActive(false);
        });
    }
}
