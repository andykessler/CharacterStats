using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat
{
    public StatType Type;
    public float BaseValue = 0; // TODO Remove public visibility to avoid rogue changes

    public List<StatModifier> StatModifiers;
    private Dictionary<StatModifierType, List<StatModifier>> modMap;

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
        modMap = new Dictionary<StatModifierType, List<StatModifier>>();
        foreach(StatModifierType modType in System.Enum.GetValues(typeof(StatModifierType)))
        {
            modMap.Add(modType, new List<StatModifier>());
        }
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
        modMap[mod.ModType].Add(mod);
        OnValueUpdated();
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (StatModifiers.Remove(mod))
        {
            OnValueUpdated();
            modMap[mod.ModType].Remove(mod);
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

    protected virtual void CalculateFinalValue() // should this return float?
    {
        float derivedValue = StatFormulas.formulaMap[Type](sheet, BaseValue);
        float flatSum = modMap[StatModifierType.Flat].Sum(x => x.Value);
        float percentSum = 1f + modMap[StatModifierType.PercentAdd].Sum(x => x.Value);
        float percentProduct = 1f; // there is Linq Aggregate function but had type issues
        foreach(StatModifier m in modMap[StatModifierType.PercentMult])
        {
            percentProduct *= 1f + m.Value;
        }

        float finalValue = (derivedValue + flatSum) * percentSum * percentProduct;

        // Workaround for float calculation errors, like displaying 12.00002 instead of 12
        _value = (float) Math.Round(finalValue, 4);
    }

    public void Invalidate()
    {
        OnValueUpdated(); // alias for now I guess?
    }
}