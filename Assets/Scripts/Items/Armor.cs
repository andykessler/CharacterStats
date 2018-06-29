using UnityEngine;

[CreateAssetMenu]
public class Armor : Equipment
{
    public ArmorSlot ArmorSlot;
    public ArmorType ArmorType;

    public override void Equip(CharacterStats cs)
    {
        base.Equip(cs);
    }

    public override void Unequip(CharacterStats cs)
    {
        base.Unequip(cs);
    }
}
