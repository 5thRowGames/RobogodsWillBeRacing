using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacePositionManager : Singleton<RacePositionManager>
{
    public List<Transform> godPosition;
    public List<int> racePosition;
    
    //TODO Hablar con alberto por el tema de las posiciones y los checkpoints para las posiciones

    public int ClosestGod(int godID)
    {
        int closest = 0;

        float amount = Mathf.Infinity;

        for (int i = 0; i < godPosition.Count; i++)
        {
            float distance = (godPosition[i].position - godPosition[godID].position).magnitude;
            
            if (i != godID && distance < amount)
            {
                amount = distance;
                closest = i;
            }
        }

        return closest;
    }
}
