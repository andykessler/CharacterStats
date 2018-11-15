using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class StatSheet {

    private Dictionary<StatType, Stat> stats;

    public StatSheet()
    {
        stats = new Dictionary<StatType, Stat>();
        StatType[] types = (StatType[]) System.Enum.GetValues(typeof(StatType));
        foreach (StatType type in types)
        {
            Stat s = new Stat(type);
            s.sheet = this;
            stats.Add(type, s);
        }
        
        foreach (StatType type in types)
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

    public Stat Get(StatType type)
    {
        return stats[type];
    }

}
