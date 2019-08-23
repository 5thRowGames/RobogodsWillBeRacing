using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    public GameObject parent;
    
    private bool poseidonShield;
    private bool anubisShield;
    private bool thorShield;

    private void OnEnable()
    {
        poseidonShield = false;
        anubisShield = false;
        thorShield = false;
        StartCoroutine(FinishEffect());
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Anubis":
                
                if(HarmManager.Instance.isAnubisShielded)
                    HarmManager.Instance.RemoveShield(God.Type.Anubis);
                else if(!HarmManager.Instance.isAnubisStunned)
                    HarmManager.Instance.StunGod(God.Type.Anubis);

                    break;
            
            case "Poseidon":

                if(HarmManager.Instance.isPoseidonShielded)
                    HarmManager.Instance.RemoveShield(God.Type.Poseidon);
                else if(!HarmManager.Instance.isPoseidonStunned)
                    HarmManager.Instance.StunGod(God.Type.Poseidon);

                break;
            
            case "Thor: ":

                if(HarmManager.Instance.isThorShielded)
                    HarmManager.Instance.RemoveShield(God.Type.Thor);
                else if(!HarmManager.Instance.isThorStunned)
                    HarmManager.Instance.StunGod(God.Type.Thor);
                
                break;
        }
    }

    IEnumerator FinishEffect()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        parent.SetActive(false);
    }
}
