using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public Image fade;
    public float fadeDuration;
    public float loadingScreenDuration;

    public int newScene;
    
    private AsyncOperation asyncLoadNextScene;

    private void OnEnable()
    {
        StartCoroutine(LoadingScreen());
    }

    #region Tweens

    IEnumerator LoadingScreen()
    {
        fade.DOFade(0, fadeDuration);
        asyncLoadNextScene = SceneManager.LoadSceneAsync(newScene);
        asyncLoadNextScene.allowSceneActivation = false;
        
        yield return new WaitForSeconds(loadingScreenDuration);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(fade.DOFade(1, fadeDuration)).OnComplete(() =>
        {
            asyncLoadNextScene.allowSceneActivation = true;

        });

    }
    
    #endregion
}
