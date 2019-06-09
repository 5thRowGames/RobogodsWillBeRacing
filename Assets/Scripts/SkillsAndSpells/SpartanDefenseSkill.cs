using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpartanDefenseSkill : SkillBase
{
    public GameObject spartanDefenseObject;

    private Sequence pushSequence;
    
    //Borrar
    public LayerMask pruebaLayer;
    
    public override void Effect()
    {
        spartanDefenseObject.SetActive(true);
        //pushSequence.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            pushSequence = DOTween.Sequence();
            pushSequence.Append(transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f));
            pushSequence.Append(transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().freezeRotation = true;
        if (pruebaLayer == (pruebaLayer | (1 << other.gameObject.layer)))
        {
            Debug.Log("Salgo");
            pushSequence = DOTween.Sequence();
            pushSequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f));
            pushSequence.Append(transform.DOScale(new Vector3(1, 1, 1), 0.5f));
            
            Vector3 direction = (other.transform.position - transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForceAtPosition(direction * 10f,other.transform.position,ForceMode.Impulse);
        }
    }
}
