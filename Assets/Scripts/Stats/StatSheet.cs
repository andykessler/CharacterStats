using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class StatSheet {

    private Dictionary<StatType, Stat> stats;

    public StatSheet()
    {
        stats = new Dictionary<StatType, Stat>();
        foreach (StatType type in StatFormulas.STAT_TYPES)
        {
            Stat s = new Stat(type);
            s.sheet = this;
            stats.Add(type, s);
        }
        RegisterStatFormulaDependencies();
    }

    // assumes we still want every Stat defined in map.
    public StatSheet(StatPresets presets) : this()
    {
        // this naming could be better.
        if(presets != null && presets.presets != null)
        {
            presets.presets.ForEach(p => stats[p.statType].BaseValue = p.baseValue);
        }
    }

    public Stat Get(StatType type)
    {
        return stats[type];
    }

    public void RegisterStatFormulaDependencies() {
        foreach (StatType type in stats.Keys)
        {
            Stat stat = stats[type];
            StatType[] deps = StatFormulas.dependencyMap[type];
            if(deps == null) continue;
            foreach(StatType dep in deps) 
            {
                stats[dep].RegisterOnValueUpdatedHandler(stat.Invalidate);
            }
        }
    }

}
