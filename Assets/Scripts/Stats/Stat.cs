using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public StatType Type;
    public float BaseValue = 0;

    public delegate void ValueUpdatedHandler();
    private event ValueUpdatedHandler OnValueUpdated;

    [SerializeField]
    protected float _value;
    public virtual float Value
    {
        get {
            return _value;
        }
    }

    public void RegisterOnValueUpdatedHandler(ValueUpdatedHandler handler)
    {
        OnValueUpdated += handler;
    }

    public void UnregisterOnValueUpdatedHandler(ValueUpdatedHandler handler)
    {
        OnValueUpdated -= handler;
    }
  
    public List<StatModifier> StatModifiers;

    public Stat()
    {
        StatModifiers = new List<StatModifier>();
        RegisterOnValueUpdatedHandler(CalculateFinalValue);
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
        StatModifiers.Add(mod);
        OnValueUpdated();
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if(StatModifiers.Remove(mod))
        {
            OnValueUpdated();
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
                didRemove = true;
                StatModifiers.RemoveAt(i);
            }
        }

        if(didRemove)
        {
            OnValueUpdated();
        }
        return didRemove;
    }
    
    protected virtual void CalculateFinalValue() // should this still return float?
    {
        StatModifiers.Sort(); // FIXME Better, but still don't sort every time...

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
        _value = (float) Math.Round(finalValue, 4);
    }

    public void Invalidate()
    {
        OnValueUpdated(); // alias for now I guess?
    }
}