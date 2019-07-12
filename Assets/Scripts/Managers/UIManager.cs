using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    //Buttons
    public RectTransform raceButton;
    public RectTransform settingsButton;
    public RectTransform creditsButton;
    public RectTransform exitButton;
    public RectTransform volumeSlider;
    public RectTransform soundEffectsSlider;
    public RectTransform languageSlider;
    public RectTransform poseidonButton;
    public RectTransform anubisButton;
    public RectTransform thorButton;
    public RectTransform kaliButton;
    public RectTransform mainMenuBackground;
    public RectTransform mainMenuTitle;
    public RectTransform characterSelectionTitle;
    public RectTransform settingsTitle;
    public RectTransform infoPanel;

    [Header("References")]


    //Solo pruebas
    public RectTransform poseidonBackground;
    public RectTransform anubisBackground;
    public RectTransform kaliBackground;
    public RectTransform thorBackground;
    //FinP pruebas

    public List<GameObject> players;
    public List<GameObject> lapsButtons;
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Prueba();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Prueba2();
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

    private void Prueba()
    {
       BuildCharacterSelection();
    }
    
    private void Prueba2()
    {
        RemoveCharacterSelection();
    }

    public void OpenCharacterSelection()
    {
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(raceButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.2f, settingsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, creditsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, mainMenuBackground.DOAnchorPosY(1035, 0.5f, true))
            .Insert(0.4f, mainMenuTitle.DOAnchorPosY(295, 0.5f, true))
            .Insert(0.6f, exitButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(1f,anubisBackground.DOAnchorPos(new Vector2(0, 0), 1f, true))
            .Insert(1f, anubisBackground.DOAnchorPos(new Vector2(0, 0), 1f, true))
            .Insert(1f, poseidonBackground.DOAnchorPos(new Vector2(0, 0), 1f, true))
            .Insert(1f, kaliBackground.DOAnchorPos(new Vector2(0, 0), 1f, true))
            .Insert(1f, thorBackground.DOAnchorPos(new Vector2(0, 0), 1f, true))
            .Insert(1f, anubisButton.DOAnchorPos(new Vector2(0, 0), 1f, true))
            .Insert(1f, poseidonButton.DOAnchorPos(new Vector2(0, 0), 1f, true))
            .Insert(1f, kaliButton.DOAnchorPos(new Vector2(0, 0), 1f, true))
            .Insert(1f, thorButton.DOAnchorPos(new Vector2(0, 0), 1f, true))
            .Insert(1.6f, characterSelectionTitle.DOAnchorPos(new Vector2(0, 0), 0.6f, true))
            .Insert(1.6f, infoPanel.DOAnchorPos(new Vector2(0, 0), 0.6f, true));
    }
    
    private void BuildCharacterSelection()
    {
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(anubisBackground.DOAnchorPosY(0, 1f, true))
            .Insert(0f, poseidonBackground.DOAnchorPosY(0, 1f, true))
            .Insert(0f, kaliBackground.DOAnchorPosY(0, 1f, true))
            .Insert(0f, thorBackground.DOAnchorPosY(0, 1f, true))
            .Insert(0f, anubisButton.DOAnchorPosY(0, 1f, true))
            .Insert(0f, poseidonButton.DOAnchorPosY(0, 1f, true))
            .Insert(0f, kaliButton.DOAnchorPosY(0, 1f, true))
            .Insert(0f, thorButton.DOAnchorPosY(0, 1f, true))
            .Insert(0.6f, characterSelectionTitle.DOAnchorPosX(0, 0.6f, true))
            .Insert(0.6f, infoPanel.DOAnchorPosX(0, 0.6f, true));
    }

    private void RemoveCharacterSelection()
    {
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Insert(0f, characterSelectionTitle.DOAnchorPosX(-1926, 0.6f, true))
            .Insert(0f, infoPanel.DOAnchorPosX(1936, 0.6f, true))
            .Insert(0.6f,anubisBackground.DOAnchorPosY(727, 1f, true))
            .Insert(0.6f, poseidonBackground.DOAnchorPosY(727, 1f, true))
            .Insert(0.6f, kaliBackground.DOAnchorPosY(727, 1f, true))
            .Insert(0.6f, thorBackground.DOAnchorPosY(727, 1f, true))
            .Insert(0.6f, anubisButton.DOAnchorPosY(-230, 1f, true))
            .Insert(0.6f, poseidonButton.DOAnchorPosY(-230, 1f, true))
            .Insert(0.6f, kaliButton.DOAnchorPosY(-230, 1f, true))
            .Insert(0.6f, thorButton.DOAnchorPosY(-230, 1f, true));
    }

    private void BuildMainMenu()
    {
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(raceButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(0.2f, settingsButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(0.4f, creditsButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(0.4f, mainMenuBackground.DOAnchorPosY(0, 0.5f, true))
            .Insert(0.4f, mainMenuTitle.DOAnchorPosY(0, 0.5f, true))
            .Insert(0.6f, exitButton.DOAnchorPosX(0, 0.5f, true));
    }

    private void RemoveMainMenu()
    {
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(raceButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.2f, settingsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, creditsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, mainMenuBackground.DOAnchorPosY(1035, 0.5f, true))
            .Insert(0.4f, mainMenuTitle.DOAnchorPosY(295, 0.5f, true))
            .Insert(0.6f, exitButton.DOAnchorPosX(-675, 0.5f, true));
    }

    private void BuildSettings()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsTitle.DOAnchorPos(new Vector2(0,settingsTitle.anchoredPosition.y), 0.5f, true))
            .Insert(0.2f, volumeSlider.DOAnchorPos(new Vector2(0,0), 0.5f, true))
            .Insert(0.4f, soundEffectsSlider.DOAnchorPos(new Vector2(0,0), 0.5f, true))
            .Insert(0.6f, languageSlider.DOAnchorPos(new Vector2(0,0), 0.5f, true));
    }

    private void RemoveSettings()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(settingsTitle.DOAnchorPos(new Vector2(-550, settingsTitle.anchoredPosition.y), 0.5f, true))
            .Insert(0.2f, volumeSlider.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, soundEffectsSlider.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.6f, languageSlider.DOAnchorPosX(-675, 0.5f, true));
    }

    private void GoToMainMenu()
    {
        allowInput = false;
        //Meter secuencia de dotween
        allowInput = true;
    }

    private void ResetMainMenu()
    {
        raceButton.gameObject.SetActive(false);

        foreach (var player in players)
        {
            player.SetActive(false);
        }

        poseidonButton.gameObject.SetActive(false);
        kaliButton.gameObject.SetActive(false);
        anubisButton.gameObject.SetActive(false);
        thorButton.gameObject.SetActive(false);

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
        raceButton.gameObject.SetActive(false);

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

        poseidonButton.gameObject.SetActive(true);
        kaliButton.gameObject.SetActive(true);
        anubisButton.gameObject.SetActive(true);
        thorButton.gameObject.SetActive(true);

        foreach (var player in players)
        {
            player.SetActive(true);
        }

        characterSelectionManager.SetActive(true);
    }

    public void ReturnRace()
    {
        raceButton.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(raceButton.gameObject);
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
