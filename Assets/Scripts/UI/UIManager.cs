using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameModes;
    public GameObject characterSelection;
    public GameObject characters;
    public GameObject apocalipsisMode;

    public FireBallSkill fireBallSkill;
    public DaggerSkill daggerSkill;
    public HidroCannonSkill hidroCannon;
    public SpartanDefenseSkill spartanDefenseSkill;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //fireBallSkill.Effect();
            //daggerSkill.Effect();
            //hidroCannon.Effect();
            spartanDefenseSkill.Effect();
        }
    }

    public void Pruebas()
    {
        Debug.Log("Entro en pruebas");
    }

    public void GoToCredits()
    {
        Debug.Log("No hay créditos aún");
    }

    public void GoToSettings()
    {
        Debug.Log("No hay opciones aún");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToChooseMode()
    {
        mainMenu.SetActive(false);
        gameModes.SetActive(true);
        EventSystem.current.SetSelectedGameObject(apocalipsisMode);
        
    }

    public void GoToClassicMode()
    {
        gameModes.SetActive(false);
        characterSelection.SetActive(true);
        characters.SetActive(true);
    }

    public void GoToApocalipsisMode()
    {
        gameModes.SetActive(false);
        characterSelection.SetActive(true);
        characters.SetActive(true);
    }

    public void CharacterSelection()
    {
        
    }

    public void ReturnMainMenu()
    {
        gameModes.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ReturnGameModes()
    {
        gameModes.SetActive(true);
        characterSelection.SetActive(false);
    }
}
