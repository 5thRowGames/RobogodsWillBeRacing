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
    public Image manaBar;
    public Image mainSkillIcon;
    public Sprite mainSkillEnabledSprite;
    public Sprite mainSkillDisabledSprite;
    public TextMeshProUGUI secondarySkillTime;
    public Image secondarySkillIcon;
    public Sprite secondarySkillEnabledSprite;
    public Sprite secondarySkillDisabledSprite;
    public Image currentItem;

    private float time;

    public float Time
    {
        get => time;
        set => time = value;
    }
}

public class HUDManager : Singleton<HUDManager>
{
    public GameObject thorCanvas;
    public GameObject poseidonCanvas;
    public GameObject anubisCanvas;
    public GameObject kaliCanvas;

    public List<HUDInfo> hudInfo;

    public Dictionary<God.Type, HUDInfo> hudDictionary; //0 anubis, 1 poseidon 2 kali 3 thor

    private void Awake()
    {
        hudDictionary = new Dictionary<God.Type, HUDInfo>();
        
        foreach (var hud in hudInfo)
        {
            hudDictionary.Add(hud.god, hud);
        }
    }

    public float chooseItemTimer;
    public List<Sprite> itemIcon;

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
        hudDictionary[god].manaBar.fillAmount += Mathf.Clamp(hudDictionary[god].manaBar.fillAmount + (amount/100f), 0, 1);

        if (hudDictionary[god].manaBar.fillAmount == 1)
        {
            hudDictionary[god].mainSkillIcon.sprite = hudDictionary[god].mainSkillEnabledSprite;
        }
        else
        {
            hudDictionary[god].mainSkillIcon.sprite = hudDictionary[god].mainSkillDisabledSprite;
        }
    }

    public void StartSecondarySkillTime(God.Type god,float time)
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
            text.text = hudDictionary[god].Time.ToString();
            yield return null;
        }

        hudDictionary[god].secondarySkillIcon.sprite = hudDictionary[god].secondarySkillEnabledSprite;
        text.text = "";
    }
}
