using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adept : MonoBehaviour
{
    public int manaAmount;
    public float correctGodRespawnTime;
    public float wrongGodRespawnTime;
    public God.Type correctGod;
    
    [Header("God Tags")]
    public string correctGodTag;
    public string wrongGodTag1;
    public string wrongGodTag2;
    public string wrongGodTag3;

    private MeshRenderer model;
    private BoxCollider boxCollider;

    private void Start()
    {
        model = GetComponentInChildren<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(correctGodTag))
        {
            other.GetComponentInParent<PlayerSkillManager>().Mana += manaAmount;
            HUDManager.Instance.UpdateManaBar(correctGod,manaAmount);
            StartCoroutine(respawnTime(correctGodRespawnTime));
        }
        else if (other.CompareTag(wrongGodTag1) || other.CompareTag(wrongGodTag2) || other.CompareTag(wrongGodTag3))
        {
            StartCoroutine(respawnTime(wrongGodRespawnTime));
        }
    }

    IEnumerator respawnTime(float time)
    {
        model.enabled = false;
        boxCollider.enabled = false;
        yield return new WaitForSeconds(time);
        model.enabled = true;
        boxCollider.enabled = true;
    }
}
