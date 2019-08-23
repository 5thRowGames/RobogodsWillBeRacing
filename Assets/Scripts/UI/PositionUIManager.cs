using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PositionUIManager : Singleton<PositionUIManager>
{
    [Header("Finish Race Positions")]
    public List<TextMeshProUGUI> positionText; //0 anubis 1 poseidon 2 kali 3 thor
    public List<RectTransform> buttonPosition; //0 anubis 1 poseidon 2 kali 3 thor

    private List<Vector2> staticPosition;
    private List<int> lastUpdatePosition;
    private List<int> auxList;
    private int godAmount;
    

    private void Awake()
    {
        godAmount = LapsManager.Instance.godRaceInfoList.Count;
        
        auxList = new List<int>(godAmount);
        staticPosition = new List<Vector2>();

        foreach (var position in buttonPosition)
        {
            staticPosition.Add(position.anchoredPosition);
        }

        lastUpdatePosition = new List<int>();
    }

    private void OnEnable()
    {
        FirstUpdate();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void UpdateRacePosition()
    {
        StartCoroutine(UpdateRacePositionCoroutine());
    }

    IEnumerator UpdateRacePositionCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < godAmount; i++)
        {
            positionText[i].text = LapsManager.Instance.godRaceInfoList[i].racePosition + 1.ToString();
        }
    }

    public void UpdateFinalPosition()
    {
        StartCoroutine(UpdateFinalPositionCoroutine());
    }

    IEnumerator UpdateFinalPositionCoroutine()
    {

        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < godAmount; i++)
                auxList[i] = LapsManager.Instance.godRaceInfoList[i].racePosition;

            for (int i = 0; i < buttonPosition.Count; i++)
            {
                if(auxList[i] < lastUpdatePosition[i])
                {

                    //TODO Habrá que hacer a lo mejor un control que no salga -1 la busqueda del índice
                    int lastPositionToDecrease = lastUpdatePosition.FindIndex(a => a == auxList[i]);

                    IncreaseRankingPosition(i,auxList[i]);
                    DecreaseRankingPosition(lastPositionToDecrease,lastUpdatePosition[i]);
                    lastUpdatePosition[i] = auxList[i];
                    lastUpdatePosition[lastPositionToDecrease] = auxList[lastPositionToDecrease];
                }
                else if (auxList[i] > lastUpdatePosition[i])
                {
                    int lastPositionToDecrease = lastUpdatePosition.FindIndex(a => a == auxList[i]);

                    IncreaseRankingPosition(lastPositionToDecrease,lastUpdatePosition[i]);
                    DecreaseRankingPosition(i,auxList[i]);
                    lastUpdatePosition[i] = auxList[i];
                    lastUpdatePosition[lastPositionToDecrease] = auxList[lastPositionToDecrease];
                }
            }
        }
    }
    
    private void FirstUpdate()
    {
        for (int i = 0; i < buttonPosition.Count; i++)
        {
            buttonPosition[i].anchoredPosition = staticPosition[LapsManager.Instance.godRaceInfoList[i].racePosition];
            positionText[i].text = LapsManager.Instance.godRaceInfoList[i].racePosition + 1 + "º";
            lastUpdatePosition.Add(LapsManager.Instance.godRaceInfoList[i].racePosition);
        }
    }

    private void IncreaseRankingPosition(int buttonID, int toPosition)
    {
        buttonPosition[buttonID].anchoredPosition3D = new Vector3(buttonPosition[buttonID].anchoredPosition.x, buttonPosition[buttonID].anchoredPosition.y, -1);

        buttonPosition[buttonID].DOAnchorPos(staticPosition[toPosition], 0.4f, true)
            .OnComplete(() =>
            {
                buttonPosition[buttonID].anchoredPosition3D = new Vector3(buttonPosition[buttonID].anchoredPosition.x, buttonPosition[buttonID].anchoredPosition.y, 0);
                positionText[buttonID].text = toPosition + 1 + "º";
            });
    }

    private void DecreaseRankingPosition(int buttonID, int toPosition)
    {

        Sequence sequence = DOTween.Sequence();
        sequence.Append(buttonPosition[buttonID].DOScale(Vector3.zero, 0.15f).OnComplete(() =>
            {
                buttonPosition[buttonID].anchoredPosition = staticPosition[toPosition];
                positionText[buttonID].text = toPosition + 1.ToString();
            }))
            .Append(buttonPosition[buttonID].DOScale(Vector3.one, 0.15f)).SetDelay(0.1f);
    }

}
