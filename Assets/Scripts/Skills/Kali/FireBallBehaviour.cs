using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FireBallBehaviour: MonoBehaviour
{
    [Range(10,40)]
    public int speed = 15;

    public Transform explosionTransform;
    public int scaleFactor;
    public bool move;

    private void OnEnable()
    {
        move = true;
        explosionTransform.localScale = new Vector3(0.1f,0.1f,0.1f);
    }

    void Update()
    {
        if(move)
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Anubis":

                if (!HarmManager.Instance.isAnubisStunned)
                {
                    if (HarmManager.Instance.isAnubisMarked)
                    {
                        move = false;
                        explosionTransform.gameObject.SetActive(true);
                        explosionTransform.DOScale(Vector3.one * scaleFactor, 0.2f);
                    }
                    else if (!HarmManager.Instance.isAnubisStunned)
                    {
                        HarmManager.Instance.StunGod(God.Type.Anubis);
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    HarmManager.Instance.RemoveShield(God.Type.Anubis);
                    gameObject.SetActive(false);
                }
                
                
                break;
            
            case "Poseidon":

                if (!HarmManager.Instance.isPoseidonShielded)
                {
                    if (HarmManager.Instance.isPoseidonMarked)
                    {
                        move = false;
                        explosionTransform.gameObject.SetActive(true);
                        explosionTransform.DOScale(Vector3.one * scaleFactor, 0.2f);
                    }
                    else if (!HarmManager.Instance.isPoseidonStunned)
                    {
                        HarmManager.Instance.StunGod(God.Type.Poseidon);
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    HarmManager.Instance.RemoveShield(God.Type.Poseidon);
                    gameObject.SetActive(false);
                }

                break;
            
            case "Thor: ":

                if (!HarmManager.Instance.isThorShielded)
                {
                    if (HarmManager.Instance.isThorMarked)
                    {
                        move = false;
                        explosionTransform.gameObject.SetActive(true);
                        explosionTransform.DOScale(Vector3.one * scaleFactor, 0.2f);
                    }
                    else if (!HarmManager.Instance.isThorStunned)
                    {
                        HarmManager.Instance.StunGod(God.Type.Thor);
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    HarmManager.Instance.RemoveShield(God.Type.Thor);
                    gameObject.SetActive(false);
                }
                
                break;
        }
    }
}
