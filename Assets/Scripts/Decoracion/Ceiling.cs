using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public float pushToTheGroundForce;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log($"{LayerMask.LayerToName(other.gameObject.layer)}");
        var velo = other.attachedRigidbody.velocity;
        other.attachedRigidbody.AddForce(-transform.up * pushToTheGroundForce, ForceMode.Acceleration);
    }
}
