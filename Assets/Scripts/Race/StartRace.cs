using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartRace : MonoBehaviour
{
    public List<Transform> startPositions;
    
    public GameObject poseidonPlayer;
    public GameObject anubisPlayer;
    public GameObject thorPlayer;
    public GameObject kaliPlayer;

    public GameObject poseidonIA;
    public GameObject anubisIA;
    public GameObject thorIA;
    public GameObject kaliIA;
    
    public List<GameObject> cameras;
    public GameObject mainCamera;
    
    public Transform finishPosition;
    public Vector3[] path1;
    public Vector3[] path2;
    public Vector3[] path3;
    public Image fadeImage; 

    private void Awake()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].SetActive(false);
        }
        mainCamera.SetActive(true);
    }

    private void Start()
    {
        StartRaceTween();
        SetIA();
        SetCameraAndControl();
    }

    public void SetIA()
    {
        if (!RaceManager.Instance.poseidonIA)
            Instantiate(poseidonIA, startPositions[0]);

        if (!RaceManager.Instance.anubisIA)
            Instantiate(anubisIA, startPositions[1]);

        if (!RaceManager.Instance.thorIA)
            Instantiate(thorIA, startPositions[2]);

        if (RaceManager.Instance.kaliIA)
            Instantiate(kaliIA, startPositions[3]);
    }

    public void SetCameraAndControl()
    {
        int cameraIndex = 0;
        
        foreach (var playerInfo in RaceManager.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                                
                case GodType.RobogodType.Poseidon:
                    
                    GameObject poseidon = Instantiate(poseidonPlayer, startPositions[0]);
                    poseidon.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    poseidon.GetComponent<IncontrolProvider>().myPlayerActions = playerInfo.myPlayerActions;
                    
                    cameras[cameraIndex].GetComponent<CameraController>().target = poseidon;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = playerInfo.myPlayerActions;
                    cameraIndex++;
                    break;

                case GodType.RobogodType.Anubis:
                    
                    GameObject anubis = Instantiate(anubisPlayer, startPositions[1]);
                    anubis.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    anubis.GetComponent<IncontrolProvider>().myPlayerActions = playerInfo.myPlayerActions;
                    
                    cameras[cameraIndex].GetComponent<CameraController>().target = anubis;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = playerInfo.myPlayerActions;
                    cameraIndex++;
                    break;
                
                case GodType.RobogodType.Thor:
                    
                    GameObject thor = Instantiate(thorPlayer, startPositions[2]);
                    thor.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    thor.GetComponent<IncontrolProvider>().myPlayerActions = playerInfo.myPlayerActions;
                    
                    cameras[cameraIndex].GetComponent<CameraController>().target = thor;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = playerInfo.myPlayerActions;
                    cameraIndex++;
                    break;
                
                case GodType.RobogodType.Kali:
                    
                    GameObject kali = Instantiate(kaliPlayer, startPositions[3]);
                    kali.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    kali.GetComponent<IncontrolProvider>().myPlayerActions = playerInfo.myPlayerActions;
                    
                    cameras[cameraIndex].GetComponent<CameraController>().target = kali;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = playerInfo.myPlayerActions;
                    cameraIndex++;
                    break;
            }   
        }
    }
    
    private void SplitScreen(int players)
    {

        switch (players)
        {
            case 1:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                cameras[0].SetActive(true);
                
                break;

            case 2:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
                cameras[0].SetActive(true);

                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
                cameras[1].SetActive(true);
                
                break;

            case 3:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[0].SetActive(true);

                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[1].SetActive(true);

                cameras[2].GetComponent<Camera>().rect = new Rect(0, 0, 1f, 0.5f);
                cameras[2].SetActive(true);

                break;

            case 4:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[0].SetActive(true);

                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[1].SetActive(true);

                cameras[2].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                cameras[2].SetActive(true);

                cameras[3].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                cameras[3].SetActive(true);
                
                break;
        }

        mainCamera.SetActive(false);
    }
    
    public void StartRaceTween()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fadeImage.DOFade(0, 0.5f));
        sequence.Insert(0.1f, mainCamera.transform.DOMove(path1[0], 0.1f));
        sequence.Append(mainCamera.transform.DOPath(path1, 2f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red));
        sequence.Append(mainCamera.transform.DOMove(path2[0], 0.1f));
        sequence.Append(mainCamera.transform.DOPath(path2,2f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red));
        sequence.Append(mainCamera.transform.DOMove(path3[0], 0.1f));
        sequence.Append(mainCamera.transform.DOPath(path3,2f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red));
        sequence.Append(fadeImage.DOFade(1, 0.5f));
        sequence.Insert(7, transform.DOMove(finishPosition.position, 0.1f));
        sequence.Append(fadeImage.DOFade(0, 0.3f));
        sequence.OnComplete(() =>
        {
            SplitScreen(RaceManager.Instance.players);
            ConnectDisconnectManager.DisconnectCarControllerDelegate();
            ConnectDisconnectManager.ConnectCarSoundManager();
        });

    }


}
