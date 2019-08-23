using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;

public class DaggerSkill : SkillBase
{
    public Transform target;
    public float distanceToTarget;

    public Transform leftDaggerSpawn;
    public Transform midDaggerSpawn;
    public Transform rightDaggerSpawn;
    
    public GameObject leftDagger;
    public GameObject rightDagger;
    public GameObject midDagger;

    private DaggerBehaviour leftBehaviour;
    private DaggerBehaviour rightBehaviour;
    private DaggerBehaviour midBehaviour;

    private void Awake()
    {
        if (leftDagger != null)
            leftBehaviour = leftDagger.GetComponent<DaggerBehaviour>();
        
        if (rightDagger != null)
            rightBehaviour = rightDagger.GetComponent<DaggerBehaviour>();
        
        if (midDagger != null)
            midBehaviour = midDagger.GetComponent<DaggerBehaviour>();

    }

    public override void Effect()
    {
        ResetSkill();
        rightBehaviour.DoEffect();
        midBehaviour.DoEffect();
        leftBehaviour.DoEffect();
    }

    public override void FinishEffect()
    {
        DOTween.Kill(leftDagger.transform);
        DOTween.Kill(midDagger.transform);
        DOTween.Kill(rightDagger.transform);
        
        leftDagger.SetActive(false);
        rightDagger.SetActive(false);
        midDagger.SetActive(false);
    }

    public override void ResetSkill()
    {
        leftBehaviour.Init(leftDaggerSpawn.position);
        rightBehaviour.Init(rightDaggerSpawn.position);
        midBehaviour.Init(midDaggerSpawn.position);
    }
}
