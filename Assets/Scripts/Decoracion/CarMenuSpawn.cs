using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarMenuSpawn : MonoBehaviour
{
    public int min;
    public int max;
    public List<GameObject> cars;
    public List<Transform> spawnPositions;

    private Coroutine spawnCarCoroutine;

    private void Start()
    {
        spawnCarCoroutine = null;
    }

    private void Update()
    {
        if (spawnCarCoroutine == null)
        {
            spawnCarCoroutine = StartCoroutine(SpawnCars());
        }
    }

    IEnumerator SpawnCars()
    {
        int randomTime = Random.Range(min, max);
        int randomCar = Random.Range(0, cars.Count);
        int randomPosition = Random.Range(0, spawnPositions.Count);
        int randomAmount = Random.Range(1, 4);
        
        yield return new WaitForSeconds(randomTime);

        for (int i = 0; i < randomAmount; i++)
        {
            Instantiate(cars[randomCar], spawnPositions[randomPosition].position, spawnPositions[randomPosition].rotation);
            
            randomPosition++;
            if (randomPosition == spawnPositions.Count)
                randomPosition = 0;
            
            yield return new WaitForSeconds(0.2f);
        }

        spawnCarCoroutine = null;
    }
}
