using System;
using InControl;
using UnityEngine;

//Clase que con controla el comportamiento de la cámara con respecto a un target.
public class CameraController : MonoBehaviour, IControllable
{
    public bool activeDevice; //Solo para hacer pruebas

    //Objetivo al que seguir
    public GameObject target;

    //Controla la rotación de la cámara
    [Header("Rotation control")]
    public float maxRotationX = 4;
    public float maxRotationZ = 2;
    public float smoothRotationTransition = 0.5f;

    //Controla la altura a la que se posicionará la cámara según el comportamiento del target
    [Header("Height control")]
    public float defaultHeight = 3;
    public float turboHeight = 3.5f;
    public float smoothHeightTransition = 0.5f;

    //Controla la distancia a la que se posicionará la cámara según el comportamiento del target;
    [Header("Distance control")]
    public float defaultDistance = 6;
    public float turboDistance = 8;
    public float smoothGetAwayTransition = 0.5f;


    private int changeTransition, changeTransitionInRotation;
    private float currentRotationX, currentRotationZ; //La x es rotación hacia arriba y abajo. La z es rotación hacia izquierda y derecha
    private float currentDistance, currentHeight;
    private float transitionGetAwayValue, transitionHeightValue, transitionRotationValueZ, transitionRotationValueX;

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + currentHeight, transform.position.z);
        
        if (activeDevice)
        {
            GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
            Core.Input.AssignControllable(GetComponent<IncontrolProvider>(),this);
            InitCamera();
        } 
    }

    private void OnEnable()
    {
        ConnectDisconnectManager.InitCamera += InitCamera;
        ConnectDisconnectManager.ConnectCarControllerDelegate += ConnectCamera;
        ConnectDisconnectManager.DisconnectCarControllerDelegate += DisconnectCamera;
    }

    public void InitCamera()
    {
        currentDistance = defaultDistance;
        currentHeight = defaultHeight;
        
        //Se incializa la posición inicial de la cámara con respecto al jugador
        transform.position = target.transform.position - new Vector3(target.transform.forward.x, 0, target.transform.forward.z) * currentDistance;
        transform.position = new Vector3(transform.position.x, transform.position.y + currentHeight, transform.position.z);
        transform.forward = target.transform.forward;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        
        //Buscar un mejor sitio para esto o cuando cepa arregle lo del bindeo de los servicios comunes, cambiarlo
        Atto.Bind<IInputService,OwnInputProvider>();

        ConnectDisconnectManager.ConnectCarControllerDelegate += ConnectCamera;
        ConnectDisconnectManager.DisconnectCarControllerDelegate += DisconnectCamera;
    }

    private void OnDisable()
    {
        ConnectDisconnectManager.ConnectCarControllerDelegate -= ConnectCamera;
        ConnectDisconnectManager.DisconnectCarControllerDelegate -= DisconnectCamera;
    }

    public void Control(IDevice device)
    {
        if (target != null)
        {
            //Calcula el punto trasero de la cámara con respecto a su target
            transform.position = target.transform.position - new Vector3(target.transform.forward.x, 0, target.transform.forward.z) * currentDistance;
            
            //Calcula el punto de la cámara según la altura
            transform.position = new Vector3(transform.position.x, transform.position.y + currentHeight, transform.position.z);
            transform.forward = target.transform.forward;
            
            //Rota la cámara  según la inclinación proporcionada en los ejes X y Z
            transform.rotation = Quaternion.Euler(currentRotationX, transform.rotation.eulerAngles.y, currentRotationZ);

            //Si pulsamos el turbo, la cámara deberá alejarse del target poco a poco
            if (device.State.RightTrigger.IsHeld)
            {

                if (device.State.Jump.IsHeld)
                {
                    //En caso de que no se pulse el turbo, la cámara vuelve a su posición original, además de inclinar la cámara
                    if (changeTransition != 1)
                    {
                        changeTransition = 1;
                        transitionGetAwayValue = 0;
                        transitionHeightValue = 0;
                        transitionRotationValueX = 0;
                    }

                    //Actualiza los valores con un Lerp para mantener un suavizado
                    transitionGetAwayValue += Time.deltaTime * smoothGetAwayTransition;
                    currentDistance = Mathf.Lerp(currentDistance, turboDistance, transitionGetAwayValue);

                    transitionHeightValue += Time.deltaTime * smoothHeightTransition;
                    currentHeight = Mathf.Lerp(currentHeight, turboHeight, transitionHeightValue);

                    transitionRotationValueX += Time.deltaTime * smoothRotationTransition;
                    currentRotationX = Mathf.Lerp(currentRotationX, maxRotationX, transitionRotationValueX);
                }
                else
                {
                    if (changeTransition != 2)
                    {
                        changeTransition = 2;
                        transitionHeightValue = 0;
                        transitionGetAwayValue = 0;
                    }

                    transitionGetAwayValue += Time.deltaTime * smoothGetAwayTransition;
                    currentDistance = Mathf.Lerp(currentDistance, defaultDistance, transitionGetAwayValue);

                    transitionHeightValue += Time.deltaTime * smoothHeightTransition;
                    currentHeight = Mathf.Lerp(currentHeight, defaultHeight, transitionHeightValue);

                    transitionRotationValueX += Time.deltaTime * smoothRotationTransition;
                    currentRotationX = Mathf.Lerp(currentRotationX, maxRotationX, transitionRotationValueX);
                }
            }
            else
            {
                if (changeTransition != 3)
                {
                    changeTransition = 3;
                    transitionGetAwayValue = 0;
                    transitionHeightValue = 0;
                    transitionRotationValueX = 0;
                }

                transitionGetAwayValue += Time.deltaTime * smoothGetAwayTransition;
                currentDistance = Mathf.Lerp(currentDistance, defaultDistance, transitionGetAwayValue);

                transitionHeightValue += Time.deltaTime * smoothHeightTransition;
                currentHeight = Mathf.Lerp(currentHeight, defaultHeight, transitionHeightValue);

                transitionRotationValueX += Time.deltaTime * smoothRotationTransition;
                currentRotationX = Mathf.Lerp(currentRotationX, 0, transitionRotationValueX);
            }

            //Si el jugador gira, la cámara rota de forma suave
            if(device.State.Horizontal.Value < -0.1f)
            {
                if (changeTransitionInRotation != 1)
                {
                    changeTransitionInRotation = 1;
                    transitionRotationValueZ = 0;
                }

                transitionRotationValueZ += Time.deltaTime * smoothRotationTransition;
                currentRotationZ = Mathf.Lerp(currentRotationZ, maxRotationZ, transitionRotationValueZ);
            }
            else if(device.State.Horizontal.Value > 0.1f)
            {
                if (changeTransitionInRotation != -1)
                {
                    changeTransitionInRotation = -1;
                    transitionRotationValueZ = 0;
                }

                transitionRotationValueZ += Time.deltaTime * smoothRotationTransition;
                currentRotationZ = Mathf.Lerp(currentRotationZ, -maxRotationZ, transitionRotationValueZ);
            }
            else
            {

                if (changeTransitionInRotation != 0)
                {
                    changeTransitionInRotation = 0;
                    transitionRotationValueZ = 0;
                }

                transitionRotationValueZ += Time.deltaTime * smoothRotationTransition;
                currentRotationZ = Mathf.Lerp(currentRotationZ, 0, transitionRotationValueZ);
            }
        }

    }

    public void ConnectCamera()
    {
        Core.Input.AssignControllable(GetComponent<IncontrolProvider>(),this);
    }

    public void DisconnectCamera()
    {
        Core.Input.UnassignControllable(GetComponent<IncontrolProvider>(),this);
    }
}
