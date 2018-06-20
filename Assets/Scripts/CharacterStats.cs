using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Consider moving to Stat.cs?
public enum StatType
{
    Strength,
    Dexterity,
    Intellect,
    Constitution,
}

public class CharacterStats : MonoBehaviour {

    // TODO Editor with field for each StatType instead of hardcoding vars
    public float baseStr, baseDex, baseInt, baseCon;
    public Dictionary<StatType, Stat> stats;

	void Start () {
        stats = new Dictionary<StatType, Stat>();
        stats.Add(StatType.Strength, new Stat(baseStr));
        stats.Add(StatType.Dexterity, new Stat(baseDex));
        stats.Add(StatType.Intellect, new Stat(baseInt));
        stats.Add(StatType.Constitution, new Stat(baseCon));
    }

    void Update () {
        // Test Controller to Play With StatModifiers
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Stat s = stats[StatType.Strength];
            string msg = string.Format("Base: {0} Final: {1}", s.BaseValue, s.Value);
            Debug.Log(msg);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Stat s = stats[StatType.Strength];
            s.AddModifier(new StatModifier(1f, StatModifierType.Flat));
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Stat s = stats[StatType.Strength];
            s.AddModifier(new StatModifier(.5f, StatModifierType.PercentAdd));
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Stat s = stats[StatType.Strength];
            s.AddModifier(new StatModifier(1f, StatModifierType.PercentMult));
        }
    }
}
