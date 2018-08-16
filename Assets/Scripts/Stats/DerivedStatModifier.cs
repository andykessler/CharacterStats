using System;

// In future consider having tiered behavior (e.g. first 100 is 50%, second 100 is 25%)

[Serializable]
public class DerivedStatModifier : StatModifier
{
    public override float Value
    {
        get {
            return stat.Value * ratio;
        }
    }

    // The Stat which this modifier depends on
    public Stat stat;

    // Used to multiply against stat.Value
    public float ratio;

    // Values this stat modifier is bounded by
    public float min;
    public float max;

    // TODO Shorten number of constructor params
    // try removing StatType, value, and source (stat is source?)
    // min and max to a single bound/range object
    public DerivedStatModifier(StatType statType, StatModifierType modType, float value, object source, Stat stat, float ratio, float min, float max) : base(statType, modType, value, source)
    {
        this.stat = stat;
        this.ratio = ratio;
        this.min = min;
        this.max = max;
    }

    // TODO Shorten number of constructor params
    public DerivedStatModifier(StatType statType, StatModifierType modType, float value, Stat stat, float ratio, float min, float max) : this(statType, modType, value, null, stat, ratio, min, max) { }

}
