using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlanoATodaPastilla : MonoBehaviour
{
    public GameObject camera;
    public Vector3 rotateVector;
    public float rotateTime;

    public List<GameObject> cars;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        foreach (var car in cars)
        {
            car.SetActive(true);
        }

        yield return new WaitForSeconds(1.2f);

        camera.transform.DORotate(rotateVector, rotateTime);
    }
}
