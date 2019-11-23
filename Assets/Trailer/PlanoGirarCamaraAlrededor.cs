using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanoGirarCamaraAlrededor : MonoBehaviour
{
    public GameObject camera;
    public GameObject target;
    public float speed;
    
    private bool canAnimate = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            canAnimate = true;

        if (canAnimate)
        {
            camera.transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
        }
    }
    
    
}
