using UnityEngine;

public class LastCheckpoint : Checkpoint
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Anubis":
                MinimapControl.Instance.Reset(0);
                LapsManager.Instance.godRaceInfoList[0].UpdateCurrentLap();
                break;
            
            case "Poseidon":
                MinimapControl.Instance.Reset(1);
                LapsManager.Instance.godRaceInfoList[1].UpdateCurrentLap();
                break;
            
            case "Kali":
                MinimapControl.Instance.Reset(2);
                LapsManager.Instance.godRaceInfoList[2].UpdateCurrentLap();
                break;
            
            case "Thor":
                MinimapControl.Instance.Reset(3);
                LapsManager.Instance.godRaceInfoList[3].UpdateCurrentLap();
                break;
        }
    }
}
