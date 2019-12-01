using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTrail : MonoBehaviour
{
    [SerializeField] private ParticleSystem powerParticles;

    [SerializeField] private Color NormalSpeedColor;
    [SerializeField] private Color TurboColor;

    private Coroutine controlPower;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float decreaseSpeedAmount;
    [SerializeField] private float increaseSpeedAmount;

    private float speed;

    private void Awake()
    {
        speed = 0;
        controlPower = null;
    }

    public void Activate()
    {
        powerParticles.Play();
    }

    public void IncreasePowerColor()
    {
        powerParticles.startColor = TurboColor;
    }

    public void DecreasePowerColor()
    {
        powerParticles.startColor = NormalSpeedColor;
    }

    public void DecreasePowerSpeed()
    {
        if (controlPower != null)
            StopCoroutine(controlPower);

        controlPower = StartCoroutine(DecreasePower());
    }

    IEnumerator DecreasePower()
    {
        speed = powerParticles.startSpeed;

        while (speed > 0)
        {
            speed -= decreaseSpeedAmount * Time.deltaTime;
            powerParticles.startSpeed = speed;
            yield return null;
        }

        speed = 0;
        powerParticles.startSpeed = 0;

        controlPower = null;
    }

    public void IncreasePowerSpeed()
    {
        if (controlPower != null)
            StopCoroutine(controlPower);

        controlPower = StartCoroutine(IncreasePower());
    }

    IEnumerator IncreasePower()
    {
        speed = powerParticles.startSpeed;

        while (speed < maxSpeed)
        {
            speed += increaseSpeedAmount * Time.deltaTime;
            powerParticles.startSpeed = speed;
            yield return null;
        }

        powerParticles.startSpeed = maxSpeed;
        speed = maxSpeed;

        controlPower = null;
    }
}
