using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapControl : MonoBehaviour
{
    public List<Transform> checkPoints;
    public Transform player;
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
            if (value < checkPoints.Count)
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
        distanceBetweenPoints = (checkPoints[LastCheckpoint].position - player.position).magnitude;

        currentPercentage = (currentAmount + distanceBetweenPoints) / totalLength;
    }

    private void CalculateTotalLength()
    {
        for (int i = 0; i < checkPoints.Count - 1; i++)
        {
            totalLength += (checkPoints[i].position - checkPoints[i + 1].position).magnitude;
        }
        
        //Borrar, como vamos a dividir el mapa no podemoss cerrar el circuito de primeras
        totalLength += (checkPoints[checkPoints.Count - 1].position - checkPoints[0].position).magnitude;
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
                amount += (checkPoints[i].position - checkPoints[i + 1].position).magnitude;
            }

            currentAmount = amount;
        }
        else
        {
            currentAmount += (checkPoints[LastCheckpoint].position - checkPoints[LastCheckpoint - 1].position).magnitude;
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
