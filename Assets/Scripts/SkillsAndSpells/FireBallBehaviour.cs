using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehaviour: MonoBehaviour
{
    public LayerMask layer;
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
        
    }
}
