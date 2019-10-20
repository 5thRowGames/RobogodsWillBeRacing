using System.Collections.Generic;
using UnityEngine;

public class TimeTrial : Singleton<TimeTrial>
{
    private List<float> lapTimes;
    
    public float seconds;
    public bool starTime;

    private void Awake()
    {
        lapTimes = new List<float>();
        seconds = 0;
    }

    private void Update()
    {
        seconds += Time.deltaTime;
    }

    public void ResetAndSaveTime()
    {
        lapTimes.Add(seconds);
    }
}
