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

            case MenuType.Menu.LoadingScreen:
                loadingScreen.SetActive(true);
                break;
        }
    }
}
