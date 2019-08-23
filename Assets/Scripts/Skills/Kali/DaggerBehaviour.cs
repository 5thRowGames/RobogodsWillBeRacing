using System;
using DG.Tweening;
using UnityEngine;

public class DaggerBehaviour : MonoBehaviour
{

    public float markTime;
    public float recoverNormalSpeedTime;
    public int speedReduced;
    public DaggerSkill daggerSkill;
    
    [Header("Movement animation")]
    public Transform target;
    public int gap;
    private bool right; //Right: True   Left: False
    private Vector3[] path;

    public enum PositionType
    {
        Left,
        Mid,
        Right
    };

    public void Init(Vector3 spawnPosition)
    {
        path = new Vector3[3];
        transform.position = spawnPosition;

    }

    public PositionType positionType;

    public void DoEffect()
    {
        switch (positionType)
        {
            case PositionType.Left:
                right = false;
                
                path[0] = transform.position;
                path[2] = target.transform.position + target.transform.forward * 30f;
                path[1] = transform.position + (path[2] - transform.position) / 2;
        
                if(right)
                    path[1].x += gap;
                else
                    path[1].x -= gap;
                
                transform.DOLocalPath(path, 1f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red)
                    .OnComplete(RepeatPathCallback);
                break;
            
            case PositionType.Mid:
                
                path[0] = transform.position;
                path[2] = target.transform.position + target.transform.forward * 30f;
                path[1] = transform.position + (path[2] - transform.position) / 2;
                
                transform.DOLocalPath(path, 1f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red).OnComplete(RepeatPathCallbackMid); 
                break;
            
            case PositionType.Right:
                right = true;
                
                path[0] = transform.position;
                path[2] = target.transform.position + target.transform.forward * 30f;
                path[1] = transform.position + (path[2] - transform.position) / 2;
        
                if(right)
                    path[1].x += gap;
                else
                    path[1].x -= gap;
                
                transform.DOLocalPath(path, 1f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red).OnComplete(RepeatPathCallback);
                break;
        }
    }

    private void RepeatPathCallback()
    {
        if (Vector3.Distance(transform.position, target.position) < 1)
        {
            path[0] = transform.position;
            path[2] = target.transform.position + target.transform.forward * 10f;
            path[1] = transform.position + (path[2] - transform.position) / 2;
        
            if(right)
                path[1].x += gap;
            else
                path[1].x -= gap;

            transform.DOLocalPath(path, 0.5f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red)
                .OnComplete(RepeatPathCallback);
        }
        else
        {
            path[0] = transform.position;
            path[2] = target.transform.position + target.transform.forward * 30f;
            path[1] = transform.position + (path[2] - transform.position) / 2;
        
            if(right)
                path[1].x += gap;
            else
                path[1].x -= gap;

            transform.DOLocalPath(path, 1f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red)
                .OnComplete(RepeatPathCallback);
        }
    }
    
    private void RepeatPathCallbackMid()
    {
        if (Vector3.Distance(transform.position, target.position) < 8)
        {
            path[0] = transform.position;
            path[2] = target.transform.position + target.transform.forward * 10f;
            path[1] = transform.position + (path[2] - transform.position) / 2;

            transform.DOLocalPath(path, 0.5f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red)
                .OnComplete(RepeatPathCallbackMid);
        }
        else
        {
            path[0] = transform.position;
            path[2] = target.transform.position + target.transform.forward * 30f;
            path[1] = transform.position + (path[2] - transform.position) / 2;

            transform.DOLocalPath(path, 1f, PathType.CatmullRom, PathMode.Full3D, 5, Color.red)
                .OnComplete(RepeatPathCallbackMid); 
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Anubis":
                HarmManager.Instance.ModifySpeedTemporally(God.Type.Anubis,speedReduced,recoverNormalSpeedTime);
                HarmManager.Instance.MarkGod(God.Type.Anubis,markTime);
                daggerSkill.FinishEffect();
                break;
            
            case "Poseidon":
                HarmManager.Instance.ModifySpeedTemporally(God.Type.Poseidon,speedReduced,recoverNormalSpeedTime);
                HarmManager.Instance.MarkGod(God.Type.Poseidon,markTime);
                daggerSkill.FinishEffect();
                break;
            
            case "Thor":
                HarmManager.Instance.ModifySpeedTemporally(God.Type.Thor,speedReduced,recoverNormalSpeedTime);
                HarmManager.Instance.MarkGod(God.Type.Thor,markTime);
                daggerSkill.FinishEffect();
                break;
            
            case "EnergyShield":
                daggerSkill.FinishEffect();
                other.gameObject.SetActive(false);
                break;
        }
    }
    
}
