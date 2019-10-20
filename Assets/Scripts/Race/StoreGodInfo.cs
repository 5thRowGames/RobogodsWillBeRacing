using System;
using System.Collections.Generic;
using InControl;
using UnityEngine;

[SerializeField]
public class PlayerInfo
{

    public int playerID;
    public InputDevice inputDevice;
    public IncontrolProvider.ControlType controlType;
    public God.Type godType;
}

public class StoreGodInfo : SingletonDontDestroy<StoreGodInfo>
{
    public bool poseidonIA;
    public bool anubisIA;
    public bool kaliIA;
    public bool thorIA;

    //Borrar
    public bool eduardo;

    public int players;
    public List<PlayerInfo> playerInfo = new List<PlayerInfo>();

    private void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        poseidonIA = true;
        anubisIA = true;
        kaliIA = true;
        thorIA = true;
        players = 0;
        playerInfo.Clear();
    }
}
