using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPrueba : MonoBehaviour
{

    public float rotateSpeed;
    
    private void Update()
    {
        transform.Rotate(Time.deltaTime * rotateSpeed * Vector3.up);
    }
}
