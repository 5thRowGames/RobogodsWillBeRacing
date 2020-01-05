using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

        private bool canAddLap;

        public bool CanAddLap
        {
            set => canAddLap = value;
        }

        public GodRaceInfo(GameObject god)
        {
            this.god = god;
            canAddLap = false;
            currentLap = 0;
        }
        

        public void UpdateCurrentLap()
        {
            if (canAddLap)
            {
                canAddLap = false;
                
                currentLap++;

                TimeTrial.Instance.ResetAndSaveTime(godType);
                if (currentLap == Instance.totalLaps)
                {
                    Instance.UpdatePlayersFinished(godType);
                }
                else
                {
                    HUDManager.Instance.UpdateLapText(godType,currentLap);
                }
            }
        }

    }

    private int playersFinished;

    [SerializeField] private int totalLaps = 2;
    public List<GameObject> road;
    public List<Checkpoint> checkPoints;
    public List<Portal> portals;
    public List<Transform> portalsExits;
    public List<int> racePosition;

    public List<Transform> Maps;
    
    [Header("Anubis = 0, Poseidon = 1, Kali = 2, Thor = 3")]
    public List<GodRaceInfo> godRaceInfoList; //Posiciones en la lista: Anubis = 0, Poseidon = 1, Kali = 2, Thor = 3

    //Apoyo para actualizar las posiciones
    private int godAmount;
    
    private void Start()
    {
        AddAllCheckpoints();
        playersFinished = 0;
        FirstUpdate();
        godAmount = godRaceInfoList.Count;

        if (StoreGodInfo.Instance.players > 1)
            UpdateGodPosition();
        else
        {
            if (StoreGodInfo.Instance.anubisIA) racePosition[0] = 99; else racePosition[0] = 0;
            if (StoreGodInfo.Instance.poseidonIA) racePosition[1] = 99; else racePosition[1] = 0;
            if (StoreGodInfo.Instance.kaliIA) racePosition[2] = 99; else racePosition[2] = 0;
            if (StoreGodInfo.Instance.thorIA) racePosition[3] = 99; else racePosition[3] = 0;
        }
    }
    
    private void AddAllCheckpoints()
    {
        int contador = 0;
        
        for (int z = 0; z < Maps.Count; z++)
        {
            for (int i = 0; i < Maps[z].childCount; i++)
            {
                for (int j = 0; j < Maps[z].GetChild(i).GetComponent<Transform>().childCount; j++)
                {
                    if (Maps[z].GetChild(i).GetComponent<Transform>().GetChild(j).name == "Checkpoint")
                    {
                        Maps[z].GetChild(i).GetComponent<Transform>().GetChild(j).GetComponent<Checkpoint>().index = contador;
                        checkPoints.Add(Maps[z].GetChild(i).GetComponent<Transform>().GetChild(j).GetComponent<Checkpoint>());
                        contador++;

                    }
                }
            }
        }
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
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
        
            UpdateDistanceNextCheckpoint();
            UpdateRacePosition();
            HUDManager.Instance.UpdatePositionUI();
        }
    }
    
    private void UpdateRacePosition()
    {
        for (int i = 0; i < 4; i++)
        {    
            for (int j = i + 1; j < 4; j++)
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
            {
                currentCheckpoint = 0;
            }
                

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
                godRaceInfoList[3].currentCheckPoint = checkPoint;
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

    public int GetIndexPosition(int position)
    {
        for (int i = 0; i < racePosition.Count; i++)
        {
            if (position == racePosition[i])
                return i;
        }

        return -1;
    }

    private void UpdatePlayersFinished(God.Type god)
    {
        playersFinished++;

        if (playersFinished == StoreGodInfo.Instance.players)
        {
            HUDManager.Instance.finishRaceFade.DOFade(1f, 0.5f).OnComplete(() =>
            {
                foreach (var canvasObject in HUDManager.Instance.playerCanvas)
                    canvasObject.SetActive(false);

                HUDManager.Instance.logoCamera.SetActive(false);
                HUDManager.Instance.HideWaitPlayers();
                godRaceInfoList[(int) god].god.SetActive(false);
                RaceEventManager.Instance.ChangeRaceEvent(RaceEvents.Race.Finish);
            });
        }
        else
        {
            HUDManager.Instance.hudDictionary[god].localFade.DOFade(1f, 0.5f).OnComplete(() =>
            {
                godRaceInfoList[(int) god].currentLap = 99 - playersFinished;
                godRaceInfoList[(int) god].god.SetActive(false);
                HUDManager.Instance.hudDictionary[god].waitingPlayers.DOScale(new Vector3(1f,1f,1f), 0.3f);
            });
        }
    }
}
