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
   public List<Sprite> lightOff;
   public List<Sprite> lightOn;
   public List<Image> confirmPlayerIcons;
   public List<GameObject> players;

   public TextMeshProUGUI playersNumberConfirmedText;

   public bool poseidonChosen;
   public bool kaliChosen;
   public bool thorChosen;
   public bool anubisChosen;

   public GameObject anubis;
   public GameObject poseidon;
   public GameObject kali;
   public GameObject thor;

   [Header ("Debug")]
   public int playersWithGodPicked;
   public int playersConfirmed;

   private void OnEnable()
   {
       poseidonChosen = false;
       kaliChosen = false;
       thorChosen = false;
       anubisChosen = false;
       playersWithGodPicked = 0;
       playersConfirmed = 0;
       StoreGodInfo.Instance.Reset();
       ResetCharacterSelectionUI();
       BuildCharacterSelection();
   }

   #region Functionality

   public void ChooseCharacter(God.Type robogod)
    {
        switch (robogod)
        {
            case God.Type.Anubis:
                
                anubis.GetComponent<HologramToNormal>().TransformIntoNormal();
                anubisChosen = true;
                StoreGodInfo.Instance.anubisIA = false;
                break;
            
            case God.Type.Kali:
                
                kali.GetComponent<HologramToNormal>().TransformIntoNormal();
                kaliChosen = true;
                StoreGodInfo.Instance.kaliIA = false;
                break;
            
            case God.Type.Poseidon:
                
                poseidon.GetComponent<HologramToNormal>().TransformIntoNormal();
                poseidonChosen = true;
                StoreGodInfo.Instance.poseidonIA = false;
                break;
            
            case God.Type.Thor:

                thor.GetComponent<HologramToNormal>().TransformIntoNormal();
                thorChosen = true;
                StoreGodInfo.Instance.thorIA = false;
                break;
        }
        EveryoneHasGod(1);
    }

    public void DeselectCharacter(God.Type robogod)
    {
        switch (robogod)
        {
            case God.Type.Anubis:
                
                anubis.GetComponent<HologramToNormal>().TransformIntoHologram();
                anubisChosen = false;
                StoreGodInfo.Instance.anubisIA = true;

                break;
            
            case God.Type.Kali:
                
                kali.GetComponent<HologramToNormal>().TransformIntoHologram();
                kaliChosen = false;
                StoreGodInfo.Instance.anubisIA = true;

                break;
            
            case God.Type.Poseidon:
                
                poseidon.GetComponent<HologramToNormal>().TransformIntoHologram();
                poseidonChosen = false;
                StoreGodInfo.Instance.anubisIA = true;

                break;
            
            case God.Type.Thor:
                
                thor.GetComponent<HologramToNormal>().TransformIntoHologram();
                thorChosen = false;
                StoreGodInfo.Instance.anubisIA = true;
                
                break;
        }
        EveryoneHasGod(-1);
    }

    public void EveryoneHasGod(int confirm)
    {
        playersWithGodPicked += confirm;

        if (playersWithGodPicked == StoreGodInfo.Instance.players) 
        {
            playersConfirmed = 0;

            playersNumberConfirmedText.text = playersConfirmed + "/" + StoreGodInfo.Instance.players;

            //REFACTORIZAR
            for (int i = 0; i < StoreGodInfo.Instance.players; i++)
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

    public void ConfirmConfirmation(int player)
    {
        playersConfirmed++;

        confirmPlayerIcons[player].sprite = lightOn[player];
        
        playersNumberConfirmedText.text = playersConfirmed + "/" + StoreGodInfo.Instance.players;

        if (playersConfirmed == StoreGodInfo.Instance.players)
        {
            for (int i = 0; i < StoreGodInfo.Instance.players; i++)
            {
                PlayerInfo playerInfo = new PlayerInfo();
                playerInfo.inputDevice = players[i].GetComponent<IncontrolProvider>().InputDevice;
                playerInfo.godType = players[i].GetComponent<CharacterSelectionController>().robogodPicked;
                playerInfo.controlType = players[i].GetComponent<IncontrolProvider>().controlType;
                playerInfo.playerID = players[i].GetComponent<IncontrolProvider>().playerID;
                StoreGodInfo.Instance.playerInfo.Add(playerInfo);
            }

            StartRaceTween();

        }
    }

    public void DenyConfirmation()
    {
        playersNumberConfirmedText.text = "";
        playersConfirmed = 0;
        
        EventSystem.current.SetSelectedGameObject(null);

        ResetConfirCharacterSelectionLights();

        foreach (var player in players)
        {
            player.GetComponent<CharacterSelectionController>().Disconfirm();
        }
    }
   
   #endregion
   
   #region Tweens

   private void StartRaceTween()
   {
       SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Conversion_Out);
       EventSystem.current.SetSelectedGameObject(null);
       EventSystem.current.firstSelectedGameObject = null;

       //Sonido de cerrar el menú
       SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Start_In);
       
       //Se para la música de la UI
       SoundManager.Instance.StopLoop(SoundManager.Music.UI);
       
       fade.DOFade(1, 0.5f).OnComplete(() =>
       {
           UIEventManager.Instance.ChangeScreen(MenuType.Menu.LoadingScreen);
           gameObject.SetActive(false);
       });
   }

   private void BuildCharacterSelection()
   {
      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.firstSelectedGameObject = null;
      
      SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Transicion_Holograma);
      
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
          .Insert(0.6f, infoPanel.DOAnchorPosX(0, 0.6f, true)).OnComplete(() =>
          {
              SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Holograma_Base_In);
              SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Holograma_Up_In);
          });
   }

   //TODO
   //Proceso contrario a la construcción con matices
   public void ReturnMainMenuTween()
   {
       ConnectDisconnectManager.DisconnectCarControllerDelegate();
       EventSystem.current.SetSelectedGameObject(null);
       EventSystem.current.firstSelectedGameObject = null;
       
       SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Back);
       
       Sequence tweenSequence = DOTween.Sequence();
       tweenSequence.Append(infoPanel.DOAnchorPosX(panelPositionX, 0.6f, true))
           .Insert(0f, characterSelectionTitlePanel.DOAnchorPosX(-panelPositionX, 0.6f, true))
           .Insert(0.6f, poseidonBackground.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, anubisBackground.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, kaliBackground.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, thorBackground.DOAnchorPosY(godPositionY, 1f, true))
           .Insert(0.6f, poseidonButton.DOAnchorPosY(-godPositionY, 1f, true))
           .Insert(0.6f, kaliButton.DOAnchorPosY(-godPositionY, 1f, true))
           .Insert(0.6f, thorButton.DOAnchorPosY(-godPositionY, 1f, true))
           .Insert(0.6f, anubisButton.DOAnchorPosY(-godPositionY, 1f, true)).OnComplete(() =>
           {
               UIEventManager.Instance.ChangeScreen(MenuType.Menu.MainMenu);
               gameObject.SetActive(false);
           });
   }

   public void ResetCharacterSelectionUI()
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

   private void ResetConfirCharacterSelectionLights()
   {
       for (int i = 0; i < confirmPlayerIcons.Count; i++)
       {
           confirmPlayerIcons[i].sprite = lightOff[i];
       }
   }
   
   #endregion
}
