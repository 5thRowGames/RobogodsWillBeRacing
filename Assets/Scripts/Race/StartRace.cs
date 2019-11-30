using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using InControl;
using UnityEngine;
using UnityEngine.UI;

public class StartRace : MonoBehaviour
{
    public GameObject anubisPlayer;
    public GameObject poseidonPlayer;
    public GameObject kaliPlayer;
    public GameObject thorPlayer;

    public GameObject logoCanvas;
    public List<GameObject> cinematicCameras; //0 Anubis 1 Poseidon 2 Kali 3 Thor
    public List<Transform> parentCameras;
    public List<GameObject> cameras;
    public List<Camera> UICameras;
    public GameObject logoCamera;
    public List<PowerTrail> powerLeftTrails;
    public List<PowerTrail> powerRightTrails;

    public Canvas globalMinimapCanvas;
    public GameObject anubisCanvas;
    public GameObject poseidonCanvas;
    public GameObject kaliCanvas;
    public GameObject thorCanvas;

    public float rotateCameraTime;
    public float intervalTimeBetweenRotateAndCountdown;
    
    public List<ParticleSystem> portalFlash;

    private List<God.Type> cameraIndex;

    private void OnEnable()
    {
        cameraIndex = new List<God.Type>(4);
        
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].SetActive(false);
            cinematicCameras[i].SetActive(false);
        }
        
        SetCameraAndControl();
        SplitScreen(StoreGodInfo.Instance.players);
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        int playersNumber = StoreGodInfo.Instance.players;

        for (int i = 0; i < playersNumber; i++)
        {
            cinematicCameras[i].SetActive(true);
                
            Sequence seq = DOTween.Sequence();
            seq.Append(cinematicCameras[i].transform.parent.transform.DORotate(new Vector3(0, -360, 0), rotateCameraTime))
                .Append(cinematicCameras[i].transform.DOMove(cameras[(int)cameraIndex[i]].transform.position, 1.2f));
        }

        yield return new WaitForSeconds(intervalTimeBetweenRotateAndCountdown + rotateCameraTime + 1f);

        for (int i = 0; i < playersNumber; i++)
        {
            cinematicCameras[i].SetActive(false);
        }

        foreach (var playerInfo in StoreGodInfo.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                case God.Type.Anubis:
                    cameras[0].transform.parent.gameObject.SetActive(true);
                    anubisCanvas.GetComponent<Canvas>().worldCamera = UICameras[(int) cameraIndex[playerInfo.playerID]];
                    anubisCanvas.GetComponent<Canvas>().planeDistance = 1;
                    anubisCanvas.SetActive(true);
                    anubisCanvas.GetComponent<CameraCanvasScaler>().enabled = false;
                    anubisCanvas.GetComponent<CameraCanvasScaler>().enabled = true;
                    
                    HUDManager.Instance.SetCamera(God.Type.Anubis,cameras[0].GetComponent<Camera>());

                    break;
                
                case God.Type.Poseidon:
                    cameras[1].transform.parent.gameObject.SetActive(true);
                    poseidonCanvas.GetComponent<Canvas>().worldCamera = UICameras[(int)cameraIndex[playerInfo.playerID]];
                    poseidonCanvas.GetComponent<Canvas>().planeDistance = 1;
                    poseidonCanvas.SetActive(true);
                    poseidonCanvas.GetComponent<CameraCanvasScaler>().enabled = false;
                    poseidonCanvas.GetComponent<CameraCanvasScaler>().enabled = true;
                    cameras[1].transform.parent.gameObject.SetActive(true);
                    
                    HUDManager.Instance.SetCamera(God.Type.Poseidon,cameras[1].GetComponent<Camera>());

                    break;

                case God.Type.Kali:
                    cameras[2].transform.parent.gameObject.SetActive(true);
                    kaliCanvas.GetComponent<Canvas>().worldCamera = UICameras[(int) cameraIndex[playerInfo.playerID]];
                    kaliCanvas.GetComponent<Canvas>().planeDistance = 1;
                    kaliCanvas.SetActive(true);
                    kaliCanvas.GetComponent<CameraCanvasScaler>().enabled = false;
                    kaliCanvas.GetComponent<CameraCanvasScaler>().enabled = true;
                    cameras[2].transform.parent.gameObject.SetActive(true);
                    
                    HUDManager.Instance.SetCamera(God.Type.Kali,cameras[2].GetComponent<Camera>());

                    break;
                
                case God.Type.Thor:
                    cameras[3].transform.parent.gameObject.SetActive(true);
                    thorCanvas.GetComponent<Canvas>().worldCamera = UICameras[(int) cameraIndex[playerInfo.playerID]];
                    thorCanvas.GetComponent<Canvas>().planeDistance = 1;
                    thorCanvas.SetActive(true);
                    thorCanvas.GetComponent<CameraCanvasScaler>().enabled = false;
                    thorCanvas.GetComponent<CameraCanvasScaler>().enabled = true;
                    cameras[3].transform.parent.gameObject.SetActive(true);
                    
                    HUDManager.Instance.SetCamera(God.Type.Thor,cameras[3].GetComponent<Camera>());

                    break;
            }
        }

        //Es necesario este bucle unos segundos después de haber dividido el viewport rect ya que se buguean ambas cámaras y no aparece la UI 
        for (int i = 0; i < playersNumber; i++)
        {
            UICameras[(int)cameraIndex[i]].gameObject.SetActive(false);
            UICameras[(int)cameraIndex[i]].gameObject.SetActive(true);
        }

        if (playersNumber > 2)
            globalMinimapCanvas.enabled = true;

        for (int i = 0; i < portalFlash.Count; i++)
        {
            portalFlash[i].Play();
        }

        Invoke(nameof(ActiveRacingCameraBehaviour), 5.9f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            portalFlash[0].Play();
        }
    }

    private void ActiveRacingCameraBehaviour()
    {
        foreach (var playerInfo in StoreGodInfo.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                case God.Type.Anubis:
                    cameras[0].transform.parent.GetComponent<RacingCamera>().enabled = true;

                    break;
                
                case God.Type.Poseidon:
                    cameras[1].transform.parent.GetComponent<RacingCamera>().enabled = true;
                    break;

                case God.Type.Kali:
                    cameras[2].transform.parent.GetComponent<RacingCamera>().enabled = true;
                    break;
                
                case God.Type.Thor:
                    cameras[3].transform.parent.GetComponent<RacingCamera>().enabled = true;
                    break;
            }
        }
    }

    private void SetCameraAndControl()
    {
        int carSoundIndex = 1;
        
        foreach (var playerInfo in StoreGodInfo.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                case God.Type.Anubis:

                    /*if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                        anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    else
                        anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();*/

                    switch (carSoundIndex)
                    {
                        case 1:
                            anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo1();
                            break;
                        
                        case 2:
                            anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo2();
                            break;
                        
                        case 3:
                            anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo3();
                            break;
                        
                        case 4:
                            anubisPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo4();
                            break;
                    }

                    anubisPlayer.GetComponent<PlayerCarSoundManager>().unsharedSoundStart = "Nave_" + carSoundIndex;
                    carSoundIndex++;
                    
                    anubisPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    anubisPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    anubisPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;

                    anubisPlayer.GetComponent<MyCarController>().powerLeftTrail = powerLeftTrails[0];
                    anubisPlayer.GetComponent<MyCarController>().powerRightTrail = powerRightTrails[0];

                    /*//Sigue el mismo orden que la UI
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().AssignIndex(0);
                    //cameras[playerInfo.playerID].GetComponentInChildren<SpeedParticles>().AssignIndex(0);
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().enabled = true;*/

                    anubisPlayer.GetComponent<MyCarController>().enabled = true;
                    anubisPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;

                    //Sirve para dividir la pantalla de las cámaras, así se el orden en el que se han seleccionado los personajes
                    //0 es porque es Anubis (mirar orden de los dioses)
                    cameraIndex.Add(God.Type.Anubis);

                    break;                
                
                case God.Type.Poseidon:
                    
                    /*if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                        poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    else
                        poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();*/
                    
                    switch (carSoundIndex)
                    {
                        case 1:
                            poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo1();
                            break;
                        
                        case 2:
                            poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo2();
                            break;
                        
                        case 3:
                            poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo3();
                            break;
                        
                        case 4:
                            poseidonPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo4();
                            break;
                    }
                    
                    poseidonPlayer.GetComponent<PlayerCarSoundManager>().unsharedSoundStart = "Nave_" + carSoundIndex;
                    carSoundIndex++;
                    
                    poseidonPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    poseidonPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    poseidonPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;
                    
                    poseidonPlayer.GetComponent<MyCarController>().powerLeftTrail = powerLeftTrails[1];
                    poseidonPlayer.GetComponent<MyCarController>().powerRightTrail = powerRightTrails[1];

                    /*//Sigue el mismo orden que la UI
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().AssignIndex(1);
                    //cameras[playerInfo.playerID].GetComponentInChildren<SpeedParticles>().AssignIndex(1);
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().enabled = true;*/

                    poseidonPlayer.GetComponent<MyCarController>().enabled = true;
                    poseidonPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;

                    cameraIndex.Add(God.Type.Poseidon);

                    break;
                
                case God.Type.Kali:

                    /*if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                        kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    else
                        kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();*/
                    
                    switch (carSoundIndex)
                    {
                        case 1:
                            kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo1();
                            break;
                        
                        case 2:
                            kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo2();
                            break;
                        
                        case 3:
                            kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo3();
                            break;
                        
                        case 4:
                            kaliPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo4();
                            break;
                    }
                    
                    kaliPlayer.GetComponent<PlayerCarSoundManager>().unsharedSoundStart = "Nave_" + carSoundIndex;
                    carSoundIndex++;
                    
                    kaliPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice; 
                    kaliPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    kaliPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;
                    
                    kaliPlayer.GetComponent<MyCarController>().powerLeftTrail = powerLeftTrails[2];
                    kaliPlayer.GetComponent<MyCarController>().powerRightTrail = powerRightTrails[2];

                    /*//Sigue el mismo orden que la UI
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().AssignIndex(2);
                    //cameras[playerInfo.playerID].GetComponentInChildren<SpeedParticles>().AssignIndex(2);
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().enabled = true;*/


                    kaliPlayer.GetComponent<MyCarController>().enabled = true;
                    kaliPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;

                    cameraIndex.Add(God.Type.Kali);
                    break;
                
                case God.Type.Thor:
                    
                    /*if (playerInfo.controlType == IncontrolProvider.ControlType.Gamepad)
                        thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    else
                        thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();*/
                    
                    switch (carSoundIndex)
                    {
                        case 1:
                            thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo1();
                            break;
                        
                        case 2:
                            thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo2();
                            break;
                        
                        case 3:
                            thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo3();
                            break;
                        
                        case 4:
                            thorPlayer.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.Eduardo4();
                            break;
                    }
                    
                    thorPlayer.GetComponent<PlayerCarSoundManager>().unsharedSoundStart = "Nave_" + carSoundIndex;
                    carSoundIndex++;
                    
                    thorPlayer.GetComponent<IncontrolProvider>().InputDevice = playerInfo.inputDevice;
                    thorPlayer.GetComponent<IncontrolProvider>().controlType = playerInfo.controlType;
                    thorPlayer.GetComponent<IncontrolProvider>().playerID = playerInfo.playerID;
                    
                    thorPlayer.GetComponent<MyCarController>().powerLeftTrail = powerLeftTrails[3];
                    thorPlayer.GetComponent<MyCarController>().powerRightTrail = powerRightTrails[3];

                    /*//Sigue el mismo orden que la UI
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().AssignIndex(3);
                    //cameras[playerInfo.playerID].GetComponentInChildren<SpeedParticles>().AssignIndex(3);
                    cameras[playerInfo.playerID].GetComponent<CameraPostProcess>().enabled = true;*/

                    thorPlayer.GetComponent<MyCarController>().enabled = true;
                    thorPlayer.GetComponent<PlayerCarSoundManager>().enabled = true;

                    cameraIndex.Add(God.Type.Thor);
                    break;
            }   
        }
    }
    
    private void SplitScreen(int players)
    {

        switch (players)
        {
            case 1:
                cameras[(int)cameraIndex[0]].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                cameras[(int)cameraIndex[0]].SetActive(true);
                
                UICameras[(int)cameraIndex[0]].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                
                //No sigue el mismo criterio que las anteriores cámaras
                cinematicCameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                break;

            case 2:

                cameras[(int)cameraIndex[0]].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
                cameras[(int)cameraIndex[0]].SetActive(true);

                cameras[(int)cameraIndex[1]].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
                cameras[(int)cameraIndex[1]].SetActive(true);
                
                UICameras[(int)cameraIndex[0]].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1f);
                UICameras[(int)cameraIndex[1]].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1f);
                
                //No sigue el mismo criterio que las anteriores cámaras
                cinematicCameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1f);
                cinematicCameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1f);
                break;

            case 3:
                cameras[(int)cameraIndex[0]].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[(int)cameraIndex[0]].SetActive(true);

                cameras[(int)cameraIndex[1]].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[(int)cameraIndex[1]].SetActive(true);

                cameras[(int)cameraIndex[2]].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                cameras[(int)cameraIndex[2]].SetActive(true);

                logoCamera.GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                logoCamera.SetActive(true);
                logoCanvas.SetActive(true);
                
                UICameras[(int)cameraIndex[0]].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                UICameras[(int)cameraIndex[1]].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                UICameras[(int) cameraIndex[2]].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                
                //No sigue el mismo criterio que las anteriores cámaras
                cinematicCameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cinematicCameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cinematicCameras[2].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                break;

            case 4:
                cameras[(int)cameraIndex[0]].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[(int)cameraIndex[0]].SetActive(true);

                cameras[(int)cameraIndex[1]].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[(int)cameraIndex[1]].SetActive(true);

                cameras[(int)cameraIndex[2]].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                cameras[(int)cameraIndex[2]].SetActive(true);

                cameras[(int)cameraIndex[3]].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                cameras[(int)cameraIndex[3]].SetActive(true);
                
                UICameras[(int)cameraIndex[0]].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                UICameras[(int)cameraIndex[1]].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                UICameras[(int)cameraIndex[2]].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                UICameras[(int)cameraIndex[3]].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                
                //No sigue el mismo criterio que las anteriores cámaras
                cinematicCameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cinematicCameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cinematicCameras[2].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                cinematicCameras[3].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                break;
        }
    }

    public void SetCameraParent()
    {
        foreach (var playerInfo in StoreGodInfo.Instance.playerInfo)
        {
            switch (playerInfo.godType)
            {
                case God.Type.Anubis:
                    cinematicCameras[playerInfo.playerID].transform.parent = parentCameras[0];
                    break;
                
                case God.Type.Poseidon:
                    cinematicCameras[playerInfo.playerID].transform.parent = parentCameras[1];
                    break;

                case God.Type.Kali:
                    cinematicCameras[playerInfo.playerID].transform.parent = parentCameras[2];
                    break;
                
                case God.Type.Thor:
                    cinematicCameras[playerInfo.playerID].transform.parent = parentCameras[3];
                    break;
            }
        }
    }

    public void ChangeCameraPosition()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cinematicCameras[i].transform.localPosition = new Vector3(0,0,cinematicCameras[i].transform.localPosition.z);
        }
    }

}
