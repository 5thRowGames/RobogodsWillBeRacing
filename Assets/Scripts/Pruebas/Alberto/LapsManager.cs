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
        public int currentLap;
        public int currentCheckPoint;
        public bool raceFinished;

        public GodLapsController(string god)
        {
            this.god = god;
            currentLap = 0;
            currentCheckPoint = 0;
            raceFinished = false;
        }
    }

    public static LapsManager instance = null;

    public List<GameObject> gods;
    public List<Checkpoint> checkPoints;
    public List<GodLapsController> godLaps;

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

    public Text lapsText;
    public Text checkPointsText;
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
        Invoke(nameof(RegisterGods), 5f);
        RegisterCheckPoints();
    }

    private void RegisterGods()
    {
        godLaps = new List<GodLapsController>();
        var players = FindObjectsOfType<MyCarController>();
        foreach (var player in players)
        {
            Debug.Log($"Hello, I'm {player.Name}");
            godLaps.Add(new GodLapsController(player.Name));
        }
    }

    private void RegisterCheckPoints()
    {
        for (int i = 0; i < checkPoints.Count; i++)
        {
            checkPoints[i].Index = i;
        }
    }

    public void UpdateCheckPoint(string godName, int checkpoint)
    {
        var god = godLaps.Find(x => x.god == godName);

        if (god == null)
            return;

        if(god.currentCheckPoint == checkpoint) // Hemos pasado el checkpoint que tocaba
        {
            if(god.currentCheckPoint + 1 >= checkPoints.Count) // nueva vuelta
            {
                if(god.currentLap + 1 >= numberOfLaps) // Termina la carrera para el dios
                {
                    god.raceFinished = true;
                    RaceFinishedByAll = isRaceFinished(); // Se comprueba si todos han terminado la carrera
                }
                god.currentLap++;
                UpdateLapsText(god.currentLap);
                god.currentCheckPoint = 0;
                UpdateCheckPointsText(0);
            }
            else
            {
                god.currentCheckPoint++;
                UpdateCheckPointsText(god.currentCheckPoint);
            }
        }
    }

    private void UpdateLapsText(int lap)
    {
        lapsText.text = lap.ToString();
    }

    private void UpdateCheckPointsText(int checkPoint)
    {
        checkPointsText.text = checkPoint.ToString();
    }

    private bool isRaceFinished()
    {
        foreach(var gl in godLaps)
        {
            if (!gl.raceFinished)
                return false;
        }
        return true;
    }
}
