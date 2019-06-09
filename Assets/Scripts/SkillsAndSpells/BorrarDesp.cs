using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorrarDesp : MonoBehaviour
{
    
    void Update()
    {
       transform.Translate(10f * Time.deltaTime * Vector3.forward); 
    }
}
