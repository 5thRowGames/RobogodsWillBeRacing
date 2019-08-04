using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PauseMenu : Singleton<PauseMenu>
{
    public GameObject continueButton;
    public RectTransform pauseMenuPanel;
    public Image fade;
    public float scaleDuration;
    public float fadeDuration;


    private void Awake()
    {
        pauseMenuPanel.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        OpenPauseMenu();
    }

    private void OpenPauseMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = continueButton;
        RaceUIManager.Instance.inControlInputModule.enabled = false;

        pauseMenuPanel.DOScale(new Vector3(1, 1, 1), scaleDuration).SetUpdate(true).OnComplete(() =>
        {
            RaceUIManager.Instance.inControlInputModule.enabled = true;
            EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
        });
    }

    public void Continue()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.firstSelectedGameObject = null;
        
        pauseMenuPanel.DOScale(new Vector3(0, 0, 0), scaleDuration).SetUpdate(true).OnComplete(() =>
        {
            //Meter la continuación de la carrera
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        });
    }

    public void OpenSettings()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RaceUIManager.Instance.inControlInputModule.enabled = false;

        pauseMenuPanel.DOScale(new Vector3(0, 0, 0), scaleDuration).SetUpdate(true).OnComplete(() =>
        {
            RaceUIManager.Instance.ChangeRaceEvent(RaceEvents.Race.SettingsPause);
            gameObject.SetActive(false);
        });
    }

    public void ExitMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RaceUIManager.Instance.inControlInputModule.enabled = false;

        fade.DOFade(1, fadeDuration).SetUpdate(true).OnComplete(() =>
        {
            //TODO Aquí habría que meter la carga y descarga de escenas
        });
    }
}
