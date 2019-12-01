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

    public List<PowerTrail> powerTrail;
    public List<MyCarController> myCarControllers;
    public List<PlayerCarSoundManager> playerCarSoundManagers;

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
                    positionUIManager.GetComponent<TimeTrial>().enabled = true;
                    positionUIManager.SetActive(true);

                    foreach (var power in powerTrail)
                    {
                        if(power.gameObject.activeInHierarchy)
                            power.Activate();
                    }

                    foreach (var car in myCarControllers)
                    {
                        if(car.gameObject.activeInHierarchy)
                            car.ConnectCar();
                    }

                    foreach (var car in playerCarSoundManagers)
                    {
                        if(car.gameObject.activeInHierarchy)
                            car.ConnectSound();
                    }
                    
                    ConnectDisconnectManager.ConnectCarSoundManager?.Invoke();
                    SoundManager.Instance.PlayLoop(SoundManager.Music.Inicio);
                    countdown = true;
                }
                break;
            
        }
    }
}
