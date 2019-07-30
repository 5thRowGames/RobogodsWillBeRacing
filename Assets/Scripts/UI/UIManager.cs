using InControl;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public InControlInputModule inControlInputModule;
    
    [Header("Menu principal")]
    public GameObject titleScreen;
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject credits;
    public GameObject characterSelection;
    public GameObject loadingScreen;

    [Header("Carrera")] 
    public GameObject pauseMenu;
    public GameObject startRace;
    public GameObject awakeRace;
    public GameObject fisnishRace;
    

    public void ChangeScreen(MenuType.Menu menuType)
    {
        switch (menuType)
        {
            case MenuType.Menu.TitleScreen:
                titleScreen.SetActive(true);
                break;
            
            case MenuType.Menu.MainMenu:
                mainMenu.SetActive(true);
                break;
            
            case MenuType.Menu.Settings:
                settings.SetActive(true);
                break;
            
            case MenuType.Menu.Credits:
                credits.SetActive(true);
                break;
            
            case MenuType.Menu.CharacterSelection:
                characterSelection.SetActive(true);
                break;
            
            case MenuType.Menu.Pause:
                pauseMenu.SetActive(true);
                break;
            
            case MenuType.Menu.LoadingScreen:
                loadingScreen.SetActive(true);
                break;
        }
    }

    public void ChangeRaceEvent(RaceEvents.Race raceEvent)
    {
        switch (raceEvent)
        {
            case RaceEvents.Race.Awake:
                break;
            
            case RaceEvents.Race.Start:
                break;
            
            case RaceEvents.Race.Finish:
                break;
            
            case RaceEvents.Race.Pause:
                break;
            
            case RaceEvents.Race.SplitScreen:
                break;
        }
    }
}
