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
        
        //hitInfo = new RaycastHit();
    }

    void Update()
    {
        if (car == null) return;

        hitSomething = Physics.Raycast(transform.position, -transform.up, out hitInfo, Mathf.Infinity, layerMask);

        transform.position = car.position + Vector3.up; // Sigue al coche una unidad por encima del mismo
        transform.rotation = Quaternion.identity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(hitSomething)
            Gizmos.DrawLine(transform.position, hitInfo.point);
    }
}
