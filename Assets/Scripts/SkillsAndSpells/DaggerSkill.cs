using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;

public class DaggerSkill : SkillBase
{
    public Transform target;
    public GameObject leftDagger;
    public GameObject rightDagger;
    public GameObject midDagger;

    private bool move = true;

    private DaggerBehaviour leftBehaviour;
    private DaggerBehaviour rightBehaviour;
    private DaggerBehaviour midBehaviour;
    private Action stopDaggersCallback;

    private void Awake()
    {
        if (leftDagger != null)
            leftBehaviour = leftDagger.GetComponent<DaggerBehaviour>();
        
        if (rightDagger != null)
            rightBehaviour = rightDagger.GetComponent<DaggerBehaviour>();
        
        if (midDagger != null)
            midBehaviour = midDagger.GetComponent<DaggerBehaviour>();

    }

    private void Start()
    {
        stopDaggersCallback += StopDaggers;
        leftBehaviour.Init(leftDagger.transform.position, stopDaggersCallback);
        rightBehaviour.Init(rightDagger.transform.position, stopDaggersCallback);
        midBehaviour.Init(midDagger.transform.position, stopDaggersCallback);
    }

    private void Update()
    {
        if(move)
            target.Translate(40f * Time.deltaTime * Vector3.forward);
        
    }
    
    public override void Effect()
    {
        if (leftDagger != null)
            leftBehaviour.DoEffect();
        
        if (rightDagger != null)
            rightBehaviour.DoEffect();
        
        if (midDagger != null)
            midBehaviour.DoEffect();
    }

    public override void FinishEffect()
    {
        throw new NotImplementedException();
    }

    private void StopDaggers()
    {
        DOTween.Kill(leftDagger.transform);
        DOTween.Kill(midDagger.transform);
        DOTween.Kill(rightDagger.transform);
    }

}
