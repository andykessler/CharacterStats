using System.Collections;
using System.Collections.Generic;

using static StatType; // apparently static imports requires C# 6+

public static class StatFormulas {

    public delegate float Formula(StatSheet sheet, float baseValue);
    private static Formula identity = (s, b) => b; // just returns base value

    public static readonly StatType[] STAT_TYPES = (StatType[]) System.Enum.GetValues(typeof(StatType));

    // There is an assumption that this hardcoded dependency map has no mistakes and reflects correctly against the formula map.
    // null list means the Stat has no dependencies.
    public static readonly Dictionary<StatType, StatType[]> dependencyMap = new Dictionary<StatType, StatType[]>
    {
        { Strength, null },
        { Dexterity, null },
        { Intellect, null },
        { Constitution, null },
        { CriticalHit, new StatType[] { Dexterity } },
        { MaxHealth, new StatType [] { Strength, Constitution } },
    };

    public static readonly Dictionary<StatType, Formula> formulaMap = new Dictionary<StatType, Formula>
    {
        { Strength, identity },
        { Dexterity, identity },
        { Intellect, identity },
        { Constitution, identity },
        { CriticalHit, (s, b) => b + (0.15f * s.Get(Dexterity).Value) }, // example "derived" formula
        { MaxHealth, (s, b) => b + (10 * s.Get(Constitution).Value) + (3 * s.Get(Strength).Value) },
    };

    
}
