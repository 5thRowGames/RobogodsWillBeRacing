using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public  float duration;
    public bool isColdown; //true: coldown   false: mana
    public float coldown;
    public int mana;
    public bool isFinished;
    public bool instantSkill; //Si la habilidad de instantanea, el coldown empezará justo al iniciarse

    public abstract void Effect();

    public abstract void FinishEffect();

}
