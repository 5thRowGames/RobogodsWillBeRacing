using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityTemplate : MonoBehaviour {

    [Header("For throwing things")]
    public bool hasProjectile = false;
    public GameObject projectile;
    public Transform projectileOrigin;

    [Header("Costs")]
    public bool hasCooldown = false;
    public float cooldown;
    public bool hasMagicCost = false;
    public float magicCost;

    [Header("Target")]
    public bool hasTarget = false;
    public GameObject target;

    public abstract void Effect();
}
