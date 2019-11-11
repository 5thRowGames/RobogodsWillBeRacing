using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodecaedroRotation : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(200f * Time.deltaTime * (Vector3.forward + Vector3.up));
    }
}
