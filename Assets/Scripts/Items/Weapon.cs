using UnityEngine;

[CreateAssetMenu]
public class Weapon : Equipment
{
    public WeaponSlot WeaponSlot;
    public WeaponType WeaponType;

    public override void Equip(CharacterStats cs)
    {
        base.Equip(cs);
    }

    public override void Unequip(CharacterStats cs)
    {
        base.Unequip(cs);
    }
}
