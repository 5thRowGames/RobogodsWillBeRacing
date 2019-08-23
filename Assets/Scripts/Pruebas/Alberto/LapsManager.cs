using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GodRaceInfo
{
    public GameObject god;
    public int currentLap;
    public int currentCheckPoint;
    public float distanceToNextCheckPoint;
    public int racePosition;
    public bool raceFinished;

    public GodRaceInfo(GameObject god)
    {
        this.god = god;
        raceFinished = false;
    }

}

public class LapsManager : Singleton<LapsManager>
{
    public List<GameObject> road;
    public List<int> racePosition;
    public List<Transform> checkPoints;
    public List<GodRaceInfo> godRaceInfoList; //Tiene que haber un orden por huevos de: Anubis = 0, Poseidon = 1, Kali = 2, Thor = 3

    private int godAmount;

    private void Awake()
    {
        FirstUpdate();
        godAmount = godRaceInfoList.Count;
    }

    private void FirstUpdate()
    {
        for (int i = 0; i < godRaceInfoList.Count; i++)
        {
            godRaceInfoList[i].racePosition = i;
        }
    }

    public void UpdateGodPosition()
    {
        StartCoroutine(UpdateRacePositionCoroutine());
    }

    IEnumerator UpdateRacePositionCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        
        UpdateDistanceNextCheckpoint();
        UpdateRacePosition();
    }
    
    private void UpdateRacePosition()
    {
        for (int i = 0; i < godAmount; i++)
        {
            for (int j = i + 1; j < godAmount; j++)
            {
                int aux;
                if (godRaceInfoList[i].currentLap == godRaceInfoList[j].currentLap)
                {
                    if (godRaceInfoList[i].currentCheckPoint == godRaceInfoList[j].currentCheckPoint)
                    {
                        if (godRaceInfoList[i].distanceToNextCheckPoint < godRaceInfoList[j].distanceToNextCheckPoint && racePosition[i] > racePosition[j])
                        {
                            aux = racePosition[i];
                            racePosition[i] = racePosition[j];
                            racePosition[j] = aux;
                        }
                    }
                    else if (godRaceInfoList[i].currentCheckPoint > godRaceInfoList[j].currentCheckPoint && racePosition[i] > racePosition[j])
                    {
                        aux = racePosition[i];
                        racePosition[i] = racePosition[j];
                        racePosition[j] = aux;
                    }
                    else if (godRaceInfoList[i].currentCheckPoint < godRaceInfoList[j].currentCheckPoint && racePosition[i] < racePosition[j])
                    {
                        aux = racePosition[i];
                        racePosition[i] = racePosition[j];
                        racePosition[j] = aux;
                    }
                }
                else if (godRaceInfoList[i].currentLap > godRaceInfoList[j].currentLap && racePosition[i] > racePosition[j])
                {
                    aux = racePosition[i];
                    racePosition[i] = racePosition[j];
                    racePosition[j] = aux;
                }
                else if (godRaceInfoList[i].currentLap < godRaceInfoList[j].currentLap && racePosition[i] < racePosition[j])
                {
                    aux = racePosition[i];
                    racePosition[i] = racePosition[j];
                    racePosition[j] = aux;
                }
            }
        }
    }

    private void UpdateDistanceNextCheckpoint()
    {
        for (int i = 0; i < godAmount; i++)
        {
            int currentCheckpoint = godRaceInfoList[i].currentCheckPoint;

            if (currentCheckpoint + 1 == checkPoints.Count)
                currentCheckpoint = 0;

            godRaceInfoList[i].distanceToNextCheckPoint = (checkPoints[currentCheckpoint].position - godRaceInfoList[i].god.transform.position).sqrMagnitude;
        }
    }
    
    
}
