using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat
{

    public StatType Type;

    private float baseValue;
    public float BaseValue {
        get {
            return baseValue;
        }
        set {
            baseValue = value;
            OnValueUpdated();
        }
    }

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
        modMap[mod.ModType].Add(mod);
        mod.RegisterOnValueUpdatedHandler(this.Invalidate);
        OnValueUpdated();
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (StatModifiers.Remove(mod))
        {
            OnValueUpdated();
            modMap[mod.ModType].Remove(mod);
            mod.UnregisterOnValueUpdatedHandler(this.Invalidate);
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false; // one-way flag to know if an update is needed
        // iterate backwards since potentially removing items.
        for (int i = StatModifiers.Count - 1; i >= 0; i--)
        {
            if (StatModifiers[i].Source == source)
            {
                StatModifier mod = StatModifiers[i];
                StatModifiers.RemoveAt(i);
                modMap[mod.ModType].Remove(mod);
                mod.UnregisterOnValueUpdatedHandler(this.Invalidate);
                didRemove = true;
            }
        }

        if (didRemove)
        {
            OnValueUpdated();
        }
        return didRemove;
    }

    protected virtual void CalculateFinalValue()
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
        OnValueUpdated();
    }
}