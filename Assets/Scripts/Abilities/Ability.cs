using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    public float cooldownMax;
    // TODO Use TargetSystem for initial targeting of ability
    public TargetSystem targetSystem;
    // TODO Consider "MultipleEffect" to group Effects under single Effect object
    public List<Effect> effects; 

    private float cooldownTimer;

    public void Use(Transform caster)
    {
        if (CanUse())
        {
            targetSystem.AcquireTargets(caster.position).ForEach(target =>
            {
                effects.ForEach(e => e.Apply(target));
            });
            cooldownTimer = cooldownMax;
        }
        else
        {
            Debug.Log(string.Format("{0} is still on cooldown: {1} ticks.", name, cooldownTimer));
        }
    }

    public void Tick(float deltaTime)
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= deltaTime;
        }
    }

    public bool CanUse()
    {
        return cooldownTimer <= 0;
    }
}
