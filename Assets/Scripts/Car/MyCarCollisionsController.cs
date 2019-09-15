using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCarCollisionsController : MonoBehaviour
{
    private Rigidbody rb; // Rigidbody de este coche
    private int portalLayer; // Layer de los portales
    private RigidbodyConstraints priorConstraints; // Restricciones del rigidbody antes de chocar con otro coche
    [SerializeField] private Vector3 myVelocity;
    [SerializeField] private float collisionAngle;

    #region UnityEvents

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        portalLayer = LayerMask.NameToLayer("Portal");
    }

    private void FixedUpdate()
    {
        myVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int otherLayer = collision.gameObject.layer;

        //AkSoundEngine.PostEvent("Impactos_In", gameObject);

        if (otherLayer != portalLayer) // Si no choco contra un portal
        {
            priorConstraints = rb.constraints; // Guardo las restricciones previas al choque
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Congelo la rotación para evitar que el coche cambie su dirección
            CheckFrontCollision(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != portalLayer)
        {
            rb.constraints = priorConstraints;
            //rb.constraints = RigidbodyConstraints.None;
        }
    }

    #endregion

    private void CheckFrontCollision(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        collisionAngle = (Vector3.Angle(myVelocity, -normal));
        //if (myVelocity) ;
    }
}
