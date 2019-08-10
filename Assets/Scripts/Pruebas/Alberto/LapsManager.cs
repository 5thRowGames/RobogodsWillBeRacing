using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapsManager : MonoBehaviour
{
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

    public static LapsManager instance = null;

    public List<GameObject> gods;
    public List<CircuitSection> circuitSections;
    public List<Checkpoint> checkPoints;
    public List<Portal> portals;
    public List<Transform> portalsExits;
    public List<GodRaceInfo> godRaceInfoList;

    public List<string> godsPositions;

    public int numberOfLaps = 3;

    private bool m_raceFinishedByAll = false;
    public bool RaceFinishedByAll
    {
        get { return m_raceFinishedByAll; }
        private set
        {
            m_raceFinishedByAll = value;
            if (m_raceFinishedByAll)
                raceOverPanel.SetActive(true);
        }
    }

    [Header("Canvas")]
    public List<Text> lapsText;
    public List<Text> checkPointsText;
    public GameObject raceOverPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        // DontDestroyOnLoad(gameObject);

        godRaceInfoList = new List<GodRaceInfo>();
        AddCheckpoints();
        RegisterCheckPoints();
        AddPortals();
        AddPortalsExits();
        RegisterPortals();
    }

    private void FixedUpdate()
    {
        UpdateDistancesToCheckPoints();
        godRaceInfoList.Sort(CompareRacePositions);
        for (int i = 0; i < godRaceInfoList.Count; i++)
            godRaceInfoList[i].racePosition = i;
    }

    private void OnEnable()
    {
        SpawnDiosesParaAlberto.OnGodSpawned += OnGodSpawned;
    }

    private void OnDisable()
    {
        SpawnDiosesParaAlberto.OnGodSpawned -= OnGodSpawned;
    }

    private void OnGodSpawned(GameObject god)
    {
        godRaceInfoList.Add(new GodRaceInfo(god));
        Debug.Log($"{god.name} registered");
    }

    private void RegisterGods()
    {
        godRaceInfoList = new List<GodRaceInfo>();
        godsPositions = new List<string>();
        var players = FindObjectsOfType<MyCarController>();
        foreach (var player in players)
        {
            Debug.Log($"Hello, I'm {player.Name}");
            godRaceInfoList.Add(new GodRaceInfo(player.gameObject));
        }
    }

    private void RegisterCheckPoints()
    {
        for (int i = 0; i < checkPoints.Count; i++)
        {
            checkPoints[i].Index = i;
        }
    }

    // Revisar
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
        }
        portals[portals.Count - 1].targetPortal = portalsExits[0];
    }

    private void AddCheckpoints()
    {
        if (circuitSections != null)
            foreach (CircuitSection cs in circuitSections)
            {
                for (int i = 0; i < cs.checkpoints.Count; i++)
                {
                    checkPoints.Add(cs.checkpoints[i]);
                }
            }
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
    }

    // Revisar porque un mismo coche actualiza ambos textos, no a la vez
    public void UpdateCheckPoint(GameObject god, int checkpoint)
    {
        GodRaceInfo gri = godRaceInfoList.Find(g => g.god == god);
        int index = godRaceInfoList.FindIndex(g => g.god == god);
        
        if (gri == null)
            return;

        if(gri.currentCheckPoint == checkpoint) // Hemos pasado el checkpoint que tocaba
        {
            if(gri.currentCheckPoint + 1 >= checkPoints.Count) // nueva vuelta
            {
                if(gri.currentLap + 1 >= numberOfLaps) // Termina la carrera para el dios
                {
                    gri.raceFinished = true;
                    RaceFinishedByAll = IsRaceFinished(); // Se comprueba si todos han terminado la carrera
                }
                gri.currentLap++;
                UpdateLapsText(index, gri.currentLap);
                gri.currentCheckPoint = 0;
                UpdateCheckPointsText(index, 0);
            }
            else
            {
                gri.currentCheckPoint++;
                UpdateCheckPointsText(index, gri.currentCheckPoint);
            }
        }
    }

    private void UpdateLapsText(int index, int lap)
    {
        lapsText[index].text = lap.ToString();
    }

    private void UpdateCheckPointsText(int index, int checkPoint)
    {
        checkPointsText[index].text = checkPoint.ToString();
    }

    private bool IsRaceFinished()
    {
        foreach(var gl in godRaceInfoList)
        {
            if (!gl.raceFinished)
                return false;
        }
        return true;
    }

    public static int CompareRacePositions(GodRaceInfo godA, GodRaceInfo godB)
    {
        if (godA.currentLap > godB.currentLap)
            return -1;
        else if (godA.currentLap < godB.currentLap)
            return 1;
        else // Mismas vueltas
        {
            if (godA.currentCheckPoint > godB.currentCheckPoint)
                return -1;
            else if (godA.currentCheckPoint < godB.currentCheckPoint)
                return 1;
            else if (godA.distanceToNextCheckPoint < godB.distanceToNextCheckPoint)
                return -1;
            else if (godA.distanceToNextCheckPoint > godB.distanceToNextCheckPoint)
                return 1;
            else
                return 0;
        }
    }

    public float DistanceToNextCheckPoint(GodRaceInfo gri)
    {
        int index = godRaceInfoList.FindIndex(g => g.god == gri.god);

        if (index == -1) return Mathf.Infinity;

        Vector3 nextCheckPointPosition = checkPoints[index].transform.position;
        return Vector3.Distance(gri.god.transform.position, nextCheckPointPosition);
    }

    public void UpdateDistancesToCheckPoints()
    {
        foreach(GodRaceInfo gri in godRaceInfoList)
        {
            gri.distanceToNextCheckPoint = DistanceToNextCheckPoint(gri);
        }
    }

    public static Vector3 GetClosestPointOnLineSegment(Vector3 A, Vector3 B, Vector3 P)
    {
        Vector3 AP = P - A;       //Vector from A to P   
        Vector3 AB = B - A;       //Vector from A to B  

        float magnitudeAB = AB.sqrMagnitude;     //Magnitude of AB vector (it's length squared)     
        float ABAPproduct = Vector3.Dot(AP, AB);    //The DOT product of a_to_p and a_to_b     
        float distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point  

        if (distance < 0)     //Check if P projection is over vectorAB     
        {
            return A;

        }
        else if (distance > 1)
        {
            return B;
        }
        else
        {
            return A + AB * distance;
        }
    }
}
