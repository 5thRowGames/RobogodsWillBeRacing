using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    public List<Animator> screws;
    public Animator logo;
    public float timeBetweenScrews;
    public float timeBetweenScrewsAndLogo;

    void Start()
    {
        StartCoroutine(LogoAnimationCoroutine());
    }

    IEnumerator LogoAnimationCoroutine()
    {

        for (int i = 0; i < screws.Count; i++)
        {
            screws[i].SetBool("activate",true);
            yield return new WaitForSeconds(timeBetweenScrews);
        }
        
        yield return new WaitForSeconds(timeBetweenScrewsAndLogo);
        logo.SetBool("activate", true);

    }
}
