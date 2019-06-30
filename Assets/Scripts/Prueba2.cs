using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Prueba2 : MonoBehaviour
{
    //public IncontrolProvider incontrol;

    void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(-100, 5f)).OnComplete(() => { Debug.Log("Primero"); });
        sequence.Append(transform.DOMoveY(-100, 5f)).OnComplete(() => { Debug.Log("Segundo"); });


    }
}
