using System;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using InControl;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

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
   public Image background;

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
   [SerializeField] private int playersWithGodPicked;
   [SerializeField] private int playersConfirmed;

   private bool returnMainMenu;

   private void OnEnable()
   {
       confirmCharacterSelectionPanel.localScale = Vector3.zero;
       anubis.GetComponent<HologramToNormal>().Reset();
       poseidon.GetComponent<HologramToNormal>().Reset();
       
       foreach (Transform child in kali.transform)
       {
           child.GetComponent<HologramToNormal>().Reset();
       }
       
       thor.GetComponent<HologramToNormal>().Reset();
       StoreGodInfo.Instance.anubisIA = true;
       StoreGodInfo.Instance.poseidonIA = true;
       StoreGodInfo.Instance.kaliIA = true;
       StoreGodInfo.Instance.thorIA = true;
       poseidonChosen = false;
       kaliChosen = false;
       thorChosen = false;
       anubisChosen = false;
       playersWithGodPicked = 0;
       playersConfirmed = 0;
       returnMainMenu = false;
       StoreGodInfo.Instance.Reset();
       ResetCharacterSelectionUI();
       BuildCharacterSelection();
   }

   private void Update()
   {
       if ((InputManager.ActiveDevice.Action4.WasPressed || Input.GetKeyDown(KeyCode.Alpha0)) && !returnMainMenu)
       {
           returnMainMenu = true;
           SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Cambio_Volumen_In);
           ReturnMainMenuTween();
       }
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

                foreach (Transform child in kali.transform)
                {
                    child.GetComponent<HologramToNormal>().TransformIntoNormal();
                }
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
                
                foreach (Transform child in kali.transform)
                {
                    child.GetComponent<HologramToNormal>().TransformIntoHologram();
                }
                kaliChosen = false;
                StoreGodInfo.Instance.kaliIA = true;

                break;
            
            case God.Type.Poseidon:
                
                poseidon.GetComponent<HologramToNormal>().TransformIntoHologram();
                poseidonChosen = false;
                StoreGodInfo.Instance.poseidonIA= true;

                break;
            
            case God.Type.Thor:
                
                thor.GetComponent<HologramToNormal>().TransformIntoHologram();
                thorChosen = false;
                StoreGodInfo.Instance.thorIA = true;
                
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
            PlayerSelectionManager.Instance.canJoin = false;

            playersNumberConfirmedText.text = playersConfirmed + "/" + StoreGodInfo.Instance.players;
            
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
            confirmCharacterSelectionPanel.DOScale(Vector3.one, 0.4f).SetUpdate(true);
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
                playerInfo.inputDevice = players[i].GetComponent<IncontrolProvider>().inputDevice;
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
        PlayerSelectionManager.Instance.canJoin = true;

        foreach (var player in players)
        {
            player.GetComponent<CharacterSelectionController>().Disconfirm();
        }
        
        confirmCharacterSelectionPanel.DOScale(Vector3.zero, 0.4f).SetUpdate(true);
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
       SoundManager.Instance.StopLoop();

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
          .Insert(0f, background.DOFade(1,0.6f))
          .Insert(0.6f, characterSelectionTitlePanel.DOAnchorPosX(0, 0.6f, true))
          .Insert(0.6f, infoPanel.DOAnchorPosX(0, 0.6f, true)).OnComplete(() =>
          {
              SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Holograma_Base_In);
              SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Holograma_Up_In);
          });
   }
   
   //Proceso contrario a la construcción con matices
   public void ReturnMainMenuTween()
   {
       EventSystem.current.SetSelectedGameObject(null);
       EventSystem.current.firstSelectedGameObject = null;

       SoundManager.Instance.PlayFx(SoundManager.Fx.UI_Back);

       confirmCharacterSelectionPanel.DOScale(Vector3.zero, 0.4f);
       
       Sequence tweenSequence = DOTween.Sequence();
       tweenSequence.Append(infoPanel.DOAnchorPosX(panelPositionX, 0.6f, true))
           .Insert(0f, characterSelectionTitlePanel.DOAnchorPosX(-panelPositionX, 0.6f, true))
           .Insert(0.6f, background.DOFade(0, 1f))
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
       background.color = new Vector4(0, 0, 0, 0);
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
