using System;

public enum StatModifierType
{
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

//[Serializable]
public class StatModifier {

    public readonly float Value;
    public readonly StatModifierType Type;
    public readonly int Order;
    public readonly object Source; // is this worth?

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
}
