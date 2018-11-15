using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public StatType Type;
    public float BaseValue = 0; // TODO Remove public visibility to avoid rogue changes

    public List<StatModifier> StatModifiers;

    // TODO Bring in this value to constructor...
    public StatSheet sheet; // the StatSheet this Stat belongs to

    public delegate void ValueUpdatedHandler();
    private event ValueUpdatedHandler OnValueUpdated;

    [SerializeField]
    protected float _value; // caches value to reduce calculations
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

    public Stat()
    {
        StatModifiers = new List<StatModifier>();
        RegisterOnValueUpdatedHandler(CalculateFinalValue);
    }

    public Stat(float baseValue) : this()
    {
        BaseValue = baseValue;
        Invalidate();
    }

    public Stat(StatType type) : this()
    {
        Type = type;
    }

    public Stat(float baseValue, StatType type) : this()
    {
        BaseValue = baseValue;
        Type = type;
        Invalidate();
    }

    public virtual void AddModifier(StatModifier mod)
    {
        StatModifiers.Add(mod);
        OnValueUpdated();
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (StatModifiers.Remove(mod))
        {
            OnValueUpdated();
            return true;
        }
        return false;
    }

    public virtual void SetBaseValue(float baseValue) {
        this.BaseValue = baseValue;
        OnValueUpdated();
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

        if (didRemove)
        {
            OnValueUpdated();
        }
        return didRemove;
    }

    protected virtual void CalculateFinalValue() // should this still return float?
    {
        // To fix the inspector updating everytime which causes weird issues. I'm not saving results of the sort.
        // Since already sorting every time might as well...will find better way without heavy/any sorting soon.
        List<StatModifier> mods = new List<StatModifier>(StatModifiers);
        mods.Sort();

        float finalValue = StatFormulas.formulaMap[Type](sheet, BaseValue);
        float sumPercentAdd = 0;

        for (int i = 0; i < mods.Count; i++)
        {
            StatModifier mod = mods[i];
            switch (mod.ModType)
            {
                case StatModifierType.Flat:
                    finalValue += mod.Value;
                    break;

                case StatModifierType.PercentAdd:
                    sumPercentAdd += mod.Value;
                    // Since statModifiers is sorted, can assume once we change types, not longer will see more PercentAdd types...
                    if (i + 1 >= mods.Count || mods[i + 1].ModType != StatModifierType.PercentAdd)
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