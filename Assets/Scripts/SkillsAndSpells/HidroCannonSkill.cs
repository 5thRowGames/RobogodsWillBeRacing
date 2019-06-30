using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HidroCannonSkill : SkillBase
{
    public float size;
    public float timeToScale;

    public override void Effect()
    {
        Scale();
    }

    public override void FinishEffect()
    {
        throw new NotImplementedException();
    }

    public void Scale()
    { 
        transform.DOScaleZ(size, timeToScale);
    }
}
