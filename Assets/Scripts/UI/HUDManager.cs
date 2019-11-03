using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HUDInfo
{
    public God.Type god;
    public Image racePositionImage;
    public Image lapText;
    public Image manaBar;
    public Image mainSkillIcon;
    public Image turboBar;
    public GameObject turbo;
    public Sprite mainSkillEnabledSprite;
    public Sprite mainSkillDisabledSprite;
    public TextMeshProUGUI secondarySkillTime;
    public Image secondarySkillIcon;
    public Sprite secondarySkillEnabledSprite;
    public Sprite secondarySkillDisabledSprite;
    public Image currentItem;
    public TextMeshProUGUI timeTrial;
    public GameObject minimap;
    public GameObject laps;
    public Image flash;
    public ParticleSystem flashParticles;
    public Camera mainCamera;

    private float time;

    public float Time
    {
        get => time;
        set => time = value;
    }
}

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField] private float flashIncrement;
    [SerializeField] private float flashTime;
    public List<Sprite> numbers;
    public List<Sprite> positionImage;
    public List<HUDInfo> hudInfo;
    public Dictionary<God.Type, HUDInfo> hudDictionary; //0 anubis, 1 poseidon 2 kali 3 thor

    public Action UpdatePositionUI;

    private God.Type godTimeTrial;

    private void Awake()
    {
        godTimeTrial = God.Type.None;
        
        hudDictionary = new Dictionary<God.Type, HUDInfo>();
        
        foreach (var hud in hudInfo)
        {
            hudDictionary.Add(hud.god, hud);
        }

        //IA y numero de players
        if (!StoreGodInfo.Instance.anubisIA)
        {
            if (StoreGodInfo.Instance.players > 1)
            {
                UpdatePositionUI += UpdateAnubisPosition;
                hudDictionary[God.Type.Anubis].timeTrial.gameObject.SetActive(false);
                hudDictionary[God.Type.Anubis].racePositionImage.gameObject.SetActive(true);
            }
            else
            {
                godTimeTrial = God.Type.Anubis;
                hudDictionary[God.Type.Anubis].timeTrial.gameObject.SetActive(true);
                hudDictionary[God.Type.Anubis].racePositionImage.gameObject.SetActive(false);
            }
        }


        if (!StoreGodInfo.Instance.poseidonIA)
        {
            if (StoreGodInfo.Instance.players > 1)
            {
                UpdatePositionUI += UpdatePoseidonPosition;
                hudDictionary[God.Type.Poseidon].timeTrial.gameObject.SetActive(false);
                hudDictionary[God.Type.Poseidon].racePositionImage.gameObject.SetActive(true);
            }
            else
            {
                godTimeTrial = God.Type.Poseidon;
                hudDictionary[God.Type.Poseidon].timeTrial.gameObject.SetActive(true);
                hudDictionary[God.Type.Poseidon].racePositionImage.gameObject.SetActive(false);
            }
        }

        if (!StoreGodInfo.Instance.kaliIA)
        {
            if (StoreGodInfo.Instance.players > 1)
            {
                UpdatePositionUI += UpdateKaliPosition;
                hudDictionary[God.Type.Kali].timeTrial.gameObject.SetActive(false);
                hudDictionary[God.Type.Kali].racePositionImage.gameObject.SetActive(true);
            }
            else
            {
                godTimeTrial = God.Type.Kali;
                hudDictionary[God.Type.Kali].timeTrial.gameObject.SetActive(true);
                hudDictionary[God.Type.Kali].racePositionImage.gameObject.SetActive(false);
            }
        }

        if (!StoreGodInfo.Instance.thorIA)
        {
            if (StoreGodInfo.Instance.players > 1)
            {
                UpdatePositionUI += UpdateThorPosition;
                hudDictionary[God.Type.Thor].timeTrial.gameObject.SetActive(false);
                hudDictionary[God.Type.Thor].racePositionImage.gameObject.SetActive(true);
            }
            else
            {
                godTimeTrial = God.Type.Thor;
                hudDictionary[God.Type.Thor].timeTrial.gameObject.SetActive(true);
                hudDictionary[God.Type.Thor].racePositionImage.gameObject.SetActive(false);
            }
        }
           
    }

    private void OnDisable()
    {
        UpdatePositionUI -= UpdateAnubisPosition;
        UpdatePositionUI -= UpdatePoseidonPosition;
        UpdatePositionUI -= UpdateKaliPosition;
        UpdatePositionUI -= UpdateThorPosition;
    }

    private void Update()
    {
        if (StoreGodInfo.Instance.players == 1)
        {
            hudDictionary[godTimeTrial].timeTrial.text = String.Format("{0:00}",TimeTrial.Instance.seconds);
        }
    }

    //Se han vuelto privados al no ser usados por ahora
    private float chooseItemTimer;
    private List<Sprite> itemIcon;

    public IEnumerator ChooseItemUI(God.Type god,int itemChosen)
    {
        float timer = chooseItemTimer;
        int i = 0;
        int total = itemIcon.Count;

        while (timer > 0)
        {
            hudDictionary[god].currentItem.sprite = itemIcon[i];
            yield return new WaitForSeconds(0.1f);
            timer -= 0.1f;
            i++;

            if (i == total)
                i = 0;
        }

        hudDictionary[god].currentItem.sprite = itemIcon[itemChosen];
    }

    public void RemoveCurrentItem(God.Type god)
    {
        hudDictionary[god].currentItem.sprite = null;
    }

    public void UpdateManaBar(God.Type god, int amount)
    {
        hudDictionary[god].manaBar.fillAmount = Mathf.Clamp((hudDictionary[god].manaBar.fillAmount + (amount/100f)), 0, 1);

        if (hudDictionary[god].manaBar.fillAmount == 1)
        {
            hudDictionary[god].mainSkillIcon.sprite = hudDictionary[god].mainSkillEnabledSprite;
        }
        else
        {
            hudDictionary[god].mainSkillIcon.sprite = hudDictionary[god].mainSkillDisabledSprite;
        }
    }

    public void StartGodSkillTimer(God.Type god,float time)
    {
        StartCoroutine(Timer(god, time));
    }

    public void ReduceTime(God.Type god, float amount)
    {
        hudDictionary[god].Time -= amount;
    }

    IEnumerator Timer(God.Type god,float time)
    {
        hudDictionary[god].secondarySkillIcon.sprite = hudDictionary[god].secondarySkillDisabledSprite;
        TextMeshProUGUI text = hudDictionary[god].secondarySkillTime;

        hudDictionary[god].Time = time;

        while (hudDictionary[god].Time > 0)
        {
            hudDictionary[god].Time -= Time.deltaTime;
            text.text = String.Format("{0:00}",hudDictionary[god].Time);
            yield return null;
        }
        
        hudDictionary[god].secondarySkillIcon.sprite = hudDictionary[god].secondarySkillEnabledSprite;
        text.text = "";
    }

    public void UpdateLapText(God.Type god, int lap)
    {
        hudDictionary[god].lapText.sprite = numbers[lap - 1];
    }

    private void UpdateAnubisPosition()
    {
        hudDictionary[God.Type.Anubis].racePositionImage.sprite = positionImage[LapsManager.Instance.racePosition[(int) God.Type.Anubis]];
    }
    
    private void UpdatePoseidonPosition()
    {
        hudDictionary[God.Type.Poseidon].racePositionImage.sprite = positionImage[LapsManager.Instance.racePosition[(int) God.Type.Poseidon]];
    }
    
    private void UpdateKaliPosition()
    {
        hudDictionary[God.Type.Kali].racePositionImage.sprite = positionImage[LapsManager.Instance.racePosition[(int) God.Type.Kali]];
    }
    
    private void UpdateThorPosition()
    {
        hudDictionary[God.Type.Thor].racePositionImage.sprite = positionImage[LapsManager.Instance.racePosition[(int) God.Type.Thor]];
    }

    public void FlashScreen(God.Type god, Color color)
    {
        StartCoroutine(FlashScreenCoroutine(god,hudDictionary[god].flash, hudDictionary[god].flashParticles,color, hudDictionary[god].mainCamera));
    }

    IEnumerator FlashScreenCoroutine(God.Type god,Image image, ParticleSystem particleSystem,Color color, Camera camera)
    {
        image.color = color;

        int oldMask = camera.cullingMask;
        
        DisableOrEnableUI(god,false);
        
        camera.cullingMask |= 1 << LayerMask.NameToLayer("PortalParticles");

        Color particleSystemColor = color;
        particleSystemColor.a = 0f;
        particleSystem.startColor = particleSystemColor;
        
        while (image.color.a < 0.5f)
        {
            color.a += flashIncrement;
            image.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(flashTime);
        
        while (image.color.a > 0)
        {
            color.a -= flashIncrement;
            image.color = color;
            yield return null;
        }
        
        yield return new WaitForSeconds(6f);

        particleSystemColor.a = 0.5f;
        particleSystem.startColor = particleSystemColor;

        camera.cullingMask = oldMask;
        
        DisableOrEnableUI(god,true);
    }

    private void DisableOrEnableUI(God.Type god, bool enabled)
    {
        hudDictionary[god].racePositionImage.enabled = enabled;
        hudDictionary[god].manaBar.enabled = enabled;
        hudDictionary[god].mainSkillIcon.enabled = enabled;
        hudDictionary[god].secondarySkillTime.enabled = enabled;
        hudDictionary[god].secondarySkillIcon.enabled = enabled;
        hudDictionary[god].turbo.SetActive(enabled);
        hudDictionary[god].laps.SetActive(enabled);

        if(StoreGodInfo.Instance.players == 1)
            hudDictionary[god].timeTrial.enabled = enabled;
        else
        {
            hudDictionary[god].racePositionImage.enabled = enabled;
            hudDictionary[god].minimap.SetActive(enabled);
        }
    }

    public void SetCamera(God.Type god, Camera camera)
    {
        hudDictionary[god].mainCamera = camera;
    }
}
