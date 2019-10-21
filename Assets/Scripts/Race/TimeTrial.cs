using System.Collections.Generic;
using UnityEngine;

public class TimeTrial : Singleton<TimeTrial>
{

    private Dictionary<God.Type, List<float>> godLapTimes;

    public float seconds;

    private void Awake()
    {

        godLapTimes = new Dictionary<God.Type, List<float>>();
        godLapTimes.Add(God.Type.Anubis,new List<float>());
        godLapTimes.Add(God.Type.Poseidon,new List<float>());
        godLapTimes.Add(God.Type.Kali,new List<float>());
        godLapTimes.Add(God.Type.Thor,new List<float>());
        
        seconds = 0;
    }

    private void Update()
    {
        seconds += Time.deltaTime;
    }

    public void ResetAndSaveTime(God.Type god)
    {
        float time;

        if (godLapTimes[god].Count == 0)
        {
            godLapTimes[god].Add(seconds);
        }
        else
        {
            time = godLapTimes[god][godLapTimes[god].Count - 1];
            godLapTimes[god].Add(seconds - time);
        }
    }

    public float GetTotalTime(God.Type god)
    {
        float amount = 0;
        
        for (int i = 0; i < godLapTimes[god].Count; i++)
        {
            amount += godLapTimes[god][i];
        }

        return amount;
    }
}
