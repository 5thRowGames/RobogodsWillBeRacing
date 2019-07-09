﻿using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class SpawnDiosesParaAlberto : MonoBehaviour
{

    public List<GameObject> dioses;
    public List<Transform> spawnPositions;
    public List<GameObject> cameras;
    public GameObject mainCamera;
    public int playersToPlay;

    private int playerOrder = 0;
    private MyPlayerActions keyboardListener;
    private MyPlayerActions joystickListener;
    private bool keyboardSelected;
    public bool updateBool;

    private void OnEnable()
    {
        keyboardListener = MyPlayerActions.BindKeyboard();
        joystickListener = MyPlayerActions.BindControls();
        keyboardSelected = false;
    }

    void Update()
    {
        if (!updateBool)
        {
            if (JoinButtonWasPressed(joystickListener))
            {   
                var inputDevice = InputManager.ActiveDevice;

                if (RaceManager.Instance.players < 4)
                {
                    RaceManager.Instance.players++;
                    GameObject dios = Instantiate(dioses[playerOrder], spawnPositions[playerOrder].position, spawnPositions[playerOrder].rotation);
                    dios.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindControls();
                    dios.GetComponent<IncontrolProvider>().InputDevice = inputDevice;
                    dios.GetComponent<MyCarController>().ConnectCar();
                    cameras[playerOrder].GetComponent<CameraController>().target = dios;
                    playerOrder++;
                }
            }
        
            //Prueba
            if (JoinButtonWasPressed(keyboardListener))
            {

                if (RaceManager.Instance.players < 4 && !keyboardSelected)
                {
                    keyboardSelected = true;
                    RaceManager.Instance.players++;
                    GameObject dios = Instantiate(dioses[playerOrder], spawnPositions[playerOrder].position, spawnPositions[playerOrder].rotation);
                    dios.GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
                    dios.GetComponent<IncontrolProvider>().InputDevice = null;
                    dios.GetComponent<MyCarController>().ConnectCar();
                    cameras[playerOrder].GetComponent<CameraController>().target = dios;
                    playerOrder++;
                }
            }

            if (joystickListener.Special.IsPressed || keyboardListener.Special.IsPressed)
            {
                SplitScreen(playersToPlay);
            }

        }
    }
    
    private void SplitScreen(int players)
    {
        updateBool = true;

        switch (players)
        {
            case 1:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                cameras[0].SetActive(true);
                cameras[0].GetComponent<CameraController>().ConnectCamera();

                break;

            case 2:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
                cameras[0].SetActive(true);
                cameras[0].GetComponent<CameraController>().ConnectCamera();

                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
                cameras[1].SetActive(true);
                cameras[1].GetComponent<CameraController>().ConnectCamera();
                
                break;

            case 3:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[0].SetActive(true);
                cameras[0].GetComponent<CameraController>().ConnectCamera();

                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[1].SetActive(true);
                cameras[1].GetComponent<CameraController>().ConnectCamera();

                cameras[2].GetComponent<Camera>().rect = new Rect(0, 0, 1f, 0.5f);
                cameras[2].SetActive(true);
                cameras[2].GetComponent<CameraController>().ConnectCamera();
                
                break;

            case 4:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[0].SetActive(true);
                cameras[0].GetComponent<CameraController>().ConnectCamera();

                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[1].SetActive(true);
                cameras[1].GetComponent<CameraController>().ConnectCamera();

                cameras[2].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                cameras[2].SetActive(true);
                cameras[2].GetComponent<CameraController>().ConnectCamera();

                cameras[3].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                cameras[3].SetActive(true);
                cameras[3].GetComponent<CameraController>().ConnectCamera();
                
                break;
        }

        mainCamera.SetActive(false);
    }
    
    bool JoinButtonWasPressed( MyPlayerActions actions )
    {
        return actions.Gas.WasPressed;
    }
}
