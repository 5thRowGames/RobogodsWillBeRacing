using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public float pushForce;
    public LayerMask godsLayerMask; // Máscara de los dioses

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log($"{LayerMask.LayerToName(other.gameObject.layer)}");
        if(LayerMaskUtils.IsLayerIncluded(other.gameObject.layer, godsLayerMask))
        {
            var myCC = other.GetComponent<MyCarController>();
            var velo = other.attachedRigidbody.velocity;
            other.attachedRigidbody.AddForce(-transform.up * pushForce, ForceMode.Acceleration);

            //var pushFactor = Mathf.Clamp01((myCC.speedForce - other.attachedRigidbody.velocity.magnitude) / myCC.speedForce);
            //Debug.Log($"pushFactor = {pushFactor}");
            //if (other.attachedRigidbody.velocity.magnitude < myCC.speedForce / 3f)
            //    other.attachedRigidbody.AddForce(other.transform.forward * pushForce * pushFactor, ForceMode.Acceleration);
        }

    }
}
