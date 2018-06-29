using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterEquipment : MonoBehaviour {

    private CharacterStats stats;

    [SerializeField]
    protected List<Weapon> weapons;

    [SerializeField]
    protected List<Armor> armors; // armor is same plural

    [SerializeField]
    protected List<Accessory> accessories;

    void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }


    public Armor armorSample;

    void Update()
    {
        // Test Controller to Play With Equipment + StatModifiers
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            EquipArmor(armorSample);
        }

        if(Input.GetKeyUp(KeyCode.Alpha5))
        {
            UnequipArmor(armorSample);
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        weapon.Equip(stats);
        weapons.Add(weapon);
    }
   
    public void EquipArmor(Armor armor)
    {
        armor.Equip(stats);
        armors.Add(armor);
    }

    public void EquipAccessory(Accessory accessory)
    {
        accessory.Equip(stats);
        accessories.Add(accessory);
    }

    public void UnequipWeapon(Weapon weapon)
    {
        if (weapons.Remove(weapon))
        {
            weapon.Unequip(stats);
        }
    }

    public void UnequipArmor(Armor armor)
    {
        if(armors.Remove(armor))
        {
            armor.Unequip(stats);
        }
    }

    public void UnequipAccessory(Accessory accessory)
    {
        if(accessories.Remove(accessory))
        {
            accessory.Unequip(stats);
        }
    }

}
