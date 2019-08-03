using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerThrow : SkillBase
{
    public Transform initPoint;
    public float hammerSpeed;
    
    private void Update()
    {
        transform.Translate(Vector3.forward * hammerSpeed * Time.deltaTime);
    }

    public override void Effect()
    {
        gameObject.SetActive(true);
    }

    public override void FinishEffect()
    {
        gameObject.SetActive(false);
        ResetSkill();
    }

    public override void ResetSkill()
    {
        transform.position = initPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO
        //Hacer que ocurra el rayo y la pérdida de derrape
    }
}
