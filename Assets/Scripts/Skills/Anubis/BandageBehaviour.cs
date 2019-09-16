using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageBehaviour : MonoBehaviour
{
    public int speedAmountIncreased;
    public int speedAmountDecreased;
    public float speedIncreasedTime;
    public float speedDecreasedTime;
    public BandageThrow bandageThrow;

    public float speed;

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Poseidon":

                if (HarmManager.Instance.isPoseidonShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Poseidon);
                    bandageThrow.FinishEffect();
                }
                else
                {
                    HarmManager.Instance.ModifySpeedTemporally(God.Type.Anubis,speedAmountIncreased,speedIncreasedTime);
                    HarmManager.Instance.ModifySpeedTemporally(God.Type.Poseidon,speedAmountDecreased,speedDecreasedTime);
                    bandageThrow.FinishEffect();
                }
                
                break;
            
            case "Kali":
                
                if (HarmManager.Instance.isKaliShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Kali);
                    bandageThrow.FinishEffect();
                    
                }
                else
                {
                    HarmManager.Instance.ModifySpeedTemporally(God.Type.Anubis,speedAmountIncreased,speedIncreasedTime);
                    HarmManager.Instance.ModifySpeedTemporally(God.Type.Kali,speedAmountDecreased,speedDecreasedTime);
                    bandageThrow.FinishEffect();
                }
                
                break;
            
            case "Thor":
                
                if (HarmManager.Instance.isThorShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Thor);
                    bandageThrow.FinishEffect();
                    
                }
                else
                {
                    HarmManager.Instance.ModifySpeedTemporally(God.Type.Anubis,speedAmountIncreased,speedIncreasedTime);
                    HarmManager.Instance.ModifySpeedTemporally(God.Type.Thor,speedAmountDecreased,speedDecreasedTime);
                    bandageThrow.FinishEffect();
                }
                
                break;
        }
    }
}
