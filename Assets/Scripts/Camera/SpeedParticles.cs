using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedParticles : MonoBehaviour
{
    public ParticleSystem speedParticles;

    private int index;

    private void OnEnable()
    {
        StartCoroutine(UpdateRateOverTime());
    }

    IEnumerator UpdateRateOverTime()
    {
        while (enabled)
        {
            if (CameraPostProcessManager.Instance.speed[index] < 60f)
            {
                var emission = speedParticles.emission;
                emission.rateOverTime = 0f;
            }
            else
            {
                var emission = speedParticles.emission;
                emission.rateOverTime = CameraPostProcessManager.Instance.speed[index] / 50f;
            }

            yield return new WaitForSeconds(2f);
        }
    }

    public void AssignIndex(int _index)
    {
        index = _index;
    }
}
