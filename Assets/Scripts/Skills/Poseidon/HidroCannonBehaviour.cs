using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidroCannonBehaviour : MonoBehaviour
{

    public int speedAmount;
    public float recoverSpeedTime;
    public HidroCannonSkill hidroCannonSkill;

    private bool anubisEffectCanceled;
    private bool thorEffectCanceled;
    private bool kaliEffectCanceled;

    private void OnEnable()
    {
        anubisEffectCanceled = false;
        thorEffectCanceled = false;
        kaliEffectCanceled = false;
    }

    private void OnDisable()
    {
        HarmManager.Instance.RestoreSpeedByWater(0,speedAmount);
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Anubis":

                if (HarmManager.Instance.isAnubisShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Anubis);
                    anubisEffectCanceled = true;
                    hidroCannonSkill.FinishEffect();
                }
                else
                {
                    if (HarmManager.Instance.isAnubisWet)
                    {
                        HarmManager.Instance.StopRecoverSpeedTimer(God.Type.Anubis);
                    }
                    else
                    {
                        HarmManager.Instance.ReduceSpeed(God.Type.Anubis,speedAmount);
                        HarmManager.Instance.isAnubisWet = true;
                    }
                }
                
                break;
            
            case "Kali":
                
                if (HarmManager.Instance.isKaliShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Kali);
                    kaliEffectCanceled = true;
                    hidroCannonSkill.FinishEffect();
                }
                else
                {
                    if (HarmManager.Instance.isKaliWet)
                    {
                        HarmManager.Instance.StopRecoverSpeedTimer(God.Type.Kali);
                    }
                    else
                    {
                        HarmManager.Instance.ReduceSpeed(God.Type.Kali,speedAmount);
                        HarmManager.Instance.isKaliWet = true;
                    }
                }
                
                break;
            
            case "Thor":

                if (HarmManager.Instance.isThorShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Thor);
                    thorEffectCanceled = true;
                    hidroCannonSkill.FinishEffect();
                }
                else
                {
                    if (HarmManager.Instance.isThorWet)
                    {
                        HarmManager.Instance.StopRecoverSpeedTimer(God.Type.Thor);
                    }
                    else
                    {
                        HarmManager.Instance.ReduceSpeed(God.Type.Thor,speedAmount);
                        HarmManager.Instance.isThorWet = true;
                    }
                }
                
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Anubis":
                if (!anubisEffectCanceled)
                {
                    HarmManager.Instance.RecoverSpeedTimer(God.Type.Anubis,recoverSpeedTime,speedAmount);
                }
                break;
            
            case "Kali":
                if(!kaliEffectCanceled)
                {
                    HarmManager.Instance.RecoverSpeedTimer(God.Type.Kali,recoverSpeedTime,speedAmount);
                }
                break;
            
            case "Thor":
                if(!thorEffectCanceled)
                {
                    HarmManager.Instance.RecoverSpeedTimer(God.Type.Thor,recoverSpeedTime,speedAmount);
                }
                break;
        }
    }
}
