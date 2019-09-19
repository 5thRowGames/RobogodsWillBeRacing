using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PauseMenu : Singleton<PauseMenu>
{
    public GameObject continueButton;
    public RectTransform pauseMenuPanel;
    public Image fade;
    public float scaleDuration;
    public float fadeDuration;

    private AsyncOperation asyncLoadNextScene;

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
        RaceEventManager.Instance.inControlInputModule.enabled = false;

        pauseMenuPanel.DOScale(new Vector3(1, 1, 1), scaleDuration).SetUpdate(true).OnComplete(() =>
        {
            RaceEventManager.Instance.inControlInputModule.enabled = true;
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
            RaceEventManager.Instance.ChangeRaceEvent(RaceEvents.Race.ContinueRace);
            gameObject.SetActive(false);
        });
    }

    public void OpenSettings()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RaceEventManager.Instance.inControlInputModule.enabled = false;

        pauseMenuPanel.DOScale(new Vector3(0, 0, 0), scaleDuration).SetUpdate(true).OnComplete(() =>
        {
            RaceEventManager.Instance.ChangeRaceEvent(RaceEvents.Race.SettingsPause);
            gameObject.SetActive(false);
        });
    }

    public void ExitMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        RaceEventManager.Instance.inControlInputModule.enabled = false;
        
        asyncLoadNextScene = SceneManager.LoadSceneAsync(1);
        asyncLoadNextScene.allowSceneActivation = false;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(pauseMenuPanel.DOScale(new Vector3(0, 0, 0), scaleDuration).SetUpdate(true))
            .Append(fade.DOFade(1, fadeDuration)).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
            asyncLoadNextScene.allowSceneActivation = true;
            gameObject.SetActive(false);
        });
    }
}
