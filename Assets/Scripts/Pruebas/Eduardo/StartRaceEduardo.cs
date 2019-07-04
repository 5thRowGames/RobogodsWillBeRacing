using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartRaceEduardo : MonoBehaviour
{
    public List<Transform> startPositions;
    
    public GameObject poseidonPlayer;
    public GameObject anubisPlayer;
    public GameObject thorPlayer;
    public GameObject kaliPlayer;

    public List<GameObject> cameras;
    public GameObject mainCamera;

    public int players;

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
        SetCameraAndControl();
        StartRaceTween();
    }

    public void SetCameraAndControl()
    {
        int cameraIndex = 0;

        for(int i = 0; i < players; i++)
        {

            switch (i)
            {
                                
                case 0:
                    
                    GameObject poseidon = Instantiate(poseidonPlayer, startPositions[0].position,startPositions[0].rotation);
                    poseidon.GetComponent<IncontrolProvider>().InputDevice = null;
                    poseidon.GetComponent<IncontrolProvider>().controlType = IncontrolProvider.ControlType.Keyboard;

                    cameras[cameraIndex].GetComponent<CameraController>().target = poseidon;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = null;

                    poseidon.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo1();
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo1();

                    cameraIndex++;
                    break;

                case 1:
                    
                    GameObject anubis = Instantiate(anubisPlayer, startPositions[1].position,startPositions[1].rotation);
                    anubis.GetComponent<IncontrolProvider>().InputDevice = null;

                    cameras[cameraIndex].GetComponent<CameraController>().target = anubis;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = null;

                    anubis.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo2();
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo2();
                    
                    cameraIndex++;
                    break;
                
                case 2:
                    
                    GameObject thor = Instantiate(thorPlayer, startPositions[2].position,startPositions[2].rotation);
                    thor.GetComponent<IncontrolProvider>().InputDevice = null;

                    cameras[cameraIndex].GetComponent<CameraController>().target = thor;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = null;

                    thor.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo3();
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo3();
                    
                    cameraIndex++;
                    break;
                
                case 3:
                    
                    GameObject kali = Instantiate(kaliPlayer, startPositions[3].position,startPositions[3].rotation);
                    kali.GetComponent<IncontrolProvider>().InputDevice = null;

                    cameras[cameraIndex].GetComponent<CameraController>().target = kali;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = null;

                    kali.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo4();
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo4();
                    
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
        SplitScreen(players);
        ConnectDisconnectManager.ConnectCarSoundManager();
        ConnectDisconnectManager.ConnectCarControllerDelegate();

    }


}
