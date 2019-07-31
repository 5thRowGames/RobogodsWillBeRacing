using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartRace : MonoBehaviour
{
    public GameObject poseidonPlayer;
    public GameObject anubisPlayer;
    public GameObject thorPlayer;
    public GameObject kaliPlayer;

    public List<GameObject> cameras;
    
    //[Header("UI")]

    private void OnEnable()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].SetActive(false);
        }
        
        SetCameraAndControl();
        SplitScreen(RaceManager.Instance.players);
        ConnectDisconnectManager.ConnectCarSoundManager();
        ConnectDisconnectManager.ConnectCarControllerDelegate();
    }

    public void SetCameraAndControl()
    {
        int cameraIndex = 0;
        
        foreach (var playerInfo in RaceManager.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                case God.Type.Poseidon:
                    
                    poseidonPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    poseidonPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;

                    cameras[cameraIndex].GetComponent<CameraController>().target = poseidonPlayer;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                        poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                    }

                    cameraIndex++;
                    break;

                case God.Type.Anubis:
                    
                    anubisPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    cameras[cameraIndex].GetComponent<CameraController>().target = anubisPlayer;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    
                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                        anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                    }
                    
                    cameraIndex++;
                    break;
                
                case God.Type.Thor:
                    
                    thorPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    cameras[cameraIndex].GetComponent<CameraController>().target = thorPlayer;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    
                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                       thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                    }
                    
                    cameraIndex++;
                    break;
                
                case God.Type.Kali:
                    
                    kaliPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    cameras[cameraIndex].GetComponent<CameraController>().target = kaliPlayer;
                    cameras[cameraIndex].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    
                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                        kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[cameraIndex].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                    }
                    
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
    }

}
