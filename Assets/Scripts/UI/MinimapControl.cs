using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapControl : MonoBehaviour
{
    public RectTransform playerIcon;
    public RectTransform start;
    public RectTransform finish;

    private int checkLastCheckpoint;
    private int lastCheckpoint;

    public int LastCheckpoint
    {
        get => lastCheckpoint;
        set
        {
            if (value < LapsManager.Instance.checkPoints.Count)
                lastCheckpoint = value;
        }
    }
    
    public float currentAmount;

    [SerializeField] private float distanceBetweenPoints;
    [SerializeField] private float totalLength;
    [SerializeField] private float currentPercentage;

    private float changeX, startIconPosition;

    private void Awake()
    {
        CalculateTotalLength();
        lastCheckpoint = 0;
        checkLastCheckpoint = 1;
        startIconPosition = playerIcon.anchoredPosition.x;
        playerIcon.anchoredPosition = start.anchoredPosition;
    }
    
    void Update()
    {
        CalculateDistance();

        changeX = Mathf.Lerp(start.anchoredPosition.x, finish.anchoredPosition.x, currentPercentage);
        playerIcon.anchoredPosition = new Vector2(startIconPosition + changeX, playerIcon.anchoredPosition.y);
    }

    private void CalculateDistance()
    {
        distanceBetweenPoints = (LapsManager.Instance.checkPoints[LastCheckpoint].transform.position - transform.position).magnitude;
        
        currentPercentage = (currentAmount + distanceBetweenPoints) / totalLength;
    }

    private void CalculateTotalLength()
    {
        for (int i = 0; i < LapsManager.Instance.checkPoints.Count - 1; i++)
        {
            totalLength += (LapsManager.Instance.checkPoints[i].transform.position - LapsManager.Instance.checkPoints[i + 1].transform.position).magnitude;
        }
        
        //Borrar, como vamos a dividir el mapa no podemoss cerrar el circuito de primeras
        totalLength += (LapsManager.Instance.checkPoints[LapsManager.Instance.checkPoints.Count - 1].transform.position - LapsManager.Instance.checkPoints[0].transform.position).magnitude;
    }

    public void UpdateCurrentDistance()
    {
        checkLastCheckpoint++;

        if (checkLastCheckpoint - 1 != lastCheckpoint)
        {
            float amount = 0;
            checkLastCheckpoint = lastCheckpoint + 1;
            
            for (int i = 0; i < lastCheckpoint; i++)
            {
                amount += (LapsManager.Instance.checkPoints[i].transform.position - LapsManager.Instance.checkPoints[i + 1].transform.position).magnitude;
            }

            currentAmount = amount;
        }
        else
        {
            currentAmount += (LapsManager.Instance.checkPoints[LastCheckpoint].transform.position - LapsManager.Instance.checkPoints[LastCheckpoint - 1].transform.position).magnitude;
        }
    }

    public void Reset()
    {
        //TODO en vez de poner 1 hay que poner el número de vueltas que lleva
        playerIcon.anchoredPosition3D = new Vector3(start.anchoredPosition.x, start.anchoredPosition.y, 1);
        lastCheckpoint = 0;
        checkLastCheckpoint = 1;
        currentAmount = 0;
        playerIcon.anchoredPosition = start.anchoredPosition;
    }
}
