using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerThrow : SkillBase
{
    public int skidAmount;
    public float recoverTime;
    
    public Transform spawnPoint;
    public float hammerSpeed;

    private void OnEnable()
    {
        transform.position = spawnPoint.position;
    }

    private void Update()
    {
        transform.Translate(hammerSpeed * Time.deltaTime * Vector3.forward);
    }

    public override void Effect()
    {
        gameObject.SetActive(true);
    }

    public override void FinishEffect()
    {
        gameObject.SetActive(false);
        ResetSkill();
    }

    public override void ResetSkill()
    {
        transform.position = spawnPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Anubis":

                if (HarmManager.Instance.isAnubisShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Anubis);
                }
                else
                {
                    HarmManager.Instance.DisableSkid(God.Type.Anubis, skidAmount, recoverTime);
                }
                
                FinishEffect();
                break;
            
            case "Poseidon":
                
                if (HarmManager.Instance.isPoseidonShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Poseidon);
                }
                else
                {
                    HarmManager.Instance.DisableSkid(God.Type.Poseidon, skidAmount, recoverTime);
                }
                
                FinishEffect();
                break;
            
            case "Kali":

                if (HarmManager.Instance.isKaliShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Kali);
                }
                else
                {
                    HarmManager.Instance.DisableSkid(God.Type.Kali, skidAmount, recoverTime);
                }
                
                FinishEffect();
                break;
            
        }
    }
}
