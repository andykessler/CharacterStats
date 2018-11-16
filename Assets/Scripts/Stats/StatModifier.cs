using System;

[Serializable]
public class StatModifier : IComparable<StatModifier> {

    // Should StatType be able to change after instantiation?
    public StatType StatType; 
    
    // Should StatModifierType be able to change after instantiation?
    public StatModifierType ModType;

    private float _value;
    public float Value 
    {
        get {
            return _value;
        }
        set {
            _value = value;
            OnStatModifierUpdated();
        }
    }

    public object Source; // is this worth?

    public delegate void StatModifierUpdatedHandler();
    private event StatModifierUpdatedHandler OnStatModifierUpdated;

    public void RegisterOnValueUpdatedHandler(StatModifierUpdatedHandler handler)
    {
        OnStatModifierUpdated += handler;
    }

    public void UnregisterOnValueUpdatedHandler(StatModifierUpdatedHandler handler)
    {
        OnStatModifierUpdated -= handler;
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
        _value = value; // if set with Value, will get null pointer since Updated handler has nothing registered yet
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
