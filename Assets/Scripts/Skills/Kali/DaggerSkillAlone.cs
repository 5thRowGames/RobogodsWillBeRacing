using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;

public class DaggerSkillAlone : SkillBase
{
    public Transform daggerSpawn;
    public Transform dagger;
    
    private DaggerBehaviourAlone daggerBehaviorAlone;

    private void Awake()
    {
        daggerBehaviorAlone = dagger.GetComponent<DaggerBehaviourAlone>();
    }

    public override void Effect()
    {
        ResetSkill();
        daggerBehaviorAlone.Init();
        gameObject.SetActive(true);
        dagger.gameObject.SetActive(true);

    }

    public override void FinishEffect()
    {
        dagger.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public override void ResetSkill()
    {
        dagger.position = daggerSpawn.position;
        dagger.rotation = daggerSpawn.rotation;
    }
}
