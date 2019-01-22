using UnityEngine;
using System.Collections;

public class StatModifierEffect : Effect
{

    public StatModifier statModifier;

    private CharacterStats targetStats;

    public override void Apply(Transform target)
    {
        base.Apply(target);
        targetStats = target.GetComponent<CharacterStats>();
        if(targetStats != null)
        {
            targetStats.AddStatModifier(statModifier);
            Debug.Log("Added StatModifier Effect.");
        }
    }

    // what calls expire method?
    public override void Expire()
    {
        base.Expire();
        bool removed = targetStats.RemoveStatModifier(statModifier);
        if (removed)
        {
            Debug.Log("Removed StatModifier Effect.");
        }
        else
        {
            Debug.LogError("Failed to remove StatModifier Effect!");
        }
    }
}
