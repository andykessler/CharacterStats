using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject {
    public string ItemName;
    public Sprite Icon;
    public Quality Quality;
    public uint MaxStackSize; // set to 1 for not stackable

    // Consider fields like Weight, Value, Soulbound
}
