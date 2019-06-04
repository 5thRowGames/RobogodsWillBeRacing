using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSphere : AbilityTemplate
{
    public override void Effect()
    {
        if (hasProjectile)
        {
            GameObject sphere = Instantiate(projectile, projectileOrigin.position, transform.rotation);
        }
    }
}
