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
    public float Height { get; private set; }

    #region Unity Events
    private void Start()
    {
        gameObject.name += transform.parent.name;
        transform.parent = null;
        Height = 3f;
    }

    void Update()
    {
        hitSomething = Physics.Raycast(transform.position, -transform.up, out hitInfo, Mathf.Infinity, layerMask);

        transform.position = car.position + Vector3.up * Height; // Sigue al coche por encima del mismo
        transform.rotation = Quaternion.Euler(new Vector3(0f, car.eulerAngles.y, 0f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(hitSomething)
            Gizmos.DrawLine(transform.position, hitInfo.point);
    }
    #endregion
}