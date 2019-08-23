using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaOrdenacion : MonoBehaviour
{
    public List<int> desiredList;
    public List<int> finalCheckpoints;
    public List<int> distancesToNextCheckpoint;
    public List<int> laps;
    public List<int> racePosition;

    private void Awake()
    {
        SortList();    
    }

    private void SortList()
    {
        for (int i = 0; i < desiredList.Count; i++)
        {
            for (int j = i + 1; j < desiredList.Count; j++)
            {
                int aux;
                if (laps[i] == laps[j])
                {
                    if (finalCheckpoints[i] == finalCheckpoints[j])
                    {
                        if (distancesToNextCheckpoint[i] < distancesToNextCheckpoint[j] && racePosition[i] > racePosition[j])
                        {
                            aux = racePosition[i];
                            racePosition[i] = racePosition[j];
                            racePosition[j] = aux;
                        }
                    }
                    else if (finalCheckpoints[i] > finalCheckpoints[j] && racePosition[i] > racePosition[j])
                    {
                        aux = racePosition[i];
                        racePosition[i] = racePosition[j];
                        racePosition[j] = aux;
                    }
                    else if (finalCheckpoints[i] < finalCheckpoints[j] && racePosition[i] < racePosition[j])
                    {
                        aux = racePosition[i];
                        racePosition[i] = racePosition[j];
                        racePosition[j] = aux;
                    }
                }
                else if (laps[i] > laps[j] && racePosition[i] > racePosition[j])
                {
                    aux = racePosition[i];
                    racePosition[i] = racePosition[j];
                    racePosition[j] = aux;
                }
                else if (laps[i] < laps[j] && racePosition[i] < racePosition[j])
                {
                    aux = racePosition[i];
                    racePosition[i] = racePosition[j];
                    racePosition[j] = aux;
                }
            }
        }
    }
}
