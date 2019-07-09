using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UITween : MonoBehaviour
{
    public RectTransform rectTransform;
    
    [Header("Scale")]
    public Vector3 expand;
    public Vector3 contract;
    
    [Space(5)]
    [Header("Movement")]
    public Vector3 from;
    public Vector3 to;

    public void ExpandTween()
    {
        rectTransform.DOScale(expand, 0.03f);
    }

    public void ContractTween()
    {
        rectTransform.DOScale(contract, 0.03f);
    }

}
