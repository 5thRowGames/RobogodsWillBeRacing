using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingCamera : MonoBehaviour, IControllable
{
    private Transform rootNode;
    private Camera carCam;
    private Transform car;
    private Rigidbody rb;

    [Tooltip("Umbral mínimo de velocidad para que la cámara siga al coche")]
    public float speedThreshold = 15f;

    [Tooltip("Cuán de cerca sigue la cámara al coche. Cuanto más bajo sea el valor, más retrasada irá la cámara")]
    public float cameraStickiness = 10.0f;

    [Tooltip("Cuán de cerca la cámara sigue el vector de velocidad del coche. Cuanto más bajo sea el valor, más suave será la rotación de la cámara " +
        "pero si es demasiado bajo, habrá momentos en los que no se vea hacia donde va el coche")]
    public float cameraRotationSpeed = 5.0f;

    #region Unity Events

    void Awake()
    {
        carCam = GetComponentInChildren<Camera>();
        rootNode = GetComponent<Transform>();
        car = rootNode.parent.GetComponent<Transform>();
        rb = car.GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Detach the camera so that it can move freely on its own.
        rootNode.parent = null;
    }

    private void OnEnable()
    {
        ConnectDisconnectManager.ConnectCarControllerDelegate += ConnectCamera;
        ConnectDisconnectManager.DisconnectCarControllerDelegate += DisconnectCamera;
    }

    private void OnDisable()
    {
        ConnectDisconnectManager.ConnectCarControllerDelegate -= ConnectCamera;
        ConnectDisconnectManager.DisconnectCarControllerDelegate -= DisconnectCamera;
    }

    #endregion

    public void ConnectCamera()
    {
        Core.Input.AssignControllable(GetComponent<IncontrolProvider>(), this);
    }

    public void DisconnectCamera()
    {
        Core.Input.UnassignControllable(GetComponent<IncontrolProvider>(), this);
    }

    private void FixedUpdate()
    {
        // Rotate the camera towards the velocity vector.
        Quaternion look = Quaternion.LookRotation(rb.velocity.normalized);

        if (rb.velocity.magnitude > speedThreshold)
        {
            // Moves the camera to match the car's position.
            rootNode.position = Vector3.Lerp(rootNode.position, car.position, cameraStickiness * Time.fixedDeltaTime);
            
            look = Quaternion.LookRotation(rb.velocity.normalized);
            look = Quaternion.Lerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
            rootNode.rotation = look;
        }
        else
        {
            Vector3 pos = carCam.WorldToViewportPoint(car.position);

            // El coche se sale de la pantalla
            if(pos.x < 0.1f || pos.x > 0.9f || pos.y < 0.1f || pos.y > 0.9f)
            {                
                look = Quaternion.Lerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
                rootNode.rotation = look;
            }
        }
    }

    public void Control(IDevice controller)
    {

    }
}
