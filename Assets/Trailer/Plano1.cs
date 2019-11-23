using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Plano1 : MonoBehaviour
{
    public GameObject camera;
    public List<Transform> path1List;
    public float travelTime;
    
    private Vector3[] path1;

    private void Awake()
    {
        path1 = path1List.Select(transform => transform.position).ToArray();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Plano();
    }

    private void Start()
    {
        camera.transform.position = path1[0];
        camera.transform.rotation = path1List[0].rotation;
    }

    private void Plano()
    {

        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0f, camera.transform.DOPath(path1, travelTime, PathType.CatmullRom, PathMode.Full3D, 5, Color.red));
    }
}
