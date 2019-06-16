using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDivision : MonoBehaviour
{
    public int players;
    
    public GameObject mainCamera;
    public List<GameObject> cameras;
    public List<GameObject> cars;

    private void Start()
    {
        SplitScreen();
        AssignCamera();
    }

    private void SplitScreen()
    {
        cameras[0].SetActive(false);
        cameras[1].SetActive(false);
        cameras[2].SetActive(false);
        cameras[3].SetActive(false);

        switch (players)
        {
            case 1:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                cameras[0].SetActive(true);

                mainCamera.SetActive(false);
                break;

            case 2:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
                cameras[0].SetActive(true);

                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
                cameras[1].SetActive(true);

                mainCamera.SetActive(false);
                break;

            case 3:

                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                cameras[0].SetActive(true);

                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                cameras[1].SetActive(true);

                cameras[2].GetComponent<Camera>().rect = new Rect(0, 0, 1f, 0.5f);
                cameras[2].SetActive(true);

                mainCamera.SetActive(false);
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

                mainCamera.SetActive(false);
                break;
        }

    }

    private void AssignCamera()
    {
        for(int i = 0; i < players; i++)
        {
            cameras[i].GetComponent<CameraController>().target = cars[i];
        }
    }
}
