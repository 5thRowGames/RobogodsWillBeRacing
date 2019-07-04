using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCarCollisionsController : MonoBehaviour
{
    //public LayerMask playerLayer; // Layer de los coches
    private Rigidbody rb; // Rigidbody de este coche
    //private int myLayer;
    private RigidbodyConstraints priorConstraints; // Restricciones del rigidbody antes de chocar con otro coche
    //public Vector3 localVelocity;
    //public float minForce = 10f; // Fuerza mínima de choque
    //public float maxForce = 50f; // Fuerza máxima de choque

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //myLayer = gameObject.layer;
    }

    //private void FixedUpdate()
    //{
    //    localVelocity = transform.InverseTransformDirection(rb.velocity);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        int otherLayer = collision.gameObject.layer;

        AkSoundEngine.PostEvent("Impactos_In", gameObject);

        //if(otherLayer == myLayer) // Si choco contra otro coche
        //{
            priorConstraints = rb.constraints; // Guardo las restricciones previas al choque
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Congelo la rotación para evitar que el coche cambie su dirección

            // Aplicar una fuerza de choque
            //Vector3 otherLocalVelocity = collision.transform.InverseTransformDirection(collision.rigidbody.velocity);
            //float otherXVelocity = otherLocalVelocity.x;
            //float localXVelocity = localVelocity.x;
            //float maxX = Mathf.Max(Mathf.Abs(localXVelocity), Mathf.Abs(otherXVelocity));
            //float dirX = Mathf.Sign(localXVelocity) * -1f; // cambio de sentido de la velocidad en X 
            //Vector3 vel = new Vector3(Mathf.Clamp(Mathf.Abs(localXVelocity) * dirX, minForce, maxForce), 0f, 0f);

            //Debug.Log($"Velocidad en X anterior = {localVelocity.x}");
            //rb.AddForce(vel, ForceMode.VelocityChange);
            //Debug.Log($"Velocidad en X posterior = {localVelocity.x}");

        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.layer == myLayer)
        //{
            rb.constraints = priorConstraints;
        //}
            
    }
}
