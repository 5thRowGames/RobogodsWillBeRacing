using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HidroCannonSkill : SkillBase
{
    public float size;
    public float scaleTime;

    private Tween scaleTween;

    private void OnEnable()
    {
        ResetSkill();
    }

    public override void Effect()
    {
        gameObject.SetActive(true);
        ScaleTween();
    }

    public override void FinishEffect()
    {
        scaleTween.Kill();
        isFinished = true;
        gameObject.SetActive(false);
    }

    private void ScaleTween()
    {
        scaleTween = transform.DOScaleZ(size, scaleTime);
    }

    public override void ResetSkill()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
