using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHelper : MonoBehaviour
{
    public Transform car;
    public LayerMask layerMask;
    private RaycastHit hitInfo;
    [SerializeField] private bool hitSomething;
    [SerializeField] private float carYRotation;

    void Start()
    {
        hitSomething = false;
        hitInfo = new RaycastHit();
    }

    void Update()
    {
        if (car == null) return;

        transform.position = car.position + Vector3.up; // Sigue al coche una unidad por encima del mismo
        carYRotation = car.eulerAngles.y;
        //transform.eulerAngles = new Vector3(0f, car.eulerAngles.y, 0f); // Copia la rotación del coche en Y
        KeepParallelToCircuit();
        //transform.forward = car.forward;
    }

    private void KeepParallelToCircuit()
    {
        hitSomething = Physics.Raycast(transform.position, -transform.up, out hitInfo, Mathf.Infinity, layerMask);

        if (hitSomething)
            transform.eulerAngles = new Vector3(hitInfo.transform.eulerAngles.x, carYRotation, hitInfo.transform.eulerAngles.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(hitSomething)
            Gizmos.DrawLine(transform.position, hitInfo.point);
    }
}
