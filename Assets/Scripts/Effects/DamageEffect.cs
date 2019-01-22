using UnityEngine;
using System.Collections;

public class DamageEffect : Effect
{

    public float damage = 1f;

    // what about SO.CreateInstance? does that use constructor?
    public DamageEffect(float damage)
    {
        this.damage = damage;
    }
    
    public override void Apply(Transform target)
    {
        base.Apply(target); // what is there to do in super class for an instant effect?
        Health health = target.GetComponent<Health>();
        if(health != null)
        {
            health.Damage(damage);
        }
    }
}
