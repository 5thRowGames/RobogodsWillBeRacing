using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PlayerSkillManager : MonoBehaviour, IControllable
{
    public SkillBase godSkill;
    public SkillBase religionSkill;

    private float godTimeSkill;
    private float religionTimeSkill;

    private bool activateGodSkill;
    private bool activateReligionSkill;


    private void Awake()
    {
        godTimeSkill = 0;
        religionTimeSkill = 0;
        activateGodSkill = true;
        activateReligionSkill = false;
    }

    private void OnEnable()
    {
        ConnectDisconnectManager.ConnectSkillManagerDelegate += ConnectSkill;
        ConnectDisconnectManager.DisconnectSkillManagerDelegate += DisconnectSkill;
    }

    private void OnDisable()
    {
        ConnectDisconnectManager.ConnectSkillManagerDelegate -= ConnectSkill;
        ConnectDisconnectManager.DisconnectSkillManagerDelegate -= DisconnectSkill;
    }

    public void Control(IDevice device)
    {

        if (activateGodSkill && device.State.Fire.IsPressed) //Menor para eliminar comparaciones igualitarias con floats
        {
            activateGodSkill = false;
            godSkill.Effect();

            if (!godSkill.instantSkill)
                godSkill.isFinished = false;

            StartCoroutine(GodTimeSkillTimer(godSkill.duration, godSkill.instantSkill));
        }

        /*if (controller.State.Fire.IsPressed && activateReligionSkill)
        {
            activateReligionSkill = false;
            religionSkill.Effect();

            if (!religionSkill.instantSkill)
                religionSkill.isFinished = false;
            
        }*/
    }

    private IEnumerator GodTimeSkillTimer(float duration, bool instantSkill)
    {
        if (!instantSkill)
        {
            float timer = duration;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;

                if (godSkill.isFinished)
                {
                    timer = 0;
                    StartCoroutine(GodTimeSkillTimer(godSkill.coldown, true));
                }
            }

            if (!godSkill.isFinished)
            {
                StartCoroutine(GodTimeSkillTimer(godSkill.coldown, true));
                godSkill.FinishEffect();
            }
        }
        else
        {
            yield return new WaitForSeconds(duration);
            activateGodSkill = true;
        }
    }

    private void ReligionTimeSkillTimer()
    {
        
    }

    public void ConnectSkill()
    {
        Core.Input.AssignControllable(GetComponent<IncontrolProvider>(),this);
    }

    public void DisconnectSkill()
    {
        Core.Input.UnassignControllable(GetComponent<IncontrolProvider>(),this);
    }
}
