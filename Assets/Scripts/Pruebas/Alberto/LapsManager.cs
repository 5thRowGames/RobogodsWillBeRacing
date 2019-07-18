using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapsManager : MonoBehaviour
{
    [System.Serializable]
    public class GodLapsController
    {
        public string god;
        public Transform tr;
        public int currentLap;
        public int currentCheckPoint;
        public float distanceToNextCheckPoint;
        public bool raceFinished;

        public GodLapsController(string god, Transform tr)
        {
            this.god = god;
            this.tr = tr;
            currentLap = 0;
            currentCheckPoint = 0;
            raceFinished = false;
        }

    }

    public static LapsManager instance = null;

    public List<GameObject> gods;
    public List<Checkpoint> checkPoints;
    public List<GodLapsController> godLaps;

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
    }

    private void Start()
    {
        //Invoke(nameof(RegisterGods), 5f);
        godLaps = new List<GodLapsController>();


        RegisterCheckPoints();
    }

    private void Update()
    {
        UpdateDistancesToCheckPoints();

        godLaps.Sort(CompareRacePositions);
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
        godLaps.Add(new GodLapsController(god.name, god.transform));
        Debug.Log($"{god.name} registered");
    }

    private void RegisterGods()
    {
        godLaps = new List<GodLapsController>();
        godsPositions = new List<string>();
        var players = FindObjectsOfType<MyCarController>();
        foreach (var player in players)
        {
            Debug.Log($"Hello, I'm {player.Name}");
            godLaps.Add(new GodLapsController(player.Name, player.transform));
        }
    }

    private void RegisterCheckPoints()
    {
        for (int i = 0; i < checkPoints.Count; i++)
        {
            checkPoints[i].Index = i;
        }
    }

    // Revisar porque un mismo coche actualiza ambos textos, no a la vez
    public void UpdateCheckPoint(string godName, int checkpoint)
    {
        GodLapsController god = godLaps.Find(g => g.god == godName);
        int index = godLaps.FindIndex(g => g.god == godName);
        
        if (god == null)
            return;

        if(god.currentCheckPoint == checkpoint) // Hemos pasado el checkpoint que tocaba
        {
            if(god.currentCheckPoint + 1 >= checkPoints.Count) // nueva vuelta
            {
                if(god.currentLap + 1 >= numberOfLaps) // Termina la carrera para el dios
                {
                    god.raceFinished = true;
                    RaceFinishedByAll = IsRaceFinished(); // Se comprueba si todos han terminado la carrera
                }
                god.currentLap++;
                UpdateLapsText(index, god.currentLap);
                god.currentCheckPoint = 0;
                UpdateCheckPointsText(index, 0);
            }
            else
            {
                god.currentCheckPoint++;
                UpdateCheckPointsText(index, god.currentCheckPoint);
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
        foreach(var gl in godLaps)
        {
            if (!gl.raceFinished)
                return false;
        }
        return true;
    }

    public static int CompareRacePositions(GodLapsController godA, GodLapsController godB)
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

    public float DistanceToNextCheckPoint(GodLapsController glc)
    {
        int index = godLaps.FindIndex(g => g.god == glc.god);
        Vector3 nextCheckPointPosition = checkPoints[index].transform.position;
        return Vector3.Distance(glc.tr.position, nextCheckPointPosition);
    }

    public void UpdateDistancesToCheckPoints()
    {
        foreach(GodLapsController glc in godLaps)
        {
            glc.distanceToNextCheckPoint = DistanceToNextCheckPoint(glc);
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
