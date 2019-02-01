using UnityEngine;

[CreateAssetMenu()]
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
            Debug.Log("Adding StatModifier Effect.");
            targetStats.AddStatModifier(statModifier);
        }
    }

    // what calls expire method?
    public override void Expire()
    {
        base.Expire();
        Debug.Log("Removing StatModifier Effect.");
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
