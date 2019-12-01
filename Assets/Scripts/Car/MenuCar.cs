using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCar : MonoBehaviour
{
    public float speed;

    private void OnEnable()
    {
     Invoke("DestroyCar",5f);   
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void DestroyCar()
    {
        Destroy(gameObject);
    }
}
