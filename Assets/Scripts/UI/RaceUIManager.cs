using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class RaceUIManager : Singleton<RaceUIManager>
{
    public InControlInputModule inControlInputModule;
    
    [Header("Carrera")] 
    public GameObject pauseMenu;
    public GameObject startRace;
    public GameObject awakeRace;
    public GameObject finishRace;
    public GameObject pauseSettingsMenu;

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
        }
    }
}
