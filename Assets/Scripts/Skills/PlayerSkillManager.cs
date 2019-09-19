using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class PlayerSkillManager : MonoBehaviour, IControllable
{
    //Pruebas
    public bool activeDevice;
    
    public God.Type god;
    public SkillBase godSkill;
    public SkillBase religionSkill;

    private bool activateGodSkill;
    private bool activateReligionSkill;

    private float godSkillTime;

    public float GodSkillTime
    {
        get => godSkillTime;
        set => godSkillTime = value;
    }

    private int mana;
    public int Mana
    {
        get => mana;
        set => mana = Mathf.Clamp(value, 0, 100);
    }


    private void Awake()
    {
        activateGodSkill = true;
        activateReligionSkill = true;
        
    }

    private void Start()
    {
        if (activeDevice)
        {
            GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
            Core.Input.AssignControllable(GetComponent<IncontrolProvider>(),this);
        } 
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
        
        StopAllCoroutines();
    }

    public void Control(IDevice device)
    {
        if (device.State.Fire.IsPressed && activateGodSkill) //Menor para eliminar comparaciones igualitarias con floats
        {
            activateGodSkill = false;
            godSkill.Effect();

            if (godSkill.instant)
            {
                StartCoroutine(GodSkillTimer());
            }
            else
            {
                godSkill.isFinished = false;
                StartCoroutine(GodTimeSkillExecution()); 
            }
        }

        if (device.State.Special.IsPressed && activateReligionSkill && Mana >= religionSkill.mana)
        {
            activateReligionSkill = false;
            Mana -= religionSkill.mana;
            
            HUDManager.Instance.UpdateManaBar(god, -religionSkill.mana);
            
            religionSkill.Effect();

            if (!religionSkill.instant)
                religionSkill.isFinished = false;

            StartCoroutine(ReligionTimeSkillTimer(religionSkill.executionDuration, religionSkill.instant));

        }
    }

    private IEnumerator GodTimeSkillExecution()
    {
        float timer = godSkill.executionDuration;
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;

            if (godSkill.isFinished)
            {
                timer = 0;
                StartCoroutine(GodTimeSkillExecution());
                godSkill.FinishEffect();
            }
        }
        
        if (!godSkill.isFinished)
        {
            godSkill.FinishEffect();
            StartCoroutine(GodSkillTimer());
        }

    }

    private IEnumerator GodSkillTimer()
    {
        float timer = godSkill.coldown;
        
        HUDManager.Instance.StartGodSkillTimer(god,timer);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        activateGodSkill = true;
    }

    //Si la religion va por mana hay que quitarle el coldown a esto
    private IEnumerator ReligionTimeSkillTimer(float duration, bool instantSkill)
    {
        if (!instantSkill)
        {
            float timer = duration;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;

                //Todo es posible que se pueda refactorizar
                if (religionSkill.isFinished)
                {
                    timer = 0;
                    religionSkill.FinishEffect();
                    activateReligionSkill = true;
                }
            }

            if (!religionSkill.isFinished)
            {
                religionSkill.FinishEffect();
                activateReligionSkill = true;
            }
        }
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
