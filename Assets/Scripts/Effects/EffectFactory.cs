using UnityEngine;
using System.Collections;

// Builder pattern?
public static class EffectFactory
{
    public static DamageEffect DamageEffect(DamageEffect damageEffect)
    {
        return DamageEffect(damageEffect.damage, damageEffect.damageType);
    }

    public static DamageEffect DamageEffect(float damage, DamageType damageType)
    {
        DamageEffect e = ScriptableObject.CreateInstance<DamageEffect>();
        e.damage = damage;
        e.damageType = damageType;
        return e;
    }

}
