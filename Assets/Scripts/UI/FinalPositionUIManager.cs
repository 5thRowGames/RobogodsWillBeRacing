using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FinalPositionUIManager : MonoBehaviour
{
    public List<TextMeshProUGUI> positionText; //0 anubis 1 poseidon 2 kali 3 thor
    public List<RectTransform> buttonPosition; //0 anubis 1 poseidon 2 kali 3 thor

    private List<Vector2> staticPosition;
    private List<int> lastUpdatePosition;
    
    //Pruebas
    public List<int> godPosition;

    private void Awake()
    {
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
        StartCoroutine(UpdateUIPositions());
        
        //Borrar
        StartCoroutine(CosaPrueba());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void UpdatePositionPrueba()
    {
        int random = Random.Range(0, 4);
        int random2 = Random.Range(0, 2);

        if (random2 == 0 && random + 1 < 4)
        {
            int aux = godPosition[random];
            godPosition[random] = godPosition[random + 1];
            godPosition[random + 1] = aux;
        }
        else if (random2 == 1 && random - 1 >= 0)
        {
            int aux = godPosition[random];
            godPosition[random] = godPosition[random - 1];
            godPosition[random - 1] = aux;
        }
        
    }

    IEnumerator CosaPrueba()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.6f);
            UpdatePositionPrueba();
        }
    }

    IEnumerator UpdateUIPositions()
    {

        while (gameObject.activeInHierarchy)
        {
            yield return null;
            
            List<int> auxList = new List<int>(godPosition);

            for (int i = 0; i < buttonPosition.Count; i++)
            {
                if(auxList[i] != lastUpdatePosition[i])
                {

                    int lastPositionToDecrease = lastUpdatePosition.FindIndex(a => a == auxList[i]);

                    IncreaseRankingPosition(i,auxList[i]);
                    //DecreaseRankingPosition(lastUpdatePosition[i],lastPositionToDecrease);
                    lastUpdatePosition[i] = auxList[i];
                    lastUpdatePosition[lastPositionToDecrease] = auxList[lastPositionToDecrease];
                }
                
                /*if (godPosition[i] < lastUpdatePosition[i])
                {
                    int lastPositionToDecrease = lastUpdatePosition.FindIndex(a => a == godPosition[i]);
                    
                    string ss = "";
                    
                    foreach (var pos in godPosition)
                    {
                        ss += " " + pos;
                    }

                    Debug.Log(ss);
                    
                    Debug.Log("Increase "+i+"  "+godPosition[i]+"  Decrease "+lastPositionToDecrease+"   "+lastUpdatePosition[i]);

                    IncreaseRankingPosition(i,godPosition[i]);
                    DecreaseRankingPosition(godPosition[i],i);
                    lastUpdatePosition[i] = godPosition[i];
                    lastUpdatePosition[lastPositionToDecrease] = lastUpdatePosition[i];
                    
                    ss = "ss ";
                    
                    foreach (var pos in lastUpdatePosition)
                    {
                        ss += " " + pos;
                    }
                    
                    Debug.Log(ss);
                }
                else if (godPosition[i] > lastUpdatePosition[i])
                {
                    int lastPositionToDecrease = lastUpdatePosition.FindIndex(a => a == godPosition[i]);
                    
                    string ss = " ";
                    
                    foreach (var pos in godPosition)
                    {
                        ss += " " + pos;
                    }

                    Debug.Log(ss);
                    
                    Debug.Log("Decrease "+i+"  "+godPosition[i]+"  Increase "+lastPositionToDecrease+"   "+lastUpdatePosition[i]);
                
                    IncreaseRankingPosition(lastPositionToDecrease,lastUpdatePosition[i]);
                    DecreaseRankingPosition(i, godPosition[i]);
                    lastUpdatePosition[lastPositionToDecrease] = lastUpdatePosition[i];
                    lastUpdatePosition[i] = godPosition[i];
                    
                    ss = "ss ";
                    
                    foreach (var pos in lastUpdatePosition)
                    {
                        ss += " " + pos;
                    }
                    
                    Debug.Log(ss);
                }*/
                
            }
        }
    }
    
    private void FirstUpdate()
    {
        for (int i = 0; i < buttonPosition.Count; i++)
        {
            buttonPosition[i].anchoredPosition = staticPosition[godPosition[i]];
            positionText[i].text = godPosition[i] + 1 + "º";
            lastUpdatePosition.Add(godPosition[i]);
        }
    }

    private void IncreaseRankingPosition(int buttonID, int toPosition)
    {
        buttonPosition[buttonID].anchoredPosition3D = new Vector3(buttonPosition[buttonID].anchoredPosition.x, buttonPosition[buttonID].anchoredPosition.y, -1);

        Debug.Log(staticPosition[toPosition]);

        Debug.Log(buttonID + "   " + toPosition);

        buttonPosition[buttonID].DOAnchorPos(staticPosition[toPosition], 0.2f, true)
            .OnComplete(() =>
            {
                buttonPosition[buttonID].anchoredPosition3D = new Vector3(buttonPosition[buttonID].anchoredPosition.x, buttonPosition[buttonID].anchoredPosition.y, 0);
                positionText[buttonID].text = toPosition + 1 + "º";
            });
    }

    private void DecreaseRankingPosition(int buttonID, int toPosition)
    {

        Sequence sequence = DOTween.Sequence();
        sequence.Append(buttonPosition[buttonID].DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                buttonPosition[buttonID].anchoredPosition = staticPosition[toPosition];
                positionText[buttonID].text = toPosition + 1 + "º";
            }))
            .Append(buttonPosition[buttonID].DOScale(Vector3.one, 0.1f)).SetDelay(0.1f);
    }

}
