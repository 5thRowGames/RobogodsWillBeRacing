using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoAnimation : MonoBehaviour
{
    public GameObject baseLogo;
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

    private AsyncOperation asyncLoadNextScene;

    IEnumerator Start()
    {
        numberMaterial = numberRenderer.material;
        yield return new WaitForSeconds(0.1f);
        Animation();

    }

    private void Animation()
    {

        StartCoroutine(NumberLogoAnimation());
        
        Sequence seq = DOTween.Sequence();
        seq.Insert(0,baseLogo.transform.DOLocalMoveY(0,baseAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, topLeftScrew.transform.DOMoveY(0, screwAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, topRightScrew.transform.DOMoveY(0, screwAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, botLeftScrew.transform.DOMoveY(0, screwAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, botRightScrew.transform.DOMoveY(0, screwAnimationTime).SetEase(Ease.Linear));
        seq.Insert(0.1f + baseAnimationTime, midScrew.transform.DOMoveY(0, screwAnimationTime)
            .SetEase(Ease.Linear));
        
        
    }

    IEnumerator NumberLogoAnimation()
    {
        asyncLoadNextScene = SceneManager.LoadSceneAsync(1);
        asyncLoadNextScene.allowSceneActivation = false;

        yield return new WaitForSeconds(0.1f);
        AkSoundEngine.PostEvent("Sonic_Logo_In", gameObject);
        yield return new WaitForSeconds(0.7f);
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

        yield return new WaitForSeconds(0.25f);
        asyncLoadNextScene.allowSceneActivation = true;
    }

    private void DisableLogo()
    {
        baseLogo.SetActive(false);
        topLeftScrew.SetActive(false);
        topRightScrew.SetActive(false);
    }

    private void CanRunAnimation()
    {
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
