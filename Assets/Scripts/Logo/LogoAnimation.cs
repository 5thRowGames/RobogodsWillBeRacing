using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    public GameObject baseLogo;
    public GameObject numberLogo;
    public GameObject topLeftScrew;
    public GameObject topRightScrew;
    public GameObject botLeftScrew;
    public GameObject botRightScrew;
    public GameObject midScrew;

    public float baseAnimationTime;
    public float screwAnimationTime;

    public Renderer numberRenderer;
    private Material numberMaterial;

    public float numberAnimationTime;
    public float minDissolve;
    public float maxDissolve;

    IEnumerator Start()
    {
        numberMaterial = numberRenderer.material;
        yield return new WaitForSeconds(0.1f);
        Animation();

    }

    private void Animation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Insert(0,baseLogo.transform.DOLocalMoveY(0,baseAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, topLeftScrew.transform.DOMoveY(0, screwAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, topRightScrew.transform.DOMoveY(0, screwAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, botLeftScrew.transform.DOMoveY(0, screwAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, botRightScrew.transform.DOMoveY(0, screwAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, midScrew.transform.DOMoveY(0, screwAnimationTime)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                StartCoroutine(NumberLogoAnimation());
            }));
    }

    IEnumerator NumberLogoAnimation()
    {

        yield return new WaitForSeconds(0.1f);
        float time = numberAnimationTime;
        float amount = maxDissolve;
        float stepAmount = Mathf.Abs(maxDissolve - minDissolve) / numberAnimationTime;
        
        while (time > 0)
        {
            numberMaterial.SetFloat("_DissolveController",amount);
            time -= Time.deltaTime;
            amount -= stepAmount * Time.deltaTime;
            yield return null;

        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
