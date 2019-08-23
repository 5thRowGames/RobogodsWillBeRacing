using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NordicWinds : SkillBase
{
    public LayerMask groundLayer;
    public string iceTag;
    public string normalTag;
    public float time;
    
    private int groundID;
    
    //Pruebas, lo suyo es hacerlo con un blend y un parámetro que controle de 0 a 1 como de mezclado esta
    public Material iceMaterial;
    public Material normalMaterial;

    public override void Effect()
    {
        gameObject.SetActive(true);
        StartCoroutine(RestoreTimer());

    }

    public override void FinishEffect()
    {
        RestoreRoads();
        gameObject.SetActive(false);
    }

    public override void ResetSkill()
    {
        throw new System.NotImplementedException();
    }

    private void CheckGround()
    {
        RaycastHit hit;

        groundID = -1;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity,
            groundLayer))
        {
            groundID = LapsManager.Instance.road.FindIndex(x => x == hit.transform.gameObject);
        }
    }

    private void FreezeRoads()
    {
        if (groundID > -1)
        {
            int totalRoads = LapsManager.Instance.road.Count;
            
            for (int i = groundID; i < totalRoads; i++)
            {
                LapsManager.Instance.road[i].GetComponent<MeshRenderer>().material = iceMaterial;
                LapsManager.Instance.road[i].tag = iceTag;
            }
        }
    }

    private void RestoreRoads()
    {
        if (groundID >= -1)
        {
            int totalRoads = LapsManager.Instance.road.Count;

            for (int i = groundID; i < totalRoads; i++)
            {
                LapsManager.Instance.road[i].GetComponent<MeshRenderer>().material = normalMaterial;
                LapsManager.Instance.road[i].tag = normalTag;
            }
        }
    }

    IEnumerator RestoreTimer()
    {
        CheckGround();
        FreezeRoads();
        yield return new WaitForSeconds(time);
        FinishEffect();
    }
}
