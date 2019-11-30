using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySuspensionController : MonoBehaviour
{
    public float wheelHeight = 1f;
    public float upForce = 10f;
    public Transform[] carCorners;
    public Rigidbody rb;

    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        Suspension();
    }

    private void Suspension()
    {
        Ray ray = new Ray(transform.position, transform.position - transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, wheelHeight))
        {
            float proportionalHeight = (wheelHeight - hit.distance) / wheelHeight;
            Vector3 appliedForce = hit.normal * proportionalHeight * upForce;
            rb.AddRelativeForce(appliedForce, ForceMode.Acceleration);
        }
    }
}
