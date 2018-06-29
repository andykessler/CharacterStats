using UnityEngine;

[CreateAssetMenu]
public class Accessory : Equipment
{
    public AccessoryType AccessoryType;

    public override void Equip(CharacterStats cs)
    {
        base.Equip(cs);
    }

    public override void Unequip(CharacterStats cs)
    {
        base.Unequip(cs);
    }
}