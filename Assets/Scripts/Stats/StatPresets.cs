using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class StatPresets : ScriptableObject
{

    [Serializable]
    public class Preset
    {
        public StatType statType;
        public float baseValue;
    }

    public List<Preset> presets;
    
}
