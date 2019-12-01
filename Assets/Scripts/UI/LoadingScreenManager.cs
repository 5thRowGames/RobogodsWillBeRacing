using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public RectTransform loadingIcon;
    public Image fade;
    public float fadeDuration;
    public float loadingScreenDuration;
    
    public int newScene;
    
    private AsyncOperation asyncLoadNextScene;

    private void OnEnable()
    {
        StartCoroutine(LoadingScreen());
        loadingIcon.DOLocalRotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetUpdate(true).SetLoops(-1);
    }

    #region Tweens

    IEnumerator LoadingScreen()
    {
        fade.DOFade(0, fadeDuration);
        asyncLoadNextScene = SceneManager.LoadSceneAsync(newScene);
        asyncLoadNextScene.allowSceneActivation = false;
        
        yield return new WaitForSeconds(loadingScreenDuration);
        SoundManager.Instance.StopAll();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(fade.DOFade(1, fadeDuration)).OnComplete(() =>
        {
            asyncLoadNextScene.allowSceneActivation = true;

        });

    }
    
    #endregion
}
