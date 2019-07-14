using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InControl;
using TMPro;
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
    public RectTransform confirmCharacterSelection;

    //Solo pruebas
    public RectTransform poseidonBackground;
    public RectTransform anubisBackground;
    public RectTransform kaliBackground;
    public RectTransform thorBackground;

    public Color confirmedColor;
    public Color deniedColor;
    //Fin pruebas

    public Image fade;
    public List<Image> confirmPlayerIcons;
    public List<GameObject> players;
    public GameObject characterSelectionManager;
    public GameObject titleScreenPanel;

    public TextMeshProUGUI playersConfirmedText;

    public bool poseidonChosen;
    public bool kaliChosen;
    public bool thorChosen;
    public bool anubisChosen;

    public int playersWithGodPicked;
    public int playersConfirmed;
    
    public InControlInputModule inControlInputModule;

    private void Awake()
    {
        poseidonChosen = false;
        kaliChosen = false;
        thorChosen = false;
        anubisChosen = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenCharacterSelection()
    {
        inControlInputModule.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
        
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(raceButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.2f, settingsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, creditsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, mainMenuBackground.DOAnchorPosY(1035, 0.5f, true))
            .Insert(0.4f, mainMenuTitle.DOAnchorPosY(295, 0.5f, true))
            .Insert(0.6f, exitButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, infoPanel.DOAnchorPosY(-100, 0.5f, true))
            .Insert(1f, anubisBackground.DOAnchorPosY(0, 1f, true))
            .Insert(1f, infoPanel.DOAnchorPos(new Vector2(1936, 0), 0.01f, true))
            .Insert(1f, poseidonBackground.DOAnchorPosY(0, 1f, true))
            .Insert(1f, kaliBackground.DOAnchorPosY(0, 1f, true))
            .Insert(1f, thorBackground.DOAnchorPosY(0, 1f, true))
            .Insert(1f, anubisButton.DOAnchorPosY(0, 1f, true))
            .Insert(1f, poseidonButton.DOAnchorPosY(0, 1f, true))
            .Insert(1f, kaliButton.DOAnchorPosY(0, 1f, true))
            .Insert(1f, thorButton.DOAnchorPosY(0, 1f, true))
            .Insert(1.6f, characterSelectionTitle.DOAnchorPosX(0, 0.6f, true))
            .Insert(1.6f, infoPanel.DOAnchorPosX(0, 0.6f, true)).OnComplete(() =>
            {
                characterSelectionManager.SetActive(true);
            });
    }

    public void OpenSettings()
    {
        inControlInputModule.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
        
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(raceButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.2f, settingsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, creditsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, mainMenuTitle.DOAnchorPosY(295, 0.5f, true))
            .Insert(0.6f, exitButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(1.1f,settingsTitle.DOAnchorPos(new Vector2(0,settingsTitle.anchoredPosition.y), 0.5f, true))
            .Insert(1.3f, volumeSlider.DOAnchorPos(new Vector2(0,0), 0.5f, true))
            .Insert(1.5f, soundEffectsSlider.DOAnchorPos(new Vector2(0,0), 0.5f, true))
            .Insert(1.6f, languageSlider.DOAnchorPos(new Vector2(0,0), 0.5f, true)).OnComplete(() =>
            {
                inControlInputModule.enabled = true;
                EventSystem.current.SetSelectedGameObject(volumeSlider.gameObject);
            });
    }

    public void CloseSettings()
    {
        inControlInputModule.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
        
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(settingsTitle.DOAnchorPos(new Vector2(-550, settingsTitle.anchoredPosition.y), 0.5f, true))
            .Insert(0.2f, volumeSlider.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, soundEffectsSlider.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.6f, languageSlider.DOAnchorPosX(-675, 0.5f, true))
            .Insert(1f, raceButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(1.2f, settingsButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(1.4f, creditsButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(1.4f, mainMenuTitle.DOAnchorPosY(0, 0.5f, true))
            .Insert(1.6f, exitButton.DOAnchorPosX(0, 0.5f, true)).OnComplete(() =>
            {
                inControlInputModule.enabled = true;
                EventSystem.current.SetSelectedGameObject(raceButton.gameObject);
            });
    }


    public void ReturnTitleScreen()
    {
        inControlInputModule.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
        
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(raceButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.2f, settingsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, creditsButton.DOAnchorPosX(-675, 0.5f, true))
            .Insert(0.4f, mainMenuBackground.DOAnchorPosY(1035, 0.5f, true))
            .Insert(0.4f, mainMenuTitle.DOAnchorPosY(295, 0.5f, true))
            .Insert(0.4f, infoPanel.DOAnchorPosY(-150,0.5f,true))
            .Insert(0.6f, exitButton.DOAnchorPosX(-675, 0.5f, true)).OnComplete(() =>
            {
                titleScreenPanel.SetActive(true);
            });
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
            .Insert(0f, infoPanel.DOAnchorPosY(-100, 0.6f, true))
            .Insert(0.6f,anubisBackground.DOAnchorPosY(727, 1f, true))
            .Insert(0.6f, poseidonBackground.DOAnchorPosY(727, 1f, true))
            .Insert(0.6f, kaliBackground.DOAnchorPosY(727, 1f, true))
            .Insert(0.6f, thorBackground.DOAnchorPosY(727, 1f, true))
            .Insert(0.6f, anubisButton.DOAnchorPosY(-230, 1f, true))
            .Insert(0.6f, poseidonButton.DOAnchorPosY(-230, 1f, true))
            .Insert(0.6f, kaliButton.DOAnchorPosY(-230, 1f, true))
            .Insert(0.6f, thorButton.DOAnchorPosY(-230, 1f, true));
    }

    public void BuildMainMenu()
    {
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Append(raceButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(0.2f, settingsButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(0.4f, creditsButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(0.4f, mainMenuBackground.DOAnchorPosY(0, 0.5f, true))
            .Insert(0.4f, mainMenuTitle.DOAnchorPosY(0, 0.5f, true))
            .Insert(0.6f, exitButton.DOAnchorPosX(0, 0.5f, true))
            .Insert(0.4f, infoPanel.DOAnchorPosY(0, 0.5f, true));
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

    private void OpenConfirmCharacterSelection()
    {
        EventSystem.current.SetSelectedGameObject(confirmCharacterSelection.gameObject);
    }

    private void CloseConfirmCharacterSelection()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void ResetMainMenu()
    {
        raceButton.gameObject.SetActive(false);

        poseidonButton.gameObject.SetActive(false);
        kaliButton.gameObject.SetActive(false);
        anubisButton.gameObject.SetActive(false);
        thorButton.gameObject.SetActive(false);

        playersWithGodPicked = 0;
        playersConfirmed = 0;

        playersConfirmedText.text = "";
        playersConfirmedText.gameObject.SetActive(false);
        characterSelectionManager.SetActive(false);
    }


    public void RacePressed()
    {
        raceButton.gameObject.SetActive(false);
    }

    //BORRAR
    public void CharacterSelectionPressed(int num)
    {

        poseidonButton.gameObject.SetActive(true);
        kaliButton.gameObject.SetActive(true);
        anubisButton.gameObject.SetActive(true);
        thorButton.gameObject.SetActive(true);
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

            playersConfirmedText.text = playersConfirmed + "/" + RaceManager.Instance.players;

            //REFACTORIZAR
            for (int i = 0; i < RaceManager.Instance.players; i++)
            {
                confirmPlayerIcons[i].gameObject.SetActive(true);
            }
            
            foreach (var player in players)
            {
                if (player.gameObject.activeInHierarchy)
                    player.GetComponent<CharacterSelectionController>().Confirm();
            }

            OpenConfirmCharacterSelection();
        }
    }

    public void ConfirmConfirmation(int playerID)
    {
        playersConfirmed++; 
        
        confirmPlayerIcons[playerID - 1].color = confirmedColor;
        
        playersConfirmedText.text = playersConfirmed + "/" + RaceManager.Instance.players;

        if (playersConfirmed == RaceManager.Instance.players)
        {
            for (int i = 0; i < RaceManager.Instance.players; i++)
            {
                PlayerInfo playerInfo = new PlayerInfo();
                playerInfo.inputDevice = players[i].GetComponent<IncontrolProvider>().InputDevice;
                playerInfo.godType = players[i].GetComponent<CharacterSelectionController>().robogodPicked;
                playerInfo.controlType = players[i].GetComponent<IncontrolProvider>().controlType;
                RaceManager.Instance.playerInfo.Add(playerInfo);
            }

            StartCoroutine(FadeToRace());

        }
    }

    public void DenyConfirmation()
    {
        playersConfirmedText.text = "";
        playersConfirmed = 0;
        
        CloseConfirmCharacterSelection();

        for (int i = 0; i < confirmPlayerIcons.Count; i++)
        {
            confirmPlayerIcons[i].color = deniedColor;
        }

        foreach (var player in players)
        {
            player.GetComponent<CharacterSelectionController>().Disconfirm();
        }
    }

    private IEnumerator FadeToRace()
    {
        yield return new WaitForSeconds(0.5f);
        Tween tween = fade.DOFade(1, 1f);
        yield return new WaitForSeconds(tween.Duration());
        ResetMainMenu();
        SceneManager.LoadScene("Carrera");
    }

}
