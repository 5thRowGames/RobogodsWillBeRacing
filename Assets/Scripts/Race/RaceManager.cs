using System.Collections.Generic;
using InControl;

public class PlayerInfo
{
    public InputDevice inputDevice;
    public MyPlayerActions myPlayerActions;
    public GodType.RobogodType godType;
}

public class RaceManager : Singleton<RaceManager>
{
    public bool poseidonIA;
    public bool anubisIA;
    public bool kaliIA;
    public bool thorIA;

    public int players;

    public List<PlayerInfo> playerInfo;

    private void Awake()
    {
        playerInfo = new List<PlayerInfo>();
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
