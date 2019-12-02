using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{
    public LogoAnimation logoAnimation;
    public Image fade;
    public Image master;

    private void Start()
    {
        StartCoroutine(SplashScreen());
    }

    IEnumerator SplashScreen()
    {
        fade.DOFade(0f, 2f);
        yield return new WaitForSeconds(1.5f);
        fade.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(1.5f);
        master.enabled = false;
        fade.enabled = false;
        logoAnimation.enabled = true;

    }
}
