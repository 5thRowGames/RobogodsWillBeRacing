using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public RectTransform leftPositionRect;
    public RectTransform rightPositionRect;
    public RectTransform currentRect;
    public TextMeshProUGUI countDownText;
    public string finalTextLocalization;

    public float showNumberInScreenTime;
    public float movementTime;

    private void Awake()
    {
        countDownText.text = "11";
        currentRect.anchoredPosition = leftPositionRect.anchoredPosition;
    }

    private void OnEnable()
    {
        StartCountdown();
    }

    private void StartCountdown()
    {
        Sequence sequence = DOTween.Sequence();
            sequence.Append(currentRect.DOAnchorPosX(0, movementTime))
            .Append(currentRect.DOAnchorPosX(rightPositionRect.anchoredPosition.x, movementTime).SetDelay(showNumberInScreenTime).OnComplete(() =>
                {
                    currentRect.anchoredPosition = leftPositionRect.anchoredPosition;
                    ChangeText("10");
                }))
            .Append(currentRect.DOAnchorPosX(0, movementTime))
            .Append(currentRect.DOAnchorPosX(rightPositionRect.anchoredPosition.x, movementTime).SetDelay(showNumberInScreenTime).OnComplete(() =>
            {
                currentRect.anchoredPosition = leftPositionRect.anchoredPosition;
                ChangeText("1");
            }))
            .Append(currentRect.DOAnchorPosX(0, movementTime))
            .Append(currentRect.DOAnchorPosX(rightPositionRect.anchoredPosition.x, movementTime).SetDelay(showNumberInScreenTime).OnComplete(() =>
            {
                currentRect.anchoredPosition = leftPositionRect.anchoredPosition;
                ChangeText(LocalizationManager.Instance.GetWord(finalTextLocalization));
            }))
            .Append(currentRect.DOAnchorPosX(0, movementTime))
            .Append(currentRect.DOAnchorPosX(rightPositionRect.anchoredPosition.x, movementTime).SetDelay(showNumberInScreenTime))
            .OnComplete(() =>
            {
                //Si queremos meter algo más aquí que sea global, hay que hacerlo en ChangeRaceEvent de RaceEventManager
                RaceEventManager.Instance.ChangeRaceEvent(RaceEvents.Race.CountdownFinished);
            });
    }

    private void ChangeText(string text)
    {
        countDownText.text = text;
    }
}
