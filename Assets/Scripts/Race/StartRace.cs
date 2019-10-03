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

    public List<Transform> parentCameras;
    public List<GameObject> cameras;
    public List<Camera> UICameras;

    public Canvas globalMinimapCanvas;
    public GameObject anubisCanvas;
    public GameObject poseidonCanvas;
    public GameObject kaliCanvas;
    public GameObject thorCanvas;

    public float rotateCameraTime;
    public float intervalTimeBetweenRotateAndCountdown;

    private void OnEnable()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].SetActive(false);
        }
        
        SetCameraParent();
        SetCameraAndControl();
        SplitScreen(StoreGodInfo.Instance.players);
        StartCoroutine(Init());
        SplitScreenUI(StoreGodInfo.Instance.players);
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
                    poseidonCanvas.GetComponent<Canvas>().worldCamera = UICameras[playerInfo.playerID];
                    poseidonCanvas.GetComponent<Canvas>().planeDistance = 1;
                    poseidonCanvas.SetActive(true);
                    poseidonCanvas.GetComponent<CameraCanvasScaler>().enabled = false;
                    poseidonCanvas.GetComponent<CameraCanvasScaler>().enabled = true;

                    break;
                
                case God.Type.Anubis:
                    anubisCanvas.GetComponent<Canvas>().worldCamera = UICameras[playerInfo.playerID];
                    anubisCanvas.GetComponent<Canvas>().planeDistance = 1;
                    anubisCanvas.SetActive(true);
                    anubisCanvas.GetComponent<CameraCanvasScaler>().enabled = false;
                    anubisCanvas.GetComponent<CameraCanvasScaler>().enabled = true;

                    break;
                
                case God.Type.Kali:
                    kaliCanvas.GetComponent<Canvas>().worldCamera = UICameras[playerInfo.playerID];
                    kaliCanvas.GetComponent<Canvas>().planeDistance = 1;
                    kaliCanvas.SetActive(true);
                    kaliCanvas.GetComponent<CameraCanvasScaler>().enabled = false;
                    kaliCanvas.GetComponent<CameraCanvasScaler>().enabled = true;

                    break;
                
                case God.Type.Thor:
                    thorCanvas.GetComponent<Canvas>().worldCamera = UICameras[playerInfo.playerID];
                    thorCanvas.GetComponent<Canvas>().planeDistance = 1;
                    thorCanvas.SetActive(true);
                    thorCanvas.GetComponent<CameraCanvasScaler>().enabled = false;
                    thorCanvas.GetComponent<CameraCanvasScaler>().enabled = true;

                    break;
            }
        }

        if (playersNumber > 2)
            globalMinimapCanvas.enabled = true;

    }

    private void SetCameraAndControl()
    {
        
        foreach (var playerInfo in StoreGodInfo.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                case God.Type.Poseidon:
                    
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
                    
                    poseidonPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    poseidonPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    poseidonPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;

                    cameras[playerInfo.playerID].GetComponent<CameraController>().target = poseidonPlayer;
                    cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    //Sigue el mismo orden que la UI
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().AssignIndex(1);
                    //cameras[playerInfo.playerID].GetComponentInChildren<SpeedParticles>().AssignIndex(1);
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().enabled = true;

                    poseidonPlayer.GetComponent<MyCarController>().enabled = true;
                    poseidonPlayer.GetComponent<PlayerSkillManager>().enabled = true;
                    poseidonPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;
                    poseidonPlayer.GetComponent<ItemManager>().enabled = true;
                    
                    break;

                case God.Type.Anubis:
                    
                    //anubisPlayer.GetComponent<IncontrolProvider>().enabled = true;

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

                    anubisPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    anubisPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    anubisPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;

                    //Sigue el mismo orden que la UI
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().AssignIndex(0);
                    //cameras[playerInfo.playerID].GetComponentInChildren<SpeedParticles>().AssignIndex(0);
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().enabled = true;

                    cameras[playerInfo.playerID].GetComponent<CameraController>().target = anubisPlayer;
                    cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    anubisPlayer.GetComponent<MyCarController>().enabled = true;
                    anubisPlayer.GetComponent<PlayerSkillManager>().enabled = true;
                    anubisPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;
                    anubisPlayer.GetComponent<ItemManager>().enabled = true;

                    break;
                
                case God.Type.Thor:
                    
                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                        thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        
                        if (StoreGodInfo.Instance.eduardo)
                        {
                            thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo2();
                            cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo2();
                        }
                    }
                    
                    thorPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    thorPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    thorPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;

                    cameras[playerInfo.playerID].GetComponent<CameraController>().target = thorPlayer;
                    cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().enabled = true;

                    //Sigue el mismo orden que la UI
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().AssignIndex(3);
                    //cameras[playerInfo.playerID].GetComponentInChildren<SpeedParticles>().AssignIndex(3);
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().enabled = true;

                    thorPlayer.GetComponent<MyCarController>().enabled = true;
                    thorPlayer.GetComponent<PlayerSkillManager>().enabled = true;
                    thorPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;
                    thorPlayer.GetComponent<ItemManager>().enabled = true;
                    
                    break;
                
                case God.Type.Kali:

                    if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                    {
                        kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    }
                    else
                    {
                        kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                        
                        if (StoreGodInfo.Instance.eduardo)
                        {
                            kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo1();
                            cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo1();
                        }
                    }
                    
                    kaliPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice; 
                    kaliPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    kaliPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;

                    cameras[playerInfo.playerID].GetComponent<CameraController>().target = kaliPlayer;
                    cameras[playerInfo.playerID].GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;

                    //Sigue el mismo orden que la UI
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().AssignIndex(2);
                    //cameras[playerInfo.playerID].GetComponentInChildren<SpeedParticles>().AssignIndex(2);
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().enabled = true;


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
    
    private void SplitScreenUI(int players)
    {

        switch (players)
        {
            case 1:

                UICameras[0].gameObject.SetActive(false);
                UICameras[0].gameObject.SetActive(true);
                UICameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                UICameras[0].gameObject.SetActive(false);
                UICameras[0].gameObject.SetActive(true);

                break;

            case 2:

                UICameras[0].gameObject.SetActive(false);
                UICameras[1].gameObject.SetActive(false);

                UICameras[0].gameObject.SetActive(true);
                UICameras[1].gameObject.SetActive(true);

                UICameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1f);
                UICameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1f);


                break;

            case 3:
                
                UICameras[0].gameObject.SetActive(false);
                UICameras[1].gameObject.SetActive(false);
                UICameras[2].gameObject.SetActive(false);

                UICameras[0].gameObject.SetActive(true);
                UICameras[1].gameObject.SetActive(true);
                UICameras[2].gameObject.SetActive(true);

                UICameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                UICameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                UICameras[2].GetComponent<Camera>().rect = new Rect(0, 0, 1f, 0.5f);

                break;

            case 4:
                
                UICameras[0].gameObject.SetActive(false);
                UICameras[1].gameObject.SetActive(false);
                UICameras[2].gameObject.SetActive(false);
                UICameras[3].gameObject.SetActive(false);
                
                UICameras[0].gameObject.SetActive(true);
                UICameras[1].gameObject.SetActive(true);
                UICameras[2].gameObject.SetActive(true);
                UICameras[3].gameObject.SetActive(true);

                UICameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                UICameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                UICameras[2].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                UICameras[3].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                
                break;
        }
    }

    private void SetCameraParent()
    {
        foreach (var playerInfo in StoreGodInfo.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                case God.Type.Anubis:
                    cameras[playerInfo.playerID].transform.parent = parentCameras[0];
                    break;
                
                case God.Type.Poseidon:
                    cameras[playerInfo.playerID].transform.parent = parentCameras[1];
                    break;

                case God.Type.Kali:
                    cameras[playerInfo.playerID].transform.parent = parentCameras[2];
                    break;
                
                case God.Type.Thor:
                    cameras[playerInfo.playerID].transform.parent = parentCameras[3];
                    break;
            }
        }
    }

}
