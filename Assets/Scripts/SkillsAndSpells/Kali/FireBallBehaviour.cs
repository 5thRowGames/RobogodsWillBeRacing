using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FireBallBehaviour: MonoBehaviour
{
    public Transform rotatePoint;
    
    [Range(50,300)]
    public int rotation = 80;

    [Range(10,40)]
    public int speed = 15;
    
    //Las bolas de la pool tendrán esto por defecto
    public bool isThrown;

    // Update is called once per frame
    void Update()
    {
        if(!isThrown)
            transform.RotateAround(rotatePoint.position, Vector3.up, rotation * Time.deltaTime);
        else
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*other.GetComponent<DeviceController>().playable = false;
        gameObject.SetActive(false);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(other.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative())
            .Append(other.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative())
            .Append(other.transform.DORotate(new Vector3(0, 360, 0), 1f,RotateMode.FastBeyond360).SetRelative())
            .OnComplete(() =>
        {
            other.GetComponent<DeviceController>().playable = false; 
        });*/
    }
}
