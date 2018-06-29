using System;

[Serializable]
public class StatModifier : IComparable<StatModifier> {

    public StatType StatType;
    public StatModifierType ModType;
    public float Value;
    public int Order;
    public object Source; // is this worth?

    public StatModifier(StatType statType, StatModifierType modType, float value, int order, object source)
    {
        StatType = statType;
        ModType = modType;
        Value = value;
        Order = order;
        Source = source;
    }

    public StatModifier(StatType statType, StatModifierType modType, float value) : this(statType, modType, value, (int) modType, null) { }

    public StatModifier(StatType statType, StatModifierType modType, float value, int order) : this(statType, modType, value, order, null) { }

    public StatModifier(StatType statType, StatModifierType modType, float value, object source) : this(statType, modType, value, (int) modType, source) { }

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
