using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class DerivedStatTest
{
    [Test]
    public void CreateDerivedStat_Test()

     {
        float val1 = 10f, val2 = 1f;
        Stat stat1 = new Stat(val1, StatType.Dexterity);
        Stat stat2 = new Stat(val2, StatType.CriticalHit);

        Assert.That(stat1.Value, Is.EqualTo(val1));
        Assert.That(stat2.Value, Is.EqualTo(val2));

        float ratio = 0.1f;
        float min = 0f, max = 99f; // max to inf?
        DerivedStatModifier derivedStatModifier = new DerivedStatModifier(StatType.CriticalHit, StatModifierType.Flat, 0f, stat1, stat1, ratio, min, max);

        StatModifier statModifier = (StatModifier) derivedStatModifier;
        stat2.AddModifier(statModifier);

        Assert.That(stat1.Value, Is.EqualTo(val1));
        Assert.That(stat2.Value, Is.EqualTo(val2 + (ratio * val1)));
        Assert.That(stat2.StatModifiers, Is.Not.Null);
        Assert.That(stat2.StatModifiers.Count, Is.EqualTo(1));
    }
}
