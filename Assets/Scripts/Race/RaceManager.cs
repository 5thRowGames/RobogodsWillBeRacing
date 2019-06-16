using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : Singleton<RaceManager>
{
    public int players;

    private void Awake()
    {
        players = 0;
    }
}
