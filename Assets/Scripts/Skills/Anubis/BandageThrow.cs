using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BandageThrow : SkillBase
{
    //TODO NO SE BIEN COMO IMPLEMENTAR VISUALMENTE ESTA HABILIDAD
    public float size;
    public float scaleTime;
    
    private Tween scaleTween;
    
    public override void Effect()
    {
        gameObject.SetActive(true);
        ScaleTween();
    }

    public override void FinishEffect()
    {
        scaleTween.Kill();
        gameObject.SetActive(false);
    }

    public override void ResetSkill()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    
    private void ScaleTween()
    {
        scaleTween = transform.DOScaleZ(size, scaleTime);
    }
}
