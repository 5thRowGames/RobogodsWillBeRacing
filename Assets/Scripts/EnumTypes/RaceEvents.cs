using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceEvents
{
    public enum Race
    {
        None = 0,
        Awake = 1,
        Finish = 2,
        Pause = 3,
        Start = 4,
        CountdownFinished = 5,
        SettingsPause = 6,
        ContinueRace = 7
    }
}
