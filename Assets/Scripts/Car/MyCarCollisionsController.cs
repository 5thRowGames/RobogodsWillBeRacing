using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCarCollisionsController : MonoBehaviour
{
    private Rigidbody rb; // Rigidbody de este coche
    private int portalLayer; // Layer de los portales
    private RigidbodyConstraints priorConstraints; // Restricciones del rigidbody antes de chocar con otro coche


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        portalLayer = LayerMask.NameToLayer("Portal");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("MyCarCollisionsController Collision");
        int otherLayer = collision.gameObject.layer;
        Debug.Log($"otherLayer = {collision.gameObject.layer}");
        Debug.Log($"portalLayer = {portalLayer}");
        AkSoundEngine.PostEvent("Impactos_In", gameObject);

        if (otherLayer != 15) // Si no choco contra un portal
        {
            Debug.Log(collision.gameObject.name);
            priorConstraints = rb.constraints; // Guardo las restricciones previas al choque
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Congelo la rotación para evitar que el coche cambie su dirección
        }
        else Debug.Log($"He entrado en el portal {collision.gameObject.GetComponent<Portal>().index}");
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != 15)
        {
            rb.constraints = priorConstraints;
        }
        else Debug.Log($"He salido del portal {collision.gameObject.GetComponent<Portal>().index}");
    }
}
