using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : SkillBase, IControllable
{
    public IncontrolProvider inControlProvider;
    public Transform spawnPosition;
    public List<GameObject> ballPool;
    public List<GameObject> kaliBalls;
    public float rotationSpeedY;

    //Si es la primera que que se activa la habilidad, solo debe activar la rotación de las bolas
    private bool firstActivate = true;
    private bool reactivateSkill = true;
    

    public void Control(IDevice controller)
    {
        transform.Rotate(Time.deltaTime * rotationSpeedY * Vector3.up);
        
        if(controller.State.Special.IsPressed)
            Effect();
    }

    public override void Effect()
    {

        if (reactivateSkill)
        {
            if (firstActivate)
            {
                gameObject.SetActive(true);
                firstActivate = false;
                StartCoroutine(PressAgain(0.5f));
                ConnectSkillControl();
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
    }

    public override void FinishEffect()
    {
        gameObject.SetActive(false);
        DisconnectSkillControl();
    }

    public override void ResetSkill()
    {
        foreach (GameObject kaliBall in kaliBalls)
        {
            kaliBall.SetActive(true);
        }
        
        isFinished = true;
    }

    private void OnEnable()
    {
        ResetSkill();
        
    }

    private void OnDisable()
    {
        reactivateSkill = true;
        firstActivate = true;
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

    private void ConnectSkillControl()
    {
        Core.Input.AssignControllable(inControlProvider,this);
    }

    private void DisconnectSkillControl()
    {
        Core.Input.UnassignControllable(inControlProvider,this);
    }
}
