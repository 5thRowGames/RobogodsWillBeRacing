using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class RaceEventManager : SingletonDontDestroy<RaceEventManager>
{
    public List<RacingCamera> racingCameraList;
    public GameObject positionUIManager;
    public InControlInputModule inControlInputModule;

    [Header("Carrera")]
    public OpenPauseMenu openPauseMenu;
    public GameObject pauseMenu;
    public GameObject startRace;
    public GameObject awakeRace;
    public GameObject finishRace;
    public GameObject pauseSettingsMenu;
    public GameObject loadingScreen;

    private bool countdown;

    public bool Countdown
    {
        get => countdown;
    }

    public void ChangeRaceEvent(RaceEvents.Race raceEvent)
    {
        switch (raceEvent)
        {
            case RaceEvents.Race.LoadingScreen:
                loadingScreen.SetActive(true);
                break;
            
            case RaceEvents.Race.Awake:
                awakeRace.SetActive(true);
                break;
            
            case RaceEvents.Race.Start:
                startRace.SetActive(true);
                break;
            
            case RaceEvents.Race.Finish:
                finishRace.SetActive(true);
                break;
            
            case RaceEvents.Race.Pause:
                pauseMenu.SetActive(true);
                break;
            
            case RaceEvents.Race.SettingsPause:
                pauseSettingsMenu.SetActive(true);
                break;
            
            case RaceEvents.Race.ContinueRace:

                openPauseMenu.enabled = true;
                break;
                
            case RaceEvents.Race.CountdownFinished:

                if (!countdown)
                {
                    if (StoreGodInfo.Instance.players > 1)
                    {
                        positionUIManager.GetComponent<PositionUIManager>().enabled = true;
                        positionUIManager.GetComponent<TimeTrial>().enabled = false;
                    }
                    else
                    {
                        positionUIManager.GetComponent<PositionUIManager>().enabled = false;
                        positionUIManager.GetComponent<TimeTrial>().enabled = true;
                    }
                    
                    positionUIManager.SetActive(true);
                    ConnectDisconnectManager.ConnectCarControllerDelegate();
                    ConnectDisconnectManager.ConnectItemManagerDelegate();
                    ConnectDisconnectManager.ConnectSkillManagerDelegate(); 
                    ConnectDisconnectManager.ConnectCarSoundManager();
                    SoundManager.Instance.PlayLoop(SoundManager.Music.Inicio);

                    countdown = true;
                }
                break;
            
        }
    }
}
