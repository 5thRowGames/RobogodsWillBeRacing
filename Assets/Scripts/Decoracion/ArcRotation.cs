using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(10f * Time.deltaTime * Vector3.up);
    }
}
