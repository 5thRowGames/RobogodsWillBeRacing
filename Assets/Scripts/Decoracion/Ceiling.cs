using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public float pushToTheGroundForce;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{LayerMask.LayerToName(collision.gameObject.layer)}");
        collision.rigidbody.AddForce(transform.up * pushToTheGroundForce, ForceMode.Acceleration);
    }
}
