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
    
    private AsyncOperation asyncLoad;

    private void OnEnable()
    {
        StartCoroutine(LoadingScreen());
    }

    #region Tweens

    IEnumerator LoadingScreen()
    {
        fade.DOFade(0, fadeDuration);
        asyncLoad = SceneManager.LoadSceneAsync("StartRaceAnimation", LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;
        
        yield return new WaitForSeconds(loadingScreenDuration);

        Sequence sequence = DOTween.Sequence();
            sequence.Append(fade.DOFade(1, fadeDuration)).OnComplete(() =>
            {
                asyncLoad.allowSceneActivation = true;
                SceneManager.UnloadSceneAsync("Menu");
                gameObject.SetActive(false);
            });
        
    }
    
    #endregion
}
