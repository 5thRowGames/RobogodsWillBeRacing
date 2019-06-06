using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : SkillBase
{
    public Transform spawnPosition;
    public List<GameObject> ballPool;

    //Si es la primera que que se activa la habilidad, solo debe activar la rotación de las bolas
    private bool firstActivate = true;

    public override void Effect()
    {
        if (firstActivate)
        {
            gameObject.SetActive(true);
            firstActivate = false;
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
            {
                gameObject.SetActive(false);
                firstActivate = true;

            }

        }
    }

    private void OnDisable()
    {
        foreach (Transform ball in gameObject.transform)
        {
            ball.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        firstActivate = true;
    }

    private GameObject GetBall()
    {
        return ballPool.Find(x => x.activeInHierarchy == false);
    }

    private bool EnoughBalls()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(false);
                return true;
            }
        }

        return false;
    }
    
}
