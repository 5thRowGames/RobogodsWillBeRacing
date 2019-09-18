using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocustPlague : SkillBase
{
    public override void Effect()
    {
        //HarmManager.Instance.StartLocustPlagueDelegate();
    }

    public override void FinishEffect()
    {
       //HarmManager.Instance.FinishLocustPlagueDelegate();
    }

    public override void ResetSkill()
    {
        throw new System.NotImplementedException();
    }
}
