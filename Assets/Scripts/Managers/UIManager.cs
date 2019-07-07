using System;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject raceButton;
    public List<GameObject> players;
    public List<GameObject> lapsButtons;
    public GameObject poseidonButton;
    public GameObject kaliButton;
    public GameObject anubisButton;
    public GameObject thorButton;
    public GameObject characterSelectionManager;

    public Text playersConfirmedText;

    public bool poseidonChosen;
    public bool kaliChosen;
    public bool thorChosen;
    public bool anubisChosen;

    public int playersWithGodPicked;
    public int playersConfirmed;

    private MenuType.MenyType menuType;
    private bool allowInput;

    private void Awake()
    {
        menuType = MenuType.MenyType.TitleScreen;
        poseidonChosen = false;
        kaliChosen = false;
        thorChosen = false;
        anubisChosen = false;
    }

    private void Update()
    {
        InputDevice device = InputManager.ActiveDevice;

        if (allowInput && (device.Action1.IsPressed || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        switch (menuType)
        {
            case MenuType.MenyType.Race:
                
                break;
            
            case MenuType.MenyType.Settings:
                break;
            
            case MenuType.MenyType.Credits:
                break;
            
            case MenuType.MenyType.Laps:
                break;
            
            case MenuType.MenyType.CharacterSelection:
                break;
            
            case MenuType.MenyType.Game:
                break;
            
            case MenuType.MenyType.TitleScreen:
                
                break;
            
            case MenuType.MenyType.Pause:
                break;
        }
    }

    private void GoToMainMenu()
    {
        allowInput = false;
        //Meter secuencia de dotween
        allowInput = true;
    }

    private void ResetMainMenu()
    {
        raceButton.SetActive(false);

        foreach (var player in players)
        {
            player.SetActive(false);
        }

        poseidonButton.SetActive(false);
        kaliButton.SetActive(false);
        anubisButton.SetActive(false);
        thorButton.SetActive(false);

        playersWithGodPicked = 0;
        playersConfirmed = 0;

        foreach (var laps in lapsButtons)
        {
            laps.SetActive(false);
        }

        playersConfirmedText.text = "";
        playersConfirmedText.gameObject.SetActive(false);
        characterSelectionManager.SetActive(false);
    }


    public void RacePressed()
    {
        raceButton.SetActive(false);

        foreach (var lap in lapsButtons)
        {
            lap.SetActive(true);
        }
    }

    public void CharacterSelectionPressed(int num)
    {
        foreach (var lap in lapsButtons)
        {
            lap.SetActive(false);
        }

        poseidonButton.SetActive(true);
        kaliButton.SetActive(true);
        anubisButton.SetActive(true);
        thorButton.SetActive(true);

        foreach (var player in players)
        {
            player.SetActive(true);
        }

        characterSelectionManager.SetActive(true);
    }

    public void ReturnRace()
    {
        raceButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(raceButton);
    }

    public void ChooseCharacter(GodType.RobogodType robogod)
    {
        switch (robogod)
        {
            case GodType.RobogodType.Anubis:

                //Corutina
                anubisChosen = true;
                RaceManager.Instance.anubisIA = false;
                break;
            
            case GodType.RobogodType.Kali:

                //Corutina
                kaliChosen = true;
                RaceManager.Instance.kaliIA = false;
                break;
            
            case GodType.RobogodType.Poseidon:

                //Corutina
                poseidonChosen = true;
                RaceManager.Instance.poseidonIA = false;
                break;
            
            case GodType.RobogodType.Thor:

                //Corutina
                thorChosen = true;
                RaceManager.Instance.thorIA = false;
                break;
        }
        EveryoneHasGod(1);
    }

    public void DeselectCharacter(GodType.RobogodType robogod)
    {
        switch (robogod)
        {
            case GodType.RobogodType.Anubis:
                //Corutina
                anubisChosen = false;
                RaceManager.Instance.anubisIA = true;

                break;
            
            case GodType.RobogodType.Kali:
                //Corutina
                kaliChosen = false;
                RaceManager.Instance.anubisIA = true;

                break;
            
            case GodType.RobogodType.Poseidon:
                //Corutina
                poseidonChosen = false;
                RaceManager.Instance.anubisIA = true;

                break;
            
            case GodType.RobogodType.Thor:
                //Corutina
                thorChosen = false;
                RaceManager.Instance.anubisIA = true;
                
                break;
        }
        EveryoneHasGod(-1);
    }

    public void EveryoneHasGod(int confirm)
    {
        playersWithGodPicked += confirm;

        if (playersWithGodPicked == RaceManager.Instance.players) 
        {
            playersConfirmed = 0;

            playersConfirmedText.text =
                "Jugadores confirmados " + playersConfirmed + "   / " + RaceManager.Instance.players;

            foreach (var player in players)
            {
                if (player.gameObject.activeInHierarchy)
                    player.GetComponent<CharacterSelectionController>().Confirm();
            }
        }
    }

    public void ConfirmConfirmation()
    {
        playersConfirmed++; 

        if (playersConfirmed == RaceManager.Instance.players)
        {

            foreach (var player in players)
            {
                if (player.GetComponent<Image>().enabled)
                {
                    PlayerInfo playerInfo = new PlayerInfo();
                    playerInfo.inputDevice = player.GetComponent<IncontrolProvider>().InputDevice;
                    playerInfo.godType = player.GetComponent<CharacterSelectionController>().robogodPicked;
                    playerInfo.controlType = player.GetComponent<IncontrolProvider>().controlType;
                    RaceManager.Instance.playerInfo.Add(playerInfo);
                }
            }

            ResetMainMenu();
            SceneManager.LoadScene("Carrera");

        }
    }

    public void DenyConfirmation()
    {
        playersConfirmedText.text = "";
        playersConfirmed = 0;

        foreach (var player in players)
        {
            player.GetComponent<CharacterSelectionController>().Disconfirm();
        }
    }


}
