using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpartanDefenseSkill : SkillBase
{
    //El objeto debería ser todo, que no tiene hijos
    //public GameObject spartanDefenseObject;

    private Sequence pushSequence;

    public override void Effect()
    {
        gameObject.SetActive(true);
    }

    public override void FinishEffect()
    {
        pushSequence.Kill();
        gameObject.SetActive(false);
        ResetSkill();
    }

    public override void ResetSkill()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Aqui se tendría que hacer algo
        
        
        /*other.GetComponent<Rigidbody>().freezeRotation = true;
        if (pruebaLayer == (pruebaLayer | (1 << other.gameObject.layer)))
        {
            pushSequence = DOTween.Sequence();
            pushSequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f));
            pushSequence.Append(transform.DOScale(new Vector3(1, 1, 1), 0.5f));
            
            Vector3 direction = (other.transform.position - transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForceAtPosition(direction * 10f,other.transform.position,ForceMode.Impulse);
        }*/

        //Tengo que hacer algo referente a empujar
    }
}
