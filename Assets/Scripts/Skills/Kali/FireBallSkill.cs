using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : SkillBase
{
    public Transform spawnPosition;
    public List<GameObject> ballPool;
    public List<GameObject> kaliBalls;
    public float rotationSpeedY;

    //Si es la primera que que se activa la habilidad, solo debe activar la rotación de las bolas
    private bool firstActivate = true;
    private bool reactivateSkill;

    private void Update()
    {
        transform.Rotate(0,rotationSpeedY,0);
    }

    public override void Effect()
    {
        if (firstActivate)
        {
            gameObject.SetActive(true);
            firstActivate = false;
            StartCoroutine(PressAgain(0.5f));
        }
        else
        {
            if (EnoughBalls())
            {
                GameObject ball = GetBall();
                ball.SetActive(true);
                ball.transform.position = spawnPosition.position;
                ball.transform.rotation = spawnPosition.rotation;
            }
            else
                FinishEffect();
        }
    }

    public override void FinishEffect()
    {
        gameObject.SetActive(false);
    }

    public override void ResetSkill()
    {
        foreach (GameObject kaliBall in kaliBalls)
        {
            kaliBall.SetActive(true);
        }

        firstActivate = true;
        isFinished = true;
    }

    private void OnEnable()
    {
        ResetSkill();
    }

    private GameObject GetBall()
    {
        return ballPool.Find(x => x.activeInHierarchy == false);
    }

    private bool EnoughBalls()
    {
        foreach (GameObject kaliBall in kaliBalls)
        {
            if (kaliBall.activeInHierarchy)
            {
                kaliBall.SetActive(false);
                return true;
            }
        }
        return false;
    }

    IEnumerator PressAgain(float time)
    {
        reactivateSkill = false;
        yield return new WaitForSeconds(time);
        reactivateSkill = true;
    }
}
