using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tweens : MonoBehaviour
{
    
    
    public void ScaleTween(RectTransform rectTransform)
    {
        Vector3 tweenScale = rectTransform.localScale + new Vector3(.5f, .5f, .5f);
        rectTransform.DOScale(tweenScale, 0.05f);
    }

}
