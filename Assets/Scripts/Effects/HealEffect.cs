using UnityEngine;
using System.Collections;

public class HealEffect : Effect
{
    public override void Apply(Transform target)
    {
        base.Apply(target); // what is there to do in super class for an instant effect?
        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(1f);
        }
    }
}
