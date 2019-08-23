using System;
using DG.Tweening;
using UnityEngine;

public class DaggerBehaviourAlone : MonoBehaviour
{

    public float distanceToChangeCheckpoint;
    public float movementSpeed;
    public float markTime;
    public float recoverNormalSpeedTime;
    public int speedReduced;
    public DaggerSkillAlone daggerSkillAlone;

    private int direction;
    private int kaliIndex;
    private int targetIndex;
    private int closestCheckpointIndex;
    private Transform godTarget;
    private Transform closestCheckpoint;
    private Transform currentTarget;

    public void Init()
    {
        kaliIndex = 0;
        ChooseTarget();
        ChooseDirectionAndCheckpoint();
    }
    
    private void Update()
    {
        SearchNextPoint();
        transform.LookAt(currentTarget.position);
        transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward);
    }

    private void ChooseTarget()
    {
        float distance = Mathf.Infinity;
        targetIndex = 0;
        kaliIndex = LapsManager.Instance.godRaceInfoList.FindIndex(x => x.god.CompareTag("Kali"));

        for (int i = 0; i < LapsManager.Instance.godRaceInfoList.Count; i++)
        {
            if (i != kaliIndex)
            {
                float newDistance = (LapsManager.Instance.godRaceInfoList[i].god.transform.position - LapsManager.Instance.godRaceInfoList[kaliIndex].god.transform.position).sqrMagnitude;

                if (newDistance < distance)
                {
                    godTarget = LapsManager.Instance.godRaceInfoList[i].god.transform;
                    distance = newDistance;
                    targetIndex = i;
                }
            }
        }
        
    }

    private void ChooseDirectionAndCheckpoint()
    {
        //Si la posición de kali es mejor que la del target es porque va mejor en la carrera (va adelantada)
        if (LapsManager.Instance.racePosition[kaliIndex] < LapsManager.Instance.racePosition[targetIndex])
        {
            direction = -1;
            closestCheckpointIndex = LapsManager.Instance.godRaceInfoList[kaliIndex].currentCheckPoint;
            closestCheckpoint = LapsManager.Instance.checkPoints[closestCheckpointIndex];
        }
        else
        {
            closestCheckpointIndex = LapsManager.Instance.godRaceInfoList[kaliIndex].currentCheckPoint + 1;
            
            if (closestCheckpointIndex == LapsManager.Instance.checkPoints.Count)
                closestCheckpointIndex = 0;
            
            closestCheckpoint = LapsManager.Instance.checkPoints[closestCheckpointIndex];

            direction = 1;
        }

        Debug.Log(closestCheckpointIndex);
        
        currentTarget = closestCheckpoint;
    }

    private void SearchNextPoint()
    {
        float targetDistance = (transform.position - godTarget.position).sqrMagnitude;
        float checkpointDistance = (transform.position - closestCheckpoint.position).sqrMagnitude;

        if (targetDistance < checkpointDistance)
        {
            currentTarget = godTarget;
        }
        else if (checkpointDistance < distanceToChangeCheckpoint)
        {
            closestCheckpointIndex += direction;

            if (closestCheckpointIndex < 0)
                closestCheckpointIndex = LapsManager.Instance.checkPoints.Count - 1;
            else if (closestCheckpointIndex == LapsManager.Instance.checkPoints.Count)
                closestCheckpointIndex = 0;
            
            closestCheckpoint = LapsManager.Instance.checkPoints[closestCheckpointIndex];
            currentTarget = closestCheckpoint;
        }
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
                    HarmManager.Instance.ModifySpeedTemporally(God.Type.Anubis,speedReduced,recoverNormalSpeedTime);
                    HarmManager.Instance.MarkGod(God.Type.Anubis,markTime);
                }
                daggerSkillAlone.FinishEffect();
                
                break;
            
            case "Poseidon":

                if (HarmManager.Instance.isPoseidonShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Poseidon);
                }
                else
                {
                    HarmManager.Instance.ModifySpeedTemporally(God.Type.Poseidon,speedReduced,recoverNormalSpeedTime);
                    HarmManager.Instance.MarkGod(God.Type.Poseidon,markTime);
                }
                daggerSkillAlone.FinishEffect();
                
                break;
            
            case "Thor":
                
                if (HarmManager.Instance.isThorShielded)
                {
                    HarmManager.Instance.RemoveShield(God.Type.Thor);
                }
                else
                {
                    HarmManager.Instance.ModifySpeedTemporally(God.Type.Thor,speedReduced,recoverNormalSpeedTime);
                    HarmManager.Instance.MarkGod(God.Type.Thor,markTime);
                }
                daggerSkillAlone.FinishEffect();
                
                break;
        }
    }
    
}
