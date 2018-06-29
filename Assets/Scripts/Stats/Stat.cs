using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class Stat
{
    // TODO Add event to subscribe to update events

    public StatType Type;
    public float BaseValue = 0; // FIXME if you change with Inspector it doesn't update Value
    protected float lastBaseValue = 0;
    protected bool isDirty = true;

    [SerializeField] // FIXME display latest value in Inspector, force update?
    protected float _value;
    public virtual float Value
    {
        get {
            if (isDirty || lastBaseValue != BaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    // TODO Find a way to read in Unity inspector
    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public Stat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public Stat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    public Stat(StatType type) : this()
    {
        Type = type;
    }

    public Stat(float baseValue, StatType type) : this()
    {
        BaseValue = baseValue;
        Type = type;
    }

    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(); // FIXME Don't sort every time
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if(statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }
    
    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            switch(mod.ModType)
            {
                case StatModifierType.Flat:
                    finalValue += mod.Value;
                    break;

                case StatModifierType.PercentAdd:
                    sumPercentAdd += mod.Value;
                    // Since statModifiers is sorted, can assume once we change types, not longer will see more PercentAdd types...
                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].ModType != StatModifierType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                    break;

                case StatModifierType.PercentMult:
                    finalValue *= 1 + mod.Value;
                    break;
            }
        }

        // Workaround for float calculation errors, like displaying 12.00002 instead of 12
        return (float) Math.Round(finalValue, 4);
    }
}