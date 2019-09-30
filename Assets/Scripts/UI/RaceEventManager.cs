using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class RaceEventManager : Singleton<RaceEventManager>
{
    public InControlInputModule inControlInputModule;

    [Header("Carrera")]
    public OpenPauseMenu openPauseMenu;
    public GameObject pauseMenu;
    public GameObject startRace;
    public GameObject awakeRace;
    public GameObject finishRace;
    public GameObject pauseSettingsMenu;

    private bool countdown;

    public void ChangeRaceEvent(RaceEvents.Race raceEvent)
    {
        switch (raceEvent)
        {
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
                    ConnectDisconnectManager.InitCamera();
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
