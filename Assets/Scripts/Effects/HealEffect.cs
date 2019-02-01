using UnityEngine;

[CreateAssetMenu()]
public class HealEffect : Effect
{
    public float heal = 1f;

    public override void Apply(Transform target)
    {
        base.Apply(target); // what is there to do in super class for an instant effect?
        Health health = target.GetComponent<Health>();
        if (health != null)
        {
            health.Heal(heal);
        }
    }
}
