using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : SkillBase
{
    public Transform spawnPosition;
    public List<GameObject> ballPool;
    public DeviceController deviceController;
    public Transform parent;

    //Si es la primera que que se activa la habilidad, solo debe activar la rotación de las bolas
    private bool firstActivate = true;
    private bool reactivateSkill;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, parent.rotation.y, 0);

        if (deviceController.device != null && deviceController.playable && deviceController.device.State.Fire.IsPressed && reactivateSkill)
        {
            Effect();
            StartCoroutine(PressAgain(0.5f));
        }
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
                gameObject.SetActive(false);
        }
    }

    public override void FinishEffect()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        foreach (Transform ball in transform)
        {
            ball.gameObject.SetActive(true);
        }

        firstActivate = true;
        isFinished = true;
    }

    private void OnEnable()
    {
        firstActivate = true;
        reactivateSkill = false;
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

    IEnumerator PressAgain(float time)
    {
        reactivateSkill = false;
        yield return new WaitForSeconds(time);
        reactivateSkill = true;
    }
}
