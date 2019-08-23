using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SimpleController : MonoBehaviour
{
    private Rigidbody rb;
    public float speedForce = 80f;
    public float turnSpeed = 50f;
    public float acceleration;
    public float steerInput;
    public bool WaitingToTeleport { set; get; }
    InputDevice device;

    [Header("*Input Info*")]
    private float accelerationInput;
    private float steeringInput;
    private float brakeInput;
    private float handBrakeInput;
    private bool boostInput;
    private bool resetInput;
    private bool jumpInput;

    [Tooltip("Vector de velocidad local del coche")] public Vector3 velocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        WaitingToTeleport = false;
    }

    private void Start()
    {
        device = InputManager.ActiveDevice;
    }

    void Update()
    {
        device = InputManager.ActiveDevice;
        acceleration = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Control(device);
        Accelerate();
        Steering();
    }

    public void Control(InputDevice device)
    {
        boostInput = device.Action1.IsPressed;
        accelerationInput = device.RightTrigger.Value;
        steeringInput = device.LeftStick.Value.x;
        brakeInput = device.RightBumper.Value;
        handBrakeInput = device.LeftTrigger.Value;
        jumpInput = device.LeftBumper.IsPressed;
        resetInput = device.Action4.IsPressed;

    }

    private void Accelerate()
    {
        rb.AddForce(transform.forward * accelerationInput * speedForce, ForceMode.Acceleration);
    }

    private void Steering()
    {
            if (Mathf.Abs(steeringInput) < 0.01f) // No se está girando
            {
                rb.angularVelocity *= 0.95f; // Reducir velocidad angular
            }
            else
            {
                if (velocity.z >= -1f)
                    rb.AddRelativeTorque(0f, steeringInput * turnSpeed, 0f);
                else
                    rb.AddRelativeTorque(0f, -steeringInput * turnSpeed, 0f);
            }
    }

}
