using System.Collections.Generic;
using InControl;
using UnityEngine;

public class PlayerInfo
{

    public InputDevice inputDevice;
    public IncontrolProvider.ControlType controlType;
    public God.Type godType;
}

public class RaceManager : Singleton<RaceManager>
{
    public bool poseidonIA;
    public bool anubisIA;
    public bool kaliIA;
    public bool thorIA;

    public int players;

    public List<PlayerInfo> playerInfo = new List<PlayerInfo>();
    
    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        poseidonIA = false;
        anubisIA = false;
        kaliIA = false;
        thorIA = false;
        players = 0;
        playerInfo.Clear();
    }
}
