using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsHelper : MonoBehaviour
{
    public List<GameObject> maps;
    public List<Checkpoint> checkpoints;
    public int currentCheckpoint;
    public CarHelper carHelper;
    bool isRotatingToCheckpoint;

    void Start()
    {
        carHelper = GetComponent<CarHelper>();

        currentCheckpoint = 0;
        var numOfMaps = maps.Count;
        for(int i = 0; i < numOfMaps; i++)
        {
            var checkpointsArray = maps[i].GetComponentsInChildren<Checkpoint>();
            var count = checkpointsArray.Length;
            for(int k = 0; k < count; k++)
            {
                checkpoints.Add(checkpointsArray[k]);
                checkpoints[k].index = checkpoints.Count - 1;
            }
        }
    }

    public Transform GetNextCheckpoint()
    {
        return checkpoints[currentCheckpoint++].transform;
    }

    public void RotateToCheckpoint(float maxDegrees)
    {
        isRotatingToCheckpoint = true;
        StartCoroutine(RotateToCheckpointCoroutine(maxDegrees));
    }


    private IEnumerator RotateToCheckpointCoroutine(float maxDegrees)
    {
        var currentDegrees = 0f;
        if(currentDegrees <= maxDegrees)
        {

        }
        yield return null;
    }
}
