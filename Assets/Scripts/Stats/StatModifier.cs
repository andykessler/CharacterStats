using System;
using UnityEngine;

[Serializable]
public class StatModifier : IComparable<StatModifier> {

    public StatType StatType;
    public StatModifierType ModType;
    public object Source; // is this worth?

    [SerializeField]
    protected float _value;
    public virtual float Value {
        get {
            return _value;
        }
    }
    
    protected int Order
    {
        get {
            return (int) ModType;
        }
    }

    public StatModifier(StatType statType, StatModifierType modType, float value, object source)
    {
        StatType = statType;
        ModType = modType;
        _value = value;
        Source = source;
    }

    public StatModifier(StatType statType, StatModifierType modType, float value) : this(statType, modType, value, null) { }

    // What about comparing by other fields? e.g. Value
    public int CompareTo(StatModifier other)
    {
        if (Order < other.Order)
            return -1;
        else if (Order > other.Order)
            return 1;
        return 0; //if (Order == other.Order)
    }



}
