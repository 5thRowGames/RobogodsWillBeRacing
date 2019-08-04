using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PlayerSkillManager : MonoBehaviour, IControllable
{
    public SkillBase godSkill;
    public SkillBase religionSkill;
    public TextMeshProUGUI godTime;
    public TextMeshProUGUI religionTime;

    private bool activateGodSkill;
    private bool activateReligionSkill;

    private float religionSkillTimer;

    public float ReligionSkillTimer
    {
        get => religionSkillTimer;
        set
        {
            if (value < 0)
                religionSkillTimer = 0;
            else
                religionSkillTimer = value;
        }
    }

    private float godSkillTimer;
    public float GodSkillTimer
    {
        get => godSkillTimer;
        set
        {
            if (value < 0)
                godSkillTimer = 0;
            else
                godSkillTimer = value;
        }
    }

    private float mana;

    public float Mana
    {
        get => mana;
        set => mana = Mathf.Clamp(value, 0, 100);
    }


    private void Awake()
    {
        activateGodSkill = true;
        activateReligionSkill = true;
        religionSkillTimer = 0;
        godSkillTimer = 0;
        
        // TODO Borrar (son pruebas)
        Core.Input.AssignControllable(GetComponent<IncontrolProvider>(),this);
        GetComponent<IncontrolProvider>().myPlayerActions = MyPlayerActions.BindKeyboard();
        Mana = 500;

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

            if (!godSkill.instant)
                godSkill.isFinished = false;

            StartCoroutine(GodTimeSkillTimer(godSkill.executionDuration, godSkill.instant));
        }

        
        if (device.State.Special.IsPressed && activateReligionSkill && Mana >= religionSkill.mana)
        {
            activateReligionSkill = false;
            Mana -= religionSkill.mana;
            religionSkill.Effect();

            if (!religionSkill.instant)
                religionSkill.isFinished = false;

            StartCoroutine(ReligionTimeSkillTimer(religionSkill.executionDuration, religionSkill.instant));

        }
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
                    godSkill.FinishEffect();
                }
            }

            //En caso de que pase el tiempo antes de que la habilidad termine
            if (!godSkill.isFinished)
            {
                StartCoroutine(GodTimeSkillTimer(godSkill.coldown, true));
                godSkill.FinishEffect();
            }
        }
        else
        {

            GodSkillTimer = duration;
            
            while (GodSkillTimer > 0)
            {
                GodSkillTimer -= Time.deltaTime;
                godTime.text = GodSkillTimer.ToString();
                yield return null;
            }
            
            activateGodSkill = true;
        }
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
