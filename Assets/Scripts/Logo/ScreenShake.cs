using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public GameObject mainCamera;
    public float shakeDuration;
    public float shakeStrength;
    
    public void ScreenShakeCall()
    {
        StartCoroutine(ScreenShakeCoroutine());
    }

    IEnumerator ScreenShakeCoroutine()
    {
        Vector3 initialPosition = mainCamera.transform.localPosition;
        float time = shakeDuration;

        while (time > 0)
        {
            mainCamera.transform.localPosition = initialPosition + Random.insideUnitSphere * shakeStrength;
            time -= Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = initialPosition;
    }
}
