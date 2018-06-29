using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterStats : MonoBehaviour {

    [SerializeField]
    public List<Stat> statList;

    // now consider...do all character have every stat type?
    // if they dont we have to null check before evaluation.
    private Dictionary<StatType, Stat> stats;
    
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
            OnValidate();
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            StatType statType = StatType.Strength;
            Stat s = stats[statType];
            s.AddModifier(new StatModifier(statType, StatModifierType.Flat, 1f));
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            StatType statType = StatType.Strength;
            Stat s = stats[StatType.Strength];
            s.AddModifier(new StatModifier(statType, StatModifierType.PercentAdd, .5f));
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            StatType statType = StatType.Strength;
            Stat s = stats[StatType.Strength];
            s.AddModifier(new StatModifier(statType, StatModifierType.PercentMult, 1f));
        }
    }

    private void OnValidate()
    {
        string msg = "";
        foreach(Stat s in statList)
        {
            s.Invalidate();
            msg += string.Format("{0}: {1}\n", s.Type, s.Value); // this will update inspector
        }
        Debug.Log(msg);
    }

    public Stat GetStat(StatType type)
    {
        return stats[type];
    }
}
