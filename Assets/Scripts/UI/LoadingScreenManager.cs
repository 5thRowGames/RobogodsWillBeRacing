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
    
    private AsyncOperation asyncLoadNextScene;
    private AsyncOperation asyncUnloadCurrentScene;

    private void OnEnable()
    {
        StartCoroutine(LoadingScreen());
    }

    #region Tweens

    IEnumerator LoadingScreen()
    {
        fade.DOFade(0, fadeDuration);
        asyncLoadNextScene = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        asyncLoadNextScene.allowSceneActivation = false;

        //Ver el flujo a partir de aquí
        yield return new WaitForSeconds(loadingScreenDuration);

        //No estoy seguro si petaría en el caso de que la escena no se cargue bien
        //El problema es el allowSceneActivation, que capa todo lo demás es una mierda, el .isDone no funciona con eso parece ser
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fade.DOFade(1, fadeDuration)).OnComplete(() =>
        {
            SceneManager.UnloadSceneAsync(0);
            Resources.UnloadUnusedAssets();
            asyncLoadNextScene.allowSceneActivation = true;
            gameObject.SetActive(false);
        });
        
    }
    
    #endregion
}
