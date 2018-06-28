using System;

[Serializable]
public class StatModifier : IComparable<StatModifier> {

    public StatModifierType Type;
    public float Value;
    public int Order;
    public object Source; // is this worth?

    public StatModifier(float value, StatModifierType type, int order, object source)
    {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }

    public StatModifier(float value, StatModifierType type) : this(value, type, (int) type, null) { }

    public StatModifier(float value, StatModifierType type, int order) : this(value, type, order, null) { }

    public StatModifier(float value, StatModifierType type, object source) : this(value, type, (int) type, source) { }

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
