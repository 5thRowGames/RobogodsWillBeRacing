using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UITween : MonoBehaviour
{
    public ParticleSystem particles;
    public RectTransform rectTransform;
    public RectTransform gear;

    public float duration = 1f;
    public float rotateDuration = 0.3f;

    [Header("Scale")]
    public Vector3 expand;
    public Vector3 contract;

    [Space(5)]
    [Header("Movement")]
    public Vector3 from;
    public Vector3 to;

    private Tween rotateTween;
    

    public void ExpandTween()
    {
        rectTransform.DOScale(expand, duration).SetUpdate(true);
    }

    public void ContractTween()
    {
        rectTransform.DOScale(contract, duration).SetUpdate(true);
    }

    public void Rotate()
    {
        rotateTween = gear.DOLocalRotate(new Vector3(0, 0, -360), rotateDuration, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void StopRotate()
    {
        rotateTween.Kill();
    }

    public void PlayParticles()
    {
        particles.Play();
    }

    public void StopParticles()
    {
        particles.Stop();
    }

}
