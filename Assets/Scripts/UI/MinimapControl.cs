using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapControl : Singleton<MinimapControl>
{
    public GameObject globalMinimap;
    public List<GameObject> karts;
    
    public List<GameObject> individualMinimaps;

    public List<float> currentPercentageList;
    
    private List<int> lastCheckpointList;
    private List<float> currentAmountList;
    private List<int> checkLastChekpoint;

    private float distanceBetweenPoints;
    private float totalLength;

    private void Awake()
    {
        lastCheckpointList = new List<int>();
        currentAmountList = new List<float>();
        checkLastChekpoint = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            lastCheckpointList.Add(0);
            currentAmountList.Add(0);
            checkLastChekpoint.Add(0);
        }

        totalLength = 0;

        CalculateTotalLength();
    }
    
    void Update()
    {
        CalculateDistance(0); //Anubis
        CalculateDistance(1); //Poseidon
        CalculateDistance(2); //Kali
        CalculateDistance(3); //Thor
    }

    private void CalculateDistance(int id)
    {
        distanceBetweenPoints = (LapsManager.Instance.checkPoints[lastCheckpointList[id]].transform.position - karts[id].transform.position).magnitude;
        
        currentPercentageList[id] = (currentAmountList[id] + distanceBetweenPoints) / totalLength;
    }

    private void CalculateTotalLength()
    {
        for (int i = 0; i < LapsManager.Instance.checkPoints.Count - 1; i++)
        {
            if (!LapsManager.Instance.checkPoints[i].firstPortal)
            {
                totalLength += (LapsManager.Instance.checkPoints[i].transform.position - LapsManager.Instance.checkPoints[i + 1].transform.position).magnitude;
            }
        }
        
        //Borrar, como vamos a dividir el mapa no podemoss cerrar el circuito de primeras
        totalLength += (LapsManager.Instance.checkPoints[LapsManager.Instance.checkPoints.Count - 1].transform.position - LapsManager.Instance.checkPoints[0].transform.position).magnitude;
    }

    private void UpdateCurrentDistance(int id)
    {
        checkLastChekpoint[id]++;

        if (checkLastChekpoint[id] - 1 != lastCheckpointList[id])
        {
            float amount = 0;
            checkLastChekpoint[id] = lastCheckpointList[id] + 1;
            
            for (int i = 0; i < lastCheckpointList[id]; i++)
            {
                amount += (LapsManager.Instance.checkPoints[i].transform.position - LapsManager.Instance.checkPoints[i + 1].transform.position).magnitude;
            }

            currentAmountList[id] = amount;
        }
        else
        {
            currentAmountList[id] += (LapsManager.Instance.checkPoints[lastCheckpointList[id]].transform.position - LapsManager.Instance.checkPoints[lastCheckpointList[id] - 1].transform.position).magnitude;
        }
    }

    public void UpdateMinimapControl(string tag, int indexCheckpoint)
    {
        switch (tag)
        {
            case "Anubis":
                ModifyLastCheckpoint(0,indexCheckpoint);
                UpdateCurrentDistance(0);
                break;
            
            case "Poseidon":
                ModifyLastCheckpoint(1,indexCheckpoint);
                UpdateCurrentDistance(1);
                break;
            
            case "Kali":
                ModifyLastCheckpoint(2,indexCheckpoint);
                UpdateCurrentDistance(2);
                break;
            
            case "Thor":
                ModifyLastCheckpoint(3,indexCheckpoint);
                UpdateCurrentDistance(3);
                break;
        }
    }

    public void Init()
    {
        if (StoreGodInfo.Instance.players < 3)
        {
            globalMinimap.SetActive(false);
            
            for (int i = 0; i < StoreGodInfo.Instance.players; i++)
            {
                individualMinimaps[i].GetComponent<MinimapUI>().Init();
                individualMinimaps[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < StoreGodInfo.Instance.players; i++)
            {
                individualMinimaps[i].SetActive(false);
            }

            globalMinimap.SetActive(true);
            
            globalMinimap.GetComponent<MinimapUI>().Init();
        }
    }

    public void Reset(int id_)
    {
        checkLastChekpoint[id_] = 0;
        currentAmountList[id_] = 0;
        checkLastChekpoint[id_] = 0;

    }

    private void ModifyLastCheckpoint(int i, int value)
    {
        if (value == LapsManager.Instance.checkPoints.Count)
        {
            lastCheckpointList[i] = 0;
        }
        else
        {
            lastCheckpointList[i] = value;
        }
    }
}
