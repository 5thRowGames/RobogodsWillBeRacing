using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BandageThrow : SkillBase
{

    public Transform spawnPosition;
    public GameObject bandage;
    
public override void Effect()
    {
        ResetSkill();
        gameObject.SetActive(true);
        bandage.SetActive(true);
    }

    public override void FinishEffect()
    {
        bandage.SetActive(false);
        gameObject.SetActive(false);
    }

    public override void ResetSkill()
    {
        bandage.transform.position = spawnPosition.position;
        bandage.transform.rotation = spawnPosition.rotation;
    }
}
