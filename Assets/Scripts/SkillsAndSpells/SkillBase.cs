using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public float duration;
    public bool isColdown; //true: coldown   false: mana
    public float coldown;
    public int mana;

    public abstract void Effect();

}
