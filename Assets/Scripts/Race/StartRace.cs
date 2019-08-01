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
    public List<GameObject> playerCanvas;

    public float rotateCameraTime;
    public float intervalTimeBetweenRotateAndCountdown;

    //[Header("UI")]

    private void OnEnable()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].SetActive(false);
        }
        
        SetCameraAndControl();
        SplitScreen(RaceManager.Instance.players);
        StartCoroutine(InitCameraPosition());
        /*ConnectDisconnectManager.ConnectCarSoundManager();
        ConnectDisconnectManager.ConnectCarControllerDelegate();*/
    }
    

    IEnumerator InitCameraPosition()
    {
        int playersNumber = RaceManager.Instance.players;
        
        Tween tween = cameras[0].transform.parent.transform.DOLocalRotate(new Vector3(0, 180, 0), rotateCameraTime);

        for (int i = 1; i < playersNumber; i++)
        {
            cameras[i].transform.parent.transform.DOLocalRotate(new Vector3(0, 180, 0), rotateCameraTime);
        }

        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(intervalTimeBetweenRotateAndCountdown);
        
        for (int i = 0; i < playersNumber; i++)
        {
            playerCanvas[i].SetActive(true);
        }
    }

    private void SetCameraAndControl()
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
