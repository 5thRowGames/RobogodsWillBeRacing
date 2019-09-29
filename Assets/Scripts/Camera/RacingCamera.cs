using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingCamera : MonoBehaviour, IControllable
{
    Transform rootNode;
    Transform carCam;
    Transform car;
    Rigidbody rb;

    [Tooltip("If car speed is below this value, then the camera will default to looking forwards.")]
    public float rotationThreshold = 1f;

    [Tooltip("How closely the camera follows the car's position. The lower the value, the more the camera will lag behind.")]
    public float cameraStickiness = 10.0f;

    [Tooltip("How closely the camera matches the car's velocity vector. The lower the value, the smoother the camera rotations, but too much results in not being able to see where you're going.")]
    public float cameraRotationSpeed = 5.0f;

    #region Unity Events

    void Awake()
    {
        carCam = GetComponentInChildren<Camera>().transform;
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
        Quaternion look;

        // Moves the camera to match the car's position.
        rootNode.position = Vector3.Lerp(rootNode.position, car.position, cameraStickiness * Time.fixedDeltaTime);

        // If the car isn't moving, default to looking forwards. Prevents camera from freaking out with a zero velocity getting put into a Quaternion.LookRotation
        if (rb.velocity.magnitude < rotationThreshold)
            look = Quaternion.LookRotation(car.forward);
        else
            look = Quaternion.LookRotation(rb.velocity.normalized);

        // Rotate the camera towards the velocity vector.
        look = Quaternion.Slerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
        rootNode.rotation = look;
    }

    public void Control(IDevice controller)
    {

    }
}
