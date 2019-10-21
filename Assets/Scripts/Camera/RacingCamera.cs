﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingCamera : MonoBehaviour, IControllable
{
    private Transform rootNode;
    private Camera carCam;
    private Transform car;
    private Rigidbody rb;
    private Transform carReference;
    //private Vector3 offset = new Vector3(0f, 1.4f, 1f);

    [Tooltip("Umbral mínimo de velocidad para que la cámara siga al coche")]
    public float speedThreshold = 15f;
    [Tooltip("Cuán de cerca sigue la cámara al coche. Cuanto más bajo sea el valor, más retrasada irá la cámara")]
    public float cameraStickiness = 10.0f;
    [Tooltip("Cuán de cerca la cámara sigue el vector de velocidad del coche. Cuanto más bajo sea el valor, más suave será la rotación de la cámara " +
        "pero si es demasiado bajo, habrá momentos en los que no se vea hacia donde va el coche")]
    public float cameraRotationSpeed = 5.0f;
    [Tooltip("Distancia máxima que la cámara permite que el coche se aleje sin seguirlo")]
    public float maxDistance;

    #region Unity Events

    void Awake()
    {
        carCam = GetComponentInChildren<Camera>();
        rootNode = GetComponent<Transform>();
        car = rootNode.parent.GetComponent<Transform>();
        rb = car.GetComponent<Rigidbody>();
        carReference = car.GetComponentInChildren<CameraReference>().transform;
        //carReference.position = car.transform.position + offset;
        //if (carReference == null)
        //    carReference = car;
    }

    void Start()
    {
        // Se saca el prefab con cámara de la jerarquía del coche para que pueda moverse libremente por su cuenta
        rootNode.parent = null;
        transform.localScale = Vector3.one; // Su escala se había transformado en (2, 1, 3) por ser hijo del coche
        carCam.transform.localPosition = new Vector3(0f, 4f, -10f);
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

    private Vector3 posRefVelocity;
    private void FixedUpdate()
    {
        // Se rota la cámara hacia el vector de velocidad
        Quaternion look = Quaternion.LookRotation(rb.velocity.normalized);

        if (rb.velocity.magnitude > speedThreshold)
        {
            // La cámara sigue la posición del coche
            //rootNode.position = Vector3.Lerp(rootNode.position, car.position, cameraStickiness * Time.fixedDeltaTime);
            rootNode.position = Vector3.SmoothDamp(rootNode.position, carReference.position, ref posRefVelocity, .1f);

            look = Quaternion.LookRotation(Vector3.Lerp(car.forward, rb.velocity.normalized, Mathf.Clamp01(rb.velocity.magnitude / 10f)));
            look = Quaternion.Lerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
            rootNode.rotation = look;
        }
        else
        {
            // La cámara sigue la posición del coche
            //rootNode.position = Vector3.Lerp(rootNode.position, car.position, cameraStickiness * Time.fixedDeltaTime);
            rootNode.position = Vector3.SmoothDamp(rootNode.position, carReference.position, ref posRefVelocity, .5f);

            look = Quaternion.LookRotation(Vector3.Lerp(car.forward, rb.velocity.normalized, Mathf.Clamp01(rb.velocity.magnitude / 10f)));
            look = Quaternion.Lerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
            rootNode.rotation = look;
        }

        //private void FixedUpdate()
        //{
        //    // Se rota la cámara hacia el vector de velocidad
        //    Quaternion look = Quaternion.LookRotation(rb.velocity.normalized);

        //    if (rb.velocity.magnitude > speedThreshold)
        //    {
        //        // Moves the camera to match the car's position.
        //        rootNode.position = Vector3.Lerp(rootNode.position, car.position, cameraStickiness * Time.fixedDeltaTime);

        //        look = Quaternion.LookRotation(rb.velocity.normalized);
        //        look = Quaternion.Lerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
        //        rootNode.rotation = look;
        //    }
        //    else
        //    {
        //        Vector3 pos = carCam.WorldToViewportPoint(car.position);

        //        // El coche se sale de la pantalla
        //        if(pos.x < 0.1f || pos.x > 0.9f || pos.y < 0.1f || pos.y > 0.9f)
        //        {                
        //            look = Quaternion.Lerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
        //            rootNode.rotation = look;
        //        }
        //        if(Vector3.Distance(rootNode.position, car.position) > maxDistance)
        //        {

        //        }
        //    }
    }

    public void Control(IDevice controller)
    {

    }
}
