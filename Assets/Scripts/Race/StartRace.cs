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

    public GameObject anubisCanvas;
    public GameObject poseidonCanvas;
    public GameObject kaliCanvas;
    public GameObject thorCanvas;

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
        SplitScreen(StoreGodInfo.Instance.players);
        StartCoroutine(Init());
    }
    

    IEnumerator Init()
    {
        int playersNumber = StoreGodInfo.Instance.players;
        
        Tween tween = cameras[0].transform.parent.transform.DOLocalRotate(new Vector3(0, 180, 0), rotateCameraTime);

        for (int i = 1; i < playersNumber; i++)
        {
            cameras[i].transform.parent.transform.DOLocalRotate(new Vector3(0, 180, 0), rotateCameraTime);
        }

        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(intervalTimeBetweenRotateAndCountdown);

        foreach (var playerInfo in StoreGodInfo.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                case God.Type.Poseidon:
                    poseidonCanvas.GetComponent<Canvas>().worldCamera = cameras[playerInfo.playerID].GetComponent<Camera>();
                    poseidonCanvas.GetComponent<Canvas>().planeDistance = 1;
                    poseidonCanvas.SetActive(true);
                    break;
                
                case God.Type.Anubis:
                    anubisCanvas.GetComponent<Canvas>().worldCamera = cameras[playerInfo.playerID].GetComponent<Camera>();
                    anubisCanvas.GetComponent<Canvas>().planeDistance = 1;
                    anubisCanvas.SetActive(true);
                    break;
                
                case God.Type.Kali:
                    kaliCanvas.GetComponent<Canvas>().worldCamera = cameras[playerInfo.playerID].GetComponent<Camera>();
                    kaliCanvas.GetComponent<Canvas>().planeDistance = 1;
                    kaliCanvas.SetActive(true);
                    break;
                
                case God.Type.Thor:
                    thorCanvas.GetComponent<Canvas>().worldCamera = cameras[playerInfo.playerID].GetComponent<Camera>();
                    thorCanvas.GetComponent<Canvas>().planeDistance = 1;
                    thorCanvas.SetActive(true);
                    break;
            }
        }
    }

    private void SetCameraAndControl()
    {
        
        foreach (var playerInfo in StoreGodInfo.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                case God.Type.Poseidon:
                    
                    poseidonPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    poseidonPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    poseidonPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;

                    cameras[playerInfo.playerID].GetComponent<CameraController>().target = poseidonPlayer;
                    cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                        poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                    }

                    poseidonPlayer.GetComponent<MyCarController>().enabled = true;
                    poseidonPlayer.GetComponent<PlayerSkillManager>().enabled = true;
                    poseidonPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;
                    poseidonPlayer.GetComponent<ItemManager>().enabled = true;
                    
                    break;

                case God.Type.Anubis:
                    
                    anubisPlayer.GetComponent<IncontrolProvider>().enabled = true;
                    anubisPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    anubisPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    anubisPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;

                    cameras[playerInfo.playerID].GetComponent<CameraController>().target = anubisPlayer;
                    cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    
                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                        anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                    }
                    
                    //anubisPlayer.GetComponent<MyCarController>().enabled = true;
                    anubisPlayer.GetComponent<PlayerSkillManager>().enabled = true;
                    anubisPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;
                    anubisPlayer.GetComponent<ItemManager>().enabled = true;
                    
                    break;
                
                case God.Type.Thor:
                    
                    thorPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    thorPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    thorPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;

                    cameras[playerInfo.playerID].GetComponent<CameraController>().target = thorPlayer;
                    cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                       thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                    }
                    
                    thorPlayer.GetComponent<MyCarController>().enabled = true;
                    thorPlayer.GetComponent<PlayerSkillManager>().enabled = true;
                    thorPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;
                    thorPlayer.GetComponent<ItemManager>().enabled = true;
                    
                    break;
                
                case God.Type.Kali:
                    
                    kaliPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice; 
                    kaliPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    kaliPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;

                    cameras[playerInfo.playerID].GetComponent<CameraController>().target = kaliPlayer;
                    cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                        kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                    }
                    
                    kaliPlayer.GetComponent<MyCarController>().enabled = true;
                    kaliPlayer.GetComponent<PlayerSkillManager>().enabled = true;
                    kaliPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;
                    kaliPlayer.GetComponent<ItemManager>().enabled = true;
                    
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
