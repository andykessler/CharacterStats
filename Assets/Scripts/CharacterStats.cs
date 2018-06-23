using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    [SerializeField]
    public List<Stat> statList;

    // now consider...do all character have every stat type?
    // if they dont we have to null check before evaluation.
    public Dictionary<StatType, Stat> stats;
    
    void Start () {
        stats = new Dictionary<StatType, Stat>();
        foreach(Stat s in statList)
        {
            if(!stats.ContainsKey(s.Type))
            {
                stats[s.Type] = s;
            }
            else
            {
                Debug.LogWarning("Duplicate StatType: " + s.Type);
            }
        }
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
