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
    public DaggerBehaviourAlone daggerBehaviorAlone;

    public override void Effect()
    {
        daggerBehaviorAlone.Init();
        gameObject.SetActive(true);
        dagger.gameObject.SetActive(true);

    }

    public override void FinishEffect()
    {
        dagger.gameObject.SetActive(false);
        gameObject.SetActive(false);
        ResetSkill();
    }

    public override void ResetSkill()
    {
        dagger.position = daggerSpawn.position;
    }
}
