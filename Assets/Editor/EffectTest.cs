using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class EffectTest
{
    [Test]
    public void CreateEffect_Test()
    {
        Effect e = ScriptableObject.CreateInstance<DamageEffect>();
        Assert.That(e, Is.Not.Null);
    }

}
