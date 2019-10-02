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

    public GameObject mainCamera;

    private Vector3[] path1;
    private Vector3[] path2;
    private Vector3[] path3;

    public float fadeDuration;

    void Awake()
    {
        //TODO PERFORMANCE
        path1 = path1List.Select(transform => transform.position).ToArray();
        path2 = path2List.Select(transform => transform.position).ToArray();
        path3 = path3List.Select(transform => transform.position).ToArray();

    }

    private void Start()
    {
        mainCamera.transform.position = path1[0];
        ShowMap();
        MinimapControl.Instance.Init();
    }

    private void ShowMap()
    {
        mainCamera.transform.position = path1[0];
        mainCamera.transform.rotation = path1List[0].rotation;
        
        //Sonido: Recorrido del circuito mostrándolos con la cámara
        SoundManager.Instance.PlayFx(SoundManager.Fx.Inicio_Carrera);
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fade.DOFade(0, fadeDuration));
        sequence.Insert(fadeDuration-0.1f, mainCamera.transform.DOLocalPath(path1, 3.6f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red)
            .OnWaypointChange(x => WayPointChanged(x, path1List)).OnComplete(() =>
                {
                    mainCamera.transform.position = path2[0];
                    mainCamera.transform.rotation = path2List[0].rotation;
                }));
        sequence.Append(mainCamera.transform.DOLocalPath(path2, 4f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red)
            .OnWaypointChange(x => WayPointChanged(x, path2List)).OnComplete(() =>
            {
                mainCamera.transform.position = path3[0];
                mainCamera.transform.rotation = path3List[0].rotation;
            }));
        sequence.Append(mainCamera.transform.DOPath(path3, 4f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red)
            .OnWaypointChange(x => WayPointChanged(x, path3List)));
        sequence.OnComplete(() =>
        {
            RaceEventManager.Instance.ChangeRaceEvent(RaceEvents.Race.Start);
            mainCamera.SetActive(false);
            gameObject.SetActive(false);
        });

    }

    private void WayPointChanged(int wayPoint, List<Transform> list)
    {
        if (wayPoint + 1 < list.Count)
            mainCamera.transform.DORotate(list[wayPoint + 1].rotation.eulerAngles, 1f);
    }

}    
