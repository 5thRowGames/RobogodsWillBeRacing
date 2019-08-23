using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public  float executionDuration; //true: coldown   false: mana
    public float coldown;
    public int mana;
    public bool isFinished;
    public bool instant; //Si la habilidad es instantanea, el coldown empezará justo al iniciarse

    public abstract void Effect();

    public abstract void FinishEffect();

    public abstract void ResetSkill();

}
