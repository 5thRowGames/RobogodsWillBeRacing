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
    
    /// <summary>
    /// Último checkpoint por el que ha pasado el jugador
    /// </summary>
    private List<int> lastCheckpointList;
    
    /// <summary>
    /// Distancia recorrida por el jugador sin contar la distancia entre el último checkpoint quue ha pasado el jugador y el siguiente que tiene que pasar
    /// </summary>
    private List<float> currentAmountList;
    
    /// <summary>
    /// Para comprobar si se ha saltado algún checkpoint
    /// </summary>
    private List<int> checkLastChekpoint;

    /// <summary>
    /// Distancia entre los dos checkpoints entre los que los que se encuentra el jugador
    /// </summary>
    private float distanceBetweenPoints;
    
    /// <summary>
    /// Tamaño total de la pista (suma de checkpoints sin contar la distancia entre portales)
    /// </summary>
    private float totalLength;

    private List<int> godsPlayingIndex;

    private void Awake()
    {
        godsPlayingIndex = new List<int>();
        
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

        if (!StoreGodInfo.Instance.anubisIA)
            godsPlayingIndex.Add(0);
        else
            karts[0].SetActive(false);
        
        if (!StoreGodInfo.Instance.poseidonIA)
            godsPlayingIndex.Add(1);
        else
            karts[1].SetActive(false);

        if (!StoreGodInfo.Instance.kaliIA)
            godsPlayingIndex.Add(2);
        else
            karts[2].SetActive(false);

        if (!StoreGodInfo.Instance.thorIA)
            godsPlayingIndex.Add(3);
        else
            karts[3].SetActive(false);
    }
    
    void Update()
    {
         for(int i = 0; i < godsPlayingIndex.Count; i++)
             CalculateDistance(godsPlayingIndex[i]);
    }

    /// <summary>
    /// Calcula la distancia actual que lleva ell jugador y su porcentaje de pista recorrido
    /// </summary>
    /// <param name="id"></param>
    private void CalculateDistance(int id)
    {
        distanceBetweenPoints = (LapsManager.Instance.checkPoints[lastCheckpointList[id]].transform.position - karts[id].transform.position).magnitude;
        currentPercentageList[id] = (currentAmountList[id] + distanceBetweenPoints) / totalLength;
    }
    
    private void CalculateTotalLength()
    {
        for (int i = 0; i < LapsManager.Instance.checkPoints.Count - 1; i++)
        {
            if (!LapsManager.Instance.checkPoints[i].exitPortal)
            {
                totalLength += (LapsManager.Instance.checkPoints[i].transform.position - LapsManager.Instance.checkPoints[i + 1].transform.position).magnitude;
            }
        }
        
        //Borrar, como vamos a dividir el mapa no podemoss cerrar el circuito de primeras
        totalLength += (LapsManager.Instance.checkPoints[LapsManager.Instance.checkPoints.Count - 1].transform.position - LapsManager.Instance.checkPoints[0].transform.position).magnitude;
    }

    /// <summary>
    /// En caso de saltarse algún checkpoint te recalcula toda la distancia que llevas 
    /// </summary>
    /// <param name="id"></param>
    private void UpdateCurrentDistance(int id)
    {
        checkLastChekpoint[id]++;

        if (checkLastChekpoint[id] - 1 != lastCheckpointList[id])
        {
            float amount = 0;
            checkLastChekpoint[id] = lastCheckpointList[id] + 1;
            
            for (int i = 0; i < lastCheckpointList[id]; i++)
            {
                if (!LapsManager.Instance.checkPoints[i].exitPortal)
                {
                    
                    amount += (LapsManager.Instance.checkPoints[i].transform.position - LapsManager.Instance.checkPoints[i + 1].transform.position).magnitude;
                }
            }

            Debug.Log(amount);
            currentAmountList[id] = amount;
        }
        else
        {
            if(!LapsManager.Instance.checkPoints[lastCheckpointList[id]].exitPortal)
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
            
            int index = 0;

            foreach (var playerInfo in StoreGodInfo.Instance.playerInfo)
            {
                switch (playerInfo.godType)
                {
                    case God.Type.Anubis:
                        index = 0;
                        break;
                    
                    case God.Type.Poseidon:
                        index = 1;   
                        break;

                    case God.Type.Kali:
                        index = 2;
                        break;
                
                    case God.Type.Thor:
                        index = 3;
                        break;
                }
                
                individualMinimaps[index].GetComponent<MinimapUI>().Init();
                individualMinimaps[index].SetActive(true);
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
        lastCheckpointList[id_] = 0;

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
