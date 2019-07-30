using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AwakeRace : MonoBehaviour
{
    public Image fade;
    
    public List<Transform> path1List;
    public List<Transform> path2List;
    public List<Transform> path3List;
    public List<Transform> finalList;
    
    public GameObject camera1;
    public GameObject mainCamera;
    
    private Vector3[] path1;
    private Vector3[] path2;
    private Vector3[] path3;
    private Vector3[] finalPath;

    public float fadeDuration;

    public Transform player;

    void Awake()
    {
        //TODO PERFORMANCE
        path1 = path1List.Select(transform => transform.position).ToArray();
        path2 = path2List.Select(transform => transform.position).ToArray();
        path3 = path3List.Select(transform => transform.position).ToArray();
        finalPath = finalList.Select(transform => transform.position).ToArray();

    }

    private void Start()
    {
        ShowMap();
    }

    private Transform ownTransform;

    private void ShowMap()
    {

        Sequence sequence = DOTween.Sequence();    
        sequence.Append(fade.DOFade(0, fadeDuration));
        sequence.Append(mainCamera.transform.DOMove(path1[0], 0.1f));
        sequence.Append(mainCamera.transform.DORotate(path1List[0].rotation.eulerAngles, 0.1f));
        sequence.Append(mainCamera.transform.DOLocalPath(path1, 4f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red).OnWaypointChange(x => WayPointChanged(x, path1List)));
        sequence.Append(mainCamera.transform.DOMove(path2[0], 0.1f));
        sequence.Append(mainCamera.transform.DORotate(path2List[0].rotation.eulerAngles, 0.1f));
        sequence.Append(mainCamera.transform.DOLocalPath(path2,4f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red).OnWaypointChange(x => WayPointChanged(x, path2List)));
        sequence.Append(mainCamera.transform.DOMove(path3[0], 0.1f));
        sequence.Append(mainCamera.transform.DORotate(path3List[0].rotation.eulerAngles, 0.1f));
        sequence.Append(mainCamera.transform.DOPath(path3,4f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red).OnWaypointChange(x => WayPointChanged(x, path3List)));
        sequence.OnComplete(() =>
        {
            camera1.SetActive(true);
            mainCamera.SetActive(false);
            RotateCamera();
        });
        
    }

    private void WayPointChanged(int wayPoint, List<Transform> list)
    {
        if(wayPoint + 1 < list.Count)
            mainCamera.transform.DORotate(list[wayPoint+1].rotation.eulerAngles, 1f);
    }

    private void RotateCamera()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(camera1.transform.DOPath(finalPath, 3f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red).SetLookAt(player));
    }
}
