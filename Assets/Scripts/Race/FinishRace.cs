using System;
using System.Collections.Generic;
using DG.Tweening;
using InControl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FinishPositionLabels
{
    public RectTransform finishPositionLabelGameObject;
    public TextMeshProUGUI name;
    public Image icon;
    public TextMeshProUGUI position;
    public TextMeshProUGUI time;
}

public class FinishRace : MonoBehaviour
{
    public List<Sprite> icons;
    public List<FinishPositionLabels> finishPositionLabels;
    public Image fade;
    public RectTransform anyKey;

    private bool changeScene;
    
    private void Awake()
    {
        changeScene = false;
        Reset();
    }

    private void Start()
    {
        FillPanel();
        FinishRaceTween();
    }

    private void Update()
    {
        if (changeScene)
        {
            InputDevice device = InputManager.ActiveDevice;

            if (Input.anyKey || device.AnyButton.WasPressed)
            {
                SoundManager.Instance.StopLoop(SoundManager.Instance.CurrentMusic);
                FadeTransition();
                changeScene = false;
            }
        }
    }

    private void Reset()
    {
        foreach (var t in finishPositionLabels)
            t.finishPositionLabelGameObject.gameObject.SetActive(false);
    }

    private void FillPanel()
    {
        if (StoreGodInfo.Instance.players == 1)
        {
            int indexPosition = LapsManager.Instance.GetIndexPosition(0);
            finishPositionLabels[0].name.text = FillName(indexPosition);
            finishPositionLabels[0].icon.sprite = icons[(int)GetGod(indexPosition)];
            finishPositionLabels[0].position.text = "1º";
            finishPositionLabels[0].time.text = TransformStringToTime(TimeTrial.Instance.GetTotalTime(GetGod(indexPosition)));
            finishPositionLabels[0].finishPositionLabelGameObject.gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < StoreGodInfo.Instance.players; i++)
            {
                int indexPosition = LapsManager.Instance.GetIndexPosition(i);
                finishPositionLabels[i].name.text = FillName(indexPosition);
                finishPositionLabels[i].icon.sprite = icons[(int)GetGod(indexPosition)];
                finishPositionLabels[i].position.text = i + 1 + "º";
                finishPositionLabels[i].time.text = TransformStringToTime(TimeTrial.Instance.GetTotalTime(GetGod(indexPosition)));
                finishPositionLabels[i].finishPositionLabelGameObject.gameObject.SetActive(true);
            }
        }
    }

    private God.Type GetGod(int indexPosition)
    {
        God.Type god = God.Type.Kali;
        
        switch (indexPosition)
        {
            case 0:
                god = God.Type.Anubis;
                break;
            
            case 1:
                god = God.Type.Poseidon;
                break;
            
            case 2:
                god = God.Type.Kali;
                break;
            
            case 3:
                god = God.Type.Thor;
                break;
        }

        return god;
    }

    private string FillName(int indexPosition)
    {
        string name = "";
        
        switch (indexPosition)
        {
            case 0:
                name = "Anubis";
                break;
            
            case 1:
                name = "Poseidon";
                break;
            
            case 2:
                name = "Kali";
                break;
            
            case 3:
                name = "Thor";
                break;
        }

        return name;
    }

    private string TransformStringToTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time- minutes * 60);

        return String.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void FinishRaceTween()
    {
        float time = 0;
        
        Sequence seq = DOTween.Sequence();
        
        for (int i = 0; i < StoreGodInfo.Instance.players; i++)
        {
            seq.Insert(time, finishPositionLabels[i].finishPositionLabelGameObject.DOAnchorPosX(0, 0.7f));
            time += 0.3f;
        }

        seq.Insert(time, anyKey.DOAnchorPosX(0, 0.7f)).OnComplete(() => { changeScene = true; });
    }

    private void FadeTransition()
    {
        fade.transform.gameObject.SetActive(true);    
            
        fade.DOFade(1, 0.5f).OnComplete(() =>
        {
            RaceEventManager.Instance.ChangeRaceEvent(RaceEvents.Race.LoadingScreen);
            gameObject.SetActive(false);
        });
    }
}
