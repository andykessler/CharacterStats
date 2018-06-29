using System;
using System.Collections.Generic;
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
                StatModifiers.Sort(); // FIXME Better, but still don't sort every time...
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }
  
    public List<StatModifier> StatModifiers;

    public Stat()
    {
        StatModifiers = new List<StatModifier>();
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
        StatModifiers.Add(mod);
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if(StatModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;
        for (int i = StatModifiers.Count - 1; i >= 0; i--)
        {
            if (StatModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                StatModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }
    
    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < StatModifiers.Count; i++)
        {
            StatModifier mod = StatModifiers[i];
            switch(mod.ModType)
            {
                case StatModifierType.Flat:
                    finalValue += mod.Value;
                    break;

                case StatModifierType.PercentAdd:
                    sumPercentAdd += mod.Value;
                    // Since statModifiers is sorted, can assume once we change types, not longer will see more PercentAdd types...
                    if (i + 1 >= StatModifiers.Count || StatModifiers[i + 1].ModType != StatModifierType.PercentAdd)
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

    public void Invalidate()
    {
        isDirty = true;
    }
}