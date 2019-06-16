using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    

    private void Awake()
    {
        poseidonChosen = false;
        kaliChosen = false;
        thorChosen = false;
        anubisChosen = false;
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
                break;
            
            case GodType.RobogodType.Kali:

                //Corutina
                kaliChosen = true;
                break;
            
            case GodType.RobogodType.Poseidon:

                //Corutina
                poseidonChosen = true;
                break;
            
            case GodType.RobogodType.Thor:

                //Corutina
                thorChosen = true;
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

                break;
            
            case GodType.RobogodType.Kali:
                //Corutina
                kaliChosen = false;

                break;
            
            case GodType.RobogodType.Poseidon:
                //Corutina
                poseidonChosen = false;

                break;
            
            case GodType.RobogodType.Thor:
                //Corutina
                thorChosen = false;
                
                break;
        }
        EveryoneHasGod(-1);
    }

    public void EveryoneHasGod(int confirm)
    {
        playersWithGodPicked += confirm;

        Debug.Log(playersWithGodPicked + "    " + RaceManager.Instance.players);

        if (playersWithGodPicked == RaceManager.Instance.players) 
        {
            playersConfirmed = 0;

            foreach (var player in players)
            {
                if (player.gameObject.activeInHierarchy)
                {
                    player.GetComponent<CharacterSelectionController>().Confirm();
                    playersConfirmedText.text = "Players confirmed: " + playersConfirmed + " / " + RaceManager.Instance.players;
                }
            }
        }
    }

    public void ConfirmConfirmation()
    {
        playersConfirmed++;

        playersConfirmedText.text = "Players confirmed: " + playersConfirmed + " / " + (RaceManager.Instance.players);

        if (playersConfirmed == RaceManager.Instance.players)
        {
            //Ir a la carrera y resetear valores
            ResetMainMenu();
            Debug.Log("Acaba de empezar la carrera");
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
