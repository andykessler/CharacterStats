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

        Assert.That(stat.Value, Is.EqualTo(0f));
        Assert.That(stat.StatModifiers, Is.Not.Null);
        Assert.That(stat.StatModifiers.Count, Is.EqualTo(0));
    }

    [Test]
    public void CreateStatWithValue_Test()
    {
        Stat stat = new Stat(3.5f);

        Assert.That(stat.Value, Is.EqualTo(3.5f));
        Assert.That(stat.StatModifiers, Is.Not.Null);
        Assert.That(stat.StatModifiers.Count, Is.EqualTo(0));
    }

    [Test]
    public void AddModifier_Test()
    {
        StatModifier mod = new StatModifier(10f, StatModifierType.Flat);
        Stat stat = new Stat();
   
        Assert.That(stat.StatModifiers.Count, Is.EqualTo(0));

        stat.AddModifier(mod);

        Assert.That(stat.StatModifiers, Contains.Item(mod));
        Assert.That(stat.StatModifiers.Count, Is.EqualTo(1));
    }

    [Test]
    public void RemoveModifier_Test()
    {
        StatModifier mod1 = new StatModifier(10f, StatModifierType.Flat);
        StatModifier mod2 = new StatModifier(.05f, StatModifierType.PercentAdd);
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
            new StatModifier(50f, StatModifierType.Flat),
            new StatModifier(40f, StatModifierType.Flat),
            new StatModifier(0.8f, StatModifierType.PercentAdd),
            new StatModifier(0.2f, StatModifierType.PercentAdd),
            new StatModifier(1.0f, StatModifierType.PercentMult),
            new StatModifier(1.5f, StatModifierType.PercentMult),
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
            new StatModifier(1.0f, StatModifierType.PercentMult),
            new StatModifier(50f, StatModifierType.Flat),
            new StatModifier(0.8f, StatModifierType.PercentAdd),
            new StatModifier(1.5f, StatModifierType.PercentMult),
            new StatModifier(40f, StatModifierType.Flat),
            new StatModifier(0.2f, StatModifierType.PercentAdd),
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
            new StatModifier(50f, StatModifierType.Flat, source1),
            new StatModifier(0.8f, StatModifierType.PercentAdd, source2),
            new StatModifier(1.0f, StatModifierType.PercentMult, source2),
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
