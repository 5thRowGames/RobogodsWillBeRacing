using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Clase temporal para la división de las cámaras y la asignación de los targets a cada una de estas
public class Lobby : MonoBehaviour
{
    public float timer = 10f;

    public GameObject mainCamera;
    public List<GameObject> cameras;

    public Text timerText;

    private float timerAmount = 0;

    void Start()
    {
        Invoke(nameof(AssignCamera), timer);
        Invoke(nameof(SplitScreen), timer + 1);

        timerText.text = "0";
    }

    private void Update()
    {
        timerAmount += Time.deltaTime;

        timerText.text = string.Format("{0:00}", timerAmount);

        if (timerAmount > timer)
            timerText.enabled = false;
    }

    //Divide la pantalla según el número de jugadores que vayan a jugar la partida
    private void SplitScreen()
    {
        cameras[0].SetActive(false);
        cameras[1].SetActive(false);
        cameras[2].SetActive(false);
        cameras[3].SetActive(false);

        switch (PlayerSelection.players.Count)
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
        for(int i = 0; i < PlayerSelection.players.Count; i++)
        {
            cameras[i].GetComponent<CameraController>().target = PlayerSelection.players[i];
        }
    }

}
