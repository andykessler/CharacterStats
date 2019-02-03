using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class EffectTest
{
    [Test]
    public void CreateDamageEffectDefault_Test()
    {
        DamageEffect e = ScriptableObject.CreateInstance<DamageEffect>();
        Assert.That(e, Is.Not.Null);
        Assert.That(e.damage, Is.EqualTo(1f)); // default value
    }

    [Test]
    public void CreateDamageEffectCopy_Test()
    {
        DamageEffect e1 = ScriptableObject.CreateInstance<DamageEffect>();
        e1.damageType = DamageType.Fire;
        DamageEffect e2 = Object.Instantiate(e1);

        Assert.That(e2.damage, Is.EqualTo(e1.damage));
        Assert.That(e2.damageType, Is.EqualTo(e1.damageType));

        e2.damage *= 2;

        Assert.That(e2.damage, Is.EqualTo(e1.damage * 2)); // checks that e1 didn't change
    }

    [Test]
    public void ApplyDamageEffect_Test()
    {
        // TODO Add NSubstitute for mocks
        GameObject go = new GameObject("Enemy");
        CharacterStats stats = go.AddComponent<CharacterStats>();
        stats.Awake(); 
        Stat healthStat = stats.Get(StatType.MaxHealth);
        healthStat.BaseValue = 100f;
        Health health = go.AddComponent<Health>();
        health.Start();
        DamageEffect e = ScriptableObject.CreateInstance<DamageEffect>();
        e.Apply(go.transform);

        Assert.That(health.Value, Is.EqualTo(100f - e.damage));
    }

    //[Test]
    //public void ApplyBranchEffect_Test()
    //{
    //    DamageEffect d = ScriptableObject.CreateInstance<DamageEffect>();
    //    HealEffect h = ScriptableObject.CreateInstance<HealEffect>();
    //    BranchEffect b = ScriptableObject.CreateInstance<BranchEffect>();
    //    b.branches.Add(new BranchEffect.Branch(TargetType.Enemy, d));
    //    b.branches.Add(new BranchEffect.Branch(TargetType.Ally, h));
    //}
}
