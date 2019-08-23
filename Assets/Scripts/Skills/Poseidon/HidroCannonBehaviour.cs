using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidroCannonBehaviour : MonoBehaviour
{

    public int speedAmount;
    public float recoverSpeedTime;
    public HidroCannonSkill hidroCannonSkill;

    private bool effectCanceled;

    private void OnEnable()
    {
        effectCanceled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            case "Anubis":

                if (HarmManager.Instance.isAnubisShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Anubis);
                    effectCanceled = true;
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
                    effectCanceled = true;
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
                    effectCanceled = true;
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
                if(!effectCanceled)
                    HarmManager.Instance.RecoverSpeedTimer(God.Type.Anubis,recoverSpeedTime,speedAmount);
                break;
            
            case "Kali":
                if(!effectCanceled)
                    HarmManager.Instance.RecoverSpeedTimer(God.Type.Kali,recoverSpeedTime,speedAmount);
                break;
            
            case "Thor":
                if(!effectCanceled)
                    HarmManager.Instance.RecoverSpeedTimer(God.Type.Thor,recoverSpeedTime,speedAmount);
                break;
        }
    }
}
