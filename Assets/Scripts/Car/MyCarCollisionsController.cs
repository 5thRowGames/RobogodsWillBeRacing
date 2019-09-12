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
        Debug.Log($"Portal layer = {portalLayer}");
    }

    private void FixedUpdate()
    {
        myVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("MyCarCollisionsController Collision");
        int otherLayer = collision.gameObject.layer;
        /*Debug.Log($"otherLayer = {collision.gameObject.layer}");
        Debug.Log($"portalLayer = {portalLayer}");*/
        AkSoundEngine.PostEvent("Impactos_In", gameObject);

            Debug.Log($"Restricciones al colisionar: {rb.constraints.ToString()}");
            CheckFrontCollision(collision);
        }
        else Debug.Log($"He entrado en el portal {collision.gameObject.GetComponent<Portal>().index}");
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != portalLayer)
        {
            Debug.Log($"Saliendo de una colisión");
            rb.constraints = priorConstraints;
            //rb.constraints = RigidbodyConstraints.None;
            Debug.Log($"Restricciones tras salir de colisión: {rb.constraints.ToString()}");
        }
        else Debug.Log($"He salido del portal {collision.gameObject.GetComponent<Portal>().index}");
    }

    #endregion

    private void CheckFrontCollision(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        collisionAngle = (Vector3.Angle(myVelocity, -normal));
        Debug.Log("Collision Angle:" + collisionAngle);
        //if (myVelocity) ;
    }
}
