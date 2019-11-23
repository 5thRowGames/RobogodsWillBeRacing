using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanoA1 : MonoBehaviour
{
    public Transform myCamera;
    public List<Transform> pathList;
    public float travelTime;

    private Vector3[] pathArray;

    #region Unity Events

    private void Awake()
    {
        var numElements = pathList.Count;
        pathArray = new Vector3[numElements];
        for (int i = 0; i < numElements; i++)
            pathArray[i] = pathList[i].position;
    }

    void Start()
    {
        Plano();
    }

    #endregion

    private void Plano()
    {
        myCamera.position = pathArray[0];
        myCamera.rotation = pathList[0].rotation;

        Sequence sequence = DOTween.Sequence();
        sequence.Insert(0f, myCamera.DOPath(pathArray, travelTime, PathType.CatmullRom, PathMode.Full3D, 5, Color.red));
    }
}
