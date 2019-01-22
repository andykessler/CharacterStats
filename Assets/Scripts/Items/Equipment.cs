using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Equipment : Item
{
    [SerializeField]
    public List<StatModifier> StatModifiers;

    public virtual void Equip(CharacterStats cs)
    {
        foreach(StatModifier mod in StatModifiers)
        {
            cs.Stats.Get(mod.StatType).AddModifier(mod);
        }
    }

    public virtual void Unequip(CharacterStats cs)
    {
        foreach (StatModifier mod in StatModifiers)
        {
            cs.Stats.Get(mod.StatType).RemoveModifier(mod);
        }
    }
}
