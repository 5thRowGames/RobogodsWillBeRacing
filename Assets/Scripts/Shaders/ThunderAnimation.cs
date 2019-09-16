using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderAnimation : MonoBehaviour
{
    public ParticleSystem exteriorCloud;
    public ParticleSystem interiorCloud;
    public ParticleSystem glow;
    public ParticleSystem lightning;
    public ParticleSystem hit_sparks;
    public ParticleSystem flash;

    public float expandCloudTime;

    private void OnEnable()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        float time = expandCloudTime;
        float step = 1 / expandCloudTime;
        float amount;

        exteriorCloud.Play();
        interiorCloud.Play();
        while (time > 0)
        {
            time -= Time.deltaTime;
            amount = step * Time.deltaTime;
            exteriorCloud.transform.localScale += Vector3.one * amount;
            interiorCloud.transform.localScale += Vector3.one * amount;
            yield return Time.deltaTime;
        }
        
        glow.Play();
        lightning.Play();
        hit_sparks.Play();
        flash.Play();

        yield return new WaitForSeconds(0.5f);
        exteriorCloud.Stop();
        interiorCloud.Stop();
        glow.Stop();
        lightning.Stop();
        hit_sparks.Stop();
        flash.Stop();
        
        time = expandCloudTime;
        
        while (time > 0)
        {
            time -= Time.deltaTime;
            amount = step * Time.deltaTime;
            exteriorCloud.transform.localScale -= Vector3.one * amount;
            interiorCloud.transform.localScale -= Vector3.one * amount;
            yield return Time.deltaTime;
        }
        
        exteriorCloud.transform.localScale = Vector3.zero;
        interiorCloud.transform.localScale = Vector3.zero;
    }
}
