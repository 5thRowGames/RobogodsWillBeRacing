﻿using System;
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
    public List<FinishPositionLabels> finishPositionLabels;
    public Image fade;
    public RectTransform anyKey;
    public RectTransform backgroundPanel;

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
        for (int i = 0; i < StoreGodInfo.Instance.players; i++)
        {
            finishPositionLabels[i].name.text = FillName(LapsManager.Instance.GetIndexPosition(i));
            finishPositionLabels[i].position.text = i + 1 + " º";
            finishPositionLabels[i].time.text = TransformStringToTime(TimeTrial.Instance.GetTotalTime(GetGod(i)));
            finishPositionLabels[i].finishPositionLabelGameObject.gameObject.SetActive(true);
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
        return String.Format("{00:00}",time);
    }

    private void FinishRaceTween()
    {
        float time = 0;

        backgroundPanel.DOScale(new Vector3(1, 1, 1), 0.4f);
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
        fade.DOFade(1, 0.5f).OnComplete(() =>
        {
            RaceEventManager.Instance.ChangeRaceEvent(RaceEvents.Race.LoadingScreen);
            gameObject.SetActive(false);
        });
    }
}