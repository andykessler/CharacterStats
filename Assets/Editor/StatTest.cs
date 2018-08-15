using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class StatTest
{
    [Test]
    public void CreateStat_Test()
    {
        Stat stat = new Stat();
        Assert.That(stat.BaseValue, Is.EqualTo(0f));
        Assert.That(stat.Value, Is.EqualTo(0f));
        Assert.That(stat.StatModifiers, Is.Not.Null);
        Assert.That(stat.StatModifiers.Count, Is.EqualTo(0));
    }

    [Test]
    public void CreateStatWithValue_Test()
    {
        Stat stat = new Stat(3.5f);
        Assert.That(stat.BaseValue, Is.EqualTo(3.5f));
        Assert.That(stat.Value, Is.EqualTo(3.5f));
        Assert.That(stat.StatModifiers, Is.Not.Null);
        Assert.That(stat.StatModifiers.Count, Is.EqualTo(0));
    }

    [Test]
    public void AddModifier_Test()
    {
        StatModifier mod = new StatModifier(StatType.Strength, StatModifierType.Flat, 10f);
        Stat stat = new Stat();
   
        Assert.That(stat.StatModifiers.Count, Is.EqualTo(0));

        stat.AddModifier(mod);

        Assert.That(stat.StatModifiers, Contains.Item(mod));
        Assert.That(stat.StatModifiers.Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveModifier_Test()
    {
        StatModifier mod1 = new StatModifier(StatType.Strength, StatModifierType.Flat, 10f);
        StatModifier mod2 = new StatModifier(StatType.Strength, StatModifierType.PercentAdd, 0.05f);
        Stat stat = new Stat();

        stat.AddModifier(mod1);
        stat.AddModifier(mod2);

        Assert.That(stat.StatModifiers.Count, Is.EqualTo(2));

        stat.RemoveModifier(mod1);

        Assert.That(stat.StatModifiers.Count, Is.EqualTo(1));
        Assert.That(stat.StatModifiers, Is.Not.Contains(mod1));
    }

    [Test]
    public void CalculateFinalValue_Test()
    {
        float baseValue = 10f;
        Stat stat = new Stat(baseValue);
        StatModifier[] mods = new StatModifier[] {
            new StatModifier(StatType.Strength, StatModifierType.Flat, 50f),
            new StatModifier(StatType.Strength, StatModifierType.Flat, 40f),
            new StatModifier(StatType.Strength, StatModifierType.PercentAdd, 0.8f),
            new StatModifier(StatType.Strength, StatModifierType.PercentAdd, 0.2f),
            new StatModifier(StatType.Strength, StatModifierType.PercentMult, 1.0f),
            new StatModifier(StatType.Strength, StatModifierType.PercentMult, 1.5f),
        };
        float finalValue = (baseValue + 90f) * (2f) * (2f * 2.5f);

        Assert.That(stat.Value, Is.EqualTo(baseValue));
       
        foreach(var m in mods)
            stat.AddModifier(m);

        Assert.That(stat.Value, Is.EqualTo(finalValue));
    }

    [Test]
    public void CalculateFinalValueCorrectOrder_Test()
    {
        float baseValue = 10f;
        Stat stat = new Stat(baseValue);
        StatModifier[] mods = new StatModifier[] {
            new StatModifier(StatType.Strength, StatModifierType.PercentMult, 1.0f),
            new StatModifier(StatType.Strength, StatModifierType.Flat, 50f),
            new StatModifier(StatType.Strength, StatModifierType.PercentAdd, 0.8f),
            new StatModifier(StatType.Strength, StatModifierType.PercentMult, 1.5f),
            new StatModifier(StatType.Strength, StatModifierType.Flat, 40f),
            new StatModifier(StatType.Strength, StatModifierType.PercentAdd, 0.2f),
        };
        float finalValue = (baseValue + 90f) * (2f) * (2f * 2.5f);

        Assert.That(stat.Value, Is.EqualTo(baseValue));

        foreach (var m in mods)
            stat.AddModifier(m);

        Assert.That(stat.Value, Is.EqualTo(finalValue));
    }

    [Test]
    public void RemoveAllModifiersFromSource_Test()
    {
        object source1 = new object();
        object source2 = new object();
        Stat stat = new Stat();
        StatModifier[] mods = new StatModifier[]
        {
            new StatModifier(StatType.Strength, StatModifierType.Flat, 50f, source1),
            new StatModifier(StatType.Strength, StatModifierType.PercentAdd, 0.8f, source2),
            new StatModifier(StatType.Strength, StatModifierType.PercentMult, 1.0f, source2),
        };
        foreach(var m in mods)
            stat.AddModifier(m);

        Assert.That(stat.StatModifiers.Count, Is.EqualTo(3));

        stat.RemoveAllModifiersFromSource(source2);

        Assert.That(stat.StatModifiers.Count, Is.EqualTo(1));

        foreach (var m in stat.StatModifiers)
            Assert.That(m.Source, Is.Not.EqualTo(source2));
    }

}
