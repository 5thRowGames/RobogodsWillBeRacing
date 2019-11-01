using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCarCollisionsController : MonoBehaviour
{
    private Rigidbody rb; // Rigidbody de este coche
    private int portalLayer; // Layer de los portales
    private RigidbodyConstraints priorConstraints; // Restricciones del rigidbody antes de chocar con otro coche
    private Vector3 myVelocity;
    private float collisionAngle;
    [SerializeField][Tooltip("Fuerza que se ejerce sobre el coche en los choques frontales")] private float frontalForce = 10f;
    [SerializeField][Tooltip("Ángulo máximo de choque para reducir velocidad")] private float maxAngle = 8f;
    [SerializeField][Tooltip("Ángulo máximo para considerar choque frontal")] private float maxFrontalAngle = 30f;
    [SerializeField][Tooltip("Velocidad máxima")] private float maxSpeed = 30f;
    [SerializeField][Tooltip("Factor de reducción de velocidad en choque frontal")][Range(0.01f, 1f)] private float reductionSpeedFactor = 0.4f;
    private float minSpeedForce = 10f;
    private float maxSpeedForce;
    public MyCarController myCarController;
    public float force;
    public float distance;
    private int numberOfCorners;
    private List<Transform> corners;
    private List<RaycastHit> hitList;

    #region UnityEvents

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        portalLayer = LayerMask.NameToLayer("Portal");
        maxSpeedForce = GetComponent<MyCarController>().speedForce;
        hitList = new List<RaycastHit>();

        if (myCarController != null)
        {
            numberOfCorners = myCarController.carCorners.Count;
            corners = myCarController.carCorners;
            for (int i = 0; i < numberOfCorners; i++)
                hitList.Add(new RaycastHit());
        }

    }

    private void FixedUpdate()
    {
        myVelocity = rb.velocity;

        //for(int i = 0; i < numberOfCorners; i++)
        //{
        //    if (i % 2 == 0) // par - izquierda
        //        Suspension(corners[i], -transform.right, hitList[i]);
        //    else // impar - derecha
        //        Suspension(corners[i], transform.right, hitList[i]);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("MyCarCollisionsController Collision");
        int otherLayer = collision.gameObject.layer;

        AkSoundEngine.PostEvent("Impactos_In", gameObject);

        if (otherLayer != portalLayer) // Si no choco contra un portal
        {
            //Debug.Log(collision.gameObject.name);
            priorConstraints = rb.constraints; // Guardo las restricciones previas al choque
            //rb.constraints = RigidbodyConstraints.FreezeRotation; // Congelo la rotación para evitar que el coche cambie su dirección
            //Debug.Log($"Restricciones al colisionar: {rb.constraints.ToString()}");
            CheckFrontCollision(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != portalLayer)
        {
            rb.constraints = priorConstraints;
        }
    }

    #endregion

    private void CheckFrontCollision(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        collisionAngle = (Vector3.Angle(myVelocity, -normal));
        if (collisionAngle > -maxFrontalAngle && collisionAngle < maxFrontalAngle) // Choque frontal
        {
            //    //if (collisionAngle > -maxAngle && collisionAngle < maxAngle && rb.velocity.magnitude > maxSpeed)
            //    //{
            //        Debug.Log("¡Reducción de velocidad!");
            //        rb.angularVelocity *= reductionSpeedFactor * (1.0f - (collisionAngle / maxAngle));
            //        rb.velocity *= reductionSpeedFactor;
            //    //}

            //    Debug.Log("¡Fuerza hacia abajo!");
            //var speedForce = rb.velocity.magnitude > minSpeedForce ? rb.velocity.magnitude : minSpeedForce;      
            var speedForce = Mathf.Clamp(rb.velocity.magnitude, minSpeedForce, maxSpeedForce);
            rb.AddForce(rb.transform.InverseTransformDirection(normal) * frontalForce * speedForce, ForceMode.Force);
            rb.AddForce(-rb.transform.up * frontalForce * speedForce, ForceMode.Force); // Fuerza hacia abajo para contener al coche en el suelo
        }
        else
        {
            rb.AddForce(rb.transform.InverseTransformDirection(normal) * rb.velocity.magnitude, ForceMode.Force);
        }
    }

    private void Suspension(Transform corner, Vector3 direction, RaycastHit hit) //, int index)
    {
        if (Physics.Raycast(corner.position, direction, out hit, distance, myCarController.layerMask))
        {
            //rb.velocity *= 0.9f;
            rb.AddForceAtPosition(force * -direction * (1.0f - (hit.distance / distance)), corner.position);
            Debug.Log($"Obstacle detected at {hit.distance} units.");
        }

        //hitList[index] = hit; // Guardo la información del impacto
    }
}
