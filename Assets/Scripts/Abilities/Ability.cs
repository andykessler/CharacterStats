using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    // TODO Use TargetSystem for initial targeting
    // Replace with List<Effect> to apply to targets from TargetSystem.
    public TargetEffect targetEffect;
    public float cooldownMax;

    private float cooldownTimer;

    public void Use(Transform t)
    {
        if (CanUse())
        {
            targetEffect.Apply(t);
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
