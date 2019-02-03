using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterStats : MonoBehaviour {

    public StatPresets presets;

    private StatSheet stats;
    public StatSheet Stats {
        get {
            return stats;
        }
    }

    public void AddStatModifier(StatModifier statModifier)
    {
        stats.Get(statModifier.StatType).AddModifier(statModifier);
    }

    public bool RemoveStatModifier(StatModifier statModifier)
    {
        return stats.Get(statModifier.StatType).RemoveModifier(statModifier);
    }

    public void Awake () {
        if(presets != null)
        {
            stats = new StatSheet(presets);
        }
        else
        {
            stats = new StatSheet();
        }
    }

    void Update () {
        
    }

    public Stat Get(StatType statType)
    {
        return Stats.Get(statType);
    }
}
