using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCheckpoint : Checkpoint
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Anubis":
                LapsManager.Instance.godRaceInfoList[0].CanAddLap = true;
                break;
            
            case "Poseidon":
                LapsManager.Instance.godRaceInfoList[1].CanAddLap = true;
                break;
            
            case "Kali":
                LapsManager.Instance.godRaceInfoList[2].CanAddLap = true;
                break;
            
            case "Thor":
                LapsManager.Instance.godRaceInfoList[3].CanAddLap = true;
                break;
        }
    }
}
