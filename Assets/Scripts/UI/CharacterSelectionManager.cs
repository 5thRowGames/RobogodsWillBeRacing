using System;
using System.Collections.Generic;
using DG.Tweening;
using InControl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectionManager : Singleton<CharacterSelectionManager>
{
   public RectTransform poseidonButton;
   public RectTransform anubisButton;
   public RectTransform thorButton;
   public RectTransform kaliButton;
   
   public RectTransform poseidonBackground;
   public RectTransform anubisBackground;
   public RectTransform kaliBackground;
   public RectTransform thorBackground;
   
   public RectTransform infoPanel;
   public RectTransform confirmCharacterSelectionPanel;
   public RectTransform characterSelectionTitlePanel;

   public float godPositionY;
   public float panelPositionX;
   public float buttonPositionY;
   
   public Image fade;
   public List<Image> confirmPlayerIcons;
   public List<GameObject> players;

   public TextMeshProUGUI playersNumberConfirmedText;

   public bool poseidonChosen;
   public bool kaliChosen;
   public bool thorChosen;
   public bool anubisChosen;

   [Header ("Debug")]
   public int playersWithGodPicked;
   public int playersConfirmed;

   private void Awake()
   {
       poseidonChosen = false;
       kaliChosen = false;
       thorChosen = false;
       anubisChosen = false;
   }

   private void OnEnable()
   {
      ResetCharacterSelection();
      BuildCharacterSelection();
   }

   #region Functionality
   
   public void ChooseCharacter(God.Type robogod)
    {
        switch (robogod)
        {
            case God.Type.Anubis:

                //Corutina
                anubisChosen = true;
                RaceManager.Instance.anubisIA = false;
                break;
            
            case God.Type.Kali:

                //Corutina
                kaliChosen = true;
                RaceManager.Instance.kaliIA = false;
                break;
            
            case God.Type.Poseidon:

                //Corutina
                poseidonChosen = true;
                RaceManager.Instance.poseidonIA = false;
                break;
            
            case God.Type.Thor:

                //Corutina
                thorChosen = true;
                RaceManager.Instance.thorIA = false;
                break;
        }
        EveryoneHasGod(1);
    }

    public void DeselectCharacter(God.Type robogod)
    {
        switch (robogod)
        {
            case God.Type.Anubis:
                //Corutina
                anubisChosen = false;
                RaceManager.Instance.anubisIA = true;

                break;
            
            case God.Type.Kali:
                //Corutina
                kaliChosen = false;
                RaceManager.Instance.anubisIA = true;

                break;
            
            case God.Type.Poseidon:
                //Corutina
                poseidonChosen = false;
                RaceManager.Instance.anubisIA = true;

                break;
            
            case God.Type.Thor:
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

            playersNumberConfirmedText.text = playersConfirmed + "/" + RaceManager.Instance.players;

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

            EventSystem.current.SetSelectedGameObject(confirmCharacterSelectionPanel.gameObject);
        }
    }

    public void ConfirmConfirmation(int playerID)
    {
        playersConfirmed++; 
        
        //confirmPlayerIcons[playerID - 1].color = confirmedColor;
        
        playersNumberConfirmedText.text = playersConfirmed + "/" + RaceManager.Instance.players;

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

            //StartCoroutine(FadeToRace());

        }
    }

    public void DenyConfirmation()
    {
        playersNumberConfirmedText.text = "";
        playersConfirmed = 0;
        
        EventSystem.current.SetSelectedGameObject(null);

        /*for (int i = 0; i < confirmPlayerIcons.Count; i++)
        {
            confirmPlayerIcons[i].color = deniedColor;
        }*/

        foreach (var player in players)
        {
            player.GetComponent<CharacterSelectionController>().Disconfirm();
        }
    }
   
   #endregion
   
   #region Tweens

   private void BuildCharacterSelection()
   {
      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.firstSelectedGameObject = null;
      
      Sequence tweenSequence = DOTween.Sequence();
      tweenSequence.Append(anubisBackground.DOAnchorPosY(0, 1f, true))
         .Insert(0f, poseidonBackground.DOAnchorPosY(0, 1f, true))
         .Insert(0f, kaliBackground.DOAnchorPosY(0, 1f, true))
         .Insert(0f, thorBackground.DOAnchorPosY(0, 1f, true))
         .Insert(0f, anubisButton.DOAnchorPosY(0, 1f, true))
         .Insert(0f, poseidonButton.DOAnchorPosY(0, 1f, true))
         .Insert(0f, kaliButton.DOAnchorPosY(0, 1f, true))
         .Insert(0f, thorButton.DOAnchorPosY(0, 1f, true))
         .Insert(0.6f, characterSelectionTitlePanel.DOAnchorPosX(0, 0.6f, true))
         .Insert(0.6f, infoPanel.DOAnchorPosX(0, 0.6f, true));
   }

   //TODO
   //Proceso contrario a la construcción con matices
   public void HideCharacterSelection()
   {
       
       Sequence tweenSequence = DOTween.Sequence();
       tweenSequence.Append(infoPanel.DOAnchorPosX(panelPositionX, 0.6f, true))
           .Insert(0f, characterSelectionTitlePanel.DOAnchorPosX(-panelPositionX, 0.6f, true))
           .Insert(0.6f, poseidonBackground.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, anubisBackground.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, kaliBackground.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, thorBackground.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, poseidonButton.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, kaliButton.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, thorButton.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, anubisButton.DOAnchorPosY(godPositionY, 1f, true)).OnComplete(() =>
           {
               UIManager.Instance.ChangeScreen(MenuType.Menu.MainMenu);
               gameObject.SetActive(false);
           });
   }

   public void ResetCharacterSelection()
   {
       anubisBackground.anchoredPosition = new Vector2(0, godPositionY);
       poseidonBackground.anchoredPosition = new Vector2(0, godPositionY);
       thorBackground.anchoredPosition = new Vector2(0, godPositionY);
       kaliBackground.anchoredPosition = new Vector2(0, godPositionY);
       poseidonButton.anchoredPosition = new Vector2(0,buttonPositionY);
       anubisButton.anchoredPosition = new Vector2(0,buttonPositionY);
       kaliButton.anchoredPosition = new Vector2(0,buttonPositionY);
       thorButton.anchoredPosition = new Vector2(0,buttonPositionY);
       infoPanel.anchoredPosition = new Vector2(panelPositionX, 0);
       characterSelectionTitlePanel.anchoredPosition = new Vector2(-panelPositionX, 0);
   }
   
   #endregion
}
