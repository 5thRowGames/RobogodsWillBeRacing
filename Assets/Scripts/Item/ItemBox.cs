using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public float respawnTime;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Anubis") || other.CompareTag("Poseidon") || other.CompareTag("Kali") ||
            other.CompareTag("Thor"))
        {
            other.GetComponentInParent<ItemManager>().ChooseItem();
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }
}
