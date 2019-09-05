using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBolaPrueba : MonoBehaviour
{

    public float speed;
    
    private void Update()
    {
       transform.Translate(Time.deltaTime * speed * Vector3.forward);
    }
}
