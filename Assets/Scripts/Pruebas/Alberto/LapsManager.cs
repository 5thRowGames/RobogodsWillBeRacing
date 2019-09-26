using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapsManager : Singleton<LapsManager>
{
    [System.Serializable]
    public class GodRaceInfo
    {
        public GameObject god;
        public God.Type godType;
        public int currentLap;
        public int currentCheckPoint;
        public float distanceToNextCheckPoint;
        public bool raceFinished;

        public GodRaceInfo(GameObject god)
        {
            this.god = god;
            raceFinished = false;
        }

        public void UpdateCurrentLap()
        {
            currentLap++;

            if (currentLap > 3)
            {
                raceFinished = true;
            }
            else
            {
                HUDManager.Instance.UpdateLapText(godType,currentLap);
            }
        }

    }
    
    public List<GameObject> road;
    public List<CircuitSection> circuitSections;
    public List<Checkpoint> checkPoints;
    public List<Portal> portals;
    public List<Transform> portalsExits;
    public List<int> racePosition;
    
    [Header("Anubis = 0, Poseidon = 1, Kali = 2, Thor = 3")]
    public List<GodRaceInfo> godRaceInfoList; //Posiciones en la lista: Anubis = 0, Poseidon = 1, Kali = 2, Thor = 3

    //Apoyo para actualizar las posiciones
    private int godAmount;
    
    private void Awake()
    {
        FirstUpdate();
        godAmount = godRaceInfoList.Count;
        UpdateGodPosition();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void FirstUpdate()
    {
        for (int i = 0; i < godRaceInfoList.Count; i++)
        {
            racePosition[i] = i;
        }
    }

    public void UpdateGodPosition()
    {
        StartCoroutine(UpdateRacePositionCoroutine());
    }

    IEnumerator UpdateRacePositionCoroutine()
    {
        float time = 0;

        while (time == 0)
        {
            yield return new WaitForSeconds(0.5f);
        
            UpdateDistanceNextCheckpoint();
            UpdateRacePosition();
            HUDManager.Instance.UpdatePositionUI();
        }
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
                        else if (godRaceInfoList[i].distanceToNextCheckPoint > godRaceInfoList[j].distanceToNextCheckPoint && racePosition[i] < racePosition[j])
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

            godRaceInfoList[i].distanceToNextCheckPoint = (checkPoints[currentCheckpoint + 1].transform.position - godRaceInfoList[i].god.transform.position).sqrMagnitude;
        }
    }

    public void UpdateCheckPoint(string tag, int checkPoint)
    {
        switch (tag)
        {
            case "Anubis":
                godRaceInfoList[0].currentCheckPoint = checkPoint;
                break;
            
            case "Poseidon":
                godRaceInfoList[1].currentCheckPoint = checkPoint;
                break;
            
            case "Kali":
                godRaceInfoList[2].currentCheckPoint = checkPoint;
                break;
            
            case "Thor":
                godRaceInfoList[2].currentCheckPoint = checkPoint;
                break;
        }
    }
    
        
    // Revisar (cosas de Alberto)
    private void RegisterPortalsOld()
    {
        for(int i = 1; i < portals.Count - 1; i += 2)
        {
            portals[i].targetPortal = portals[i + 1].transform;
        }
        portals[portals.Count - 1].targetPortal = portals[0].transform;
        portals[0].targetPortal = portals[portals.Count - 1].transform;
        for(int i = 2; i < portals.Count - 1; i += 2)
        {
            portals[i].targetPortal = portals[i - 1].transform;
        }

        for(int i = 0; i < portals.Count; i++)
        {
            portals[i].index = i;
        }
    }

    private void RegisterPortals()
    {
        for(int i = 0; i < portals.Count - 1; i++)
        {
            portals[i].targetPortal = portalsExits[i + 1];
            portals[i].index = i;
        }
        portals[portals.Count - 1].targetPortal = portalsExits[0];
        portals[portals.Count - 1].index = portals.Count - 1;
    }

    /*private void AddCheckpoints()
    {
        Checkpoint lastCheckpoint = null;
        if (circuitSections != null)
        {
            foreach (CircuitSection cs in circuitSections)
            {
                for (int i = 0; i < cs.checkpoints.Count; i++)
                {
                    if (!cs.checkpoints[i].lastCheckpoint)
                        checkPoints.Add(cs.checkpoints[i]);
                    else
                    {
                        lastCheckpoint = cs.checkpoints[i];
                        Debug.Log($"{lastCheckpoint.transform.parent.name}.{lastCheckpoint.name}");
                    }
                }
            }
        }
        if (lastCheckpoint != null)
            checkPoints.Add(lastCheckpoint);
    }

    private void AddPortals()
    {
        if (circuitSections != null)
        {
            foreach (CircuitSection cs in circuitSections)
            {
                for (int i = 0; i < cs.portalsEntrances.Count; i++)
                {
                    portals.Add(cs.portalsEntrances[i]);
                }
            }
        }
    }

    private void AddPortalsExits()
    {
        if(circuitSections != null)
        {
            foreach(CircuitSection cs in circuitSections)
            {
                for(int i = 0; i < cs.portalsExits.Count; i++)
                {
                    portalsExits.Add(cs.portalsExits[i]);
                }
            }
        }
    }*/
}
