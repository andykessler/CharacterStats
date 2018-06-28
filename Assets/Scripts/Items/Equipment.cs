using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Equipment : Item
{
    // TODO Map StatType to StatModifier, needs to be editable through Unity inspector
    [SerializeField]
    public List<StatModifier> StatModifiers; 

    // TODO Consider equip/unequip logic here
}
