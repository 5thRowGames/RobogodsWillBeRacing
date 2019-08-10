using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    private Rigidbody rb;
    public float speedForce = 20f;
    public float rotationForce = 150f;
    public float acceleration;
    public float steerInput;
    public bool WaitingToTeleport { set; get; }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        WaitingToTeleport = false;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Accelerate();
        Steer();
    }

    private void Accelerate()
    {
        rb.AddForce(transform.forward * acceleration * speedForce, ForceMode.Acceleration);
    }

    private void Steer()
    {
        rb.AddRelativeTorque(0f, steerInput * rotationForce, 0f);
    }
}
