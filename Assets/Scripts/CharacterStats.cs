using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterStats : MonoBehaviour {

    private StatSheet stats;
    public StatSheet Stats {
        get {
            return stats;
        }
    }

    void Start () {
        stats = new StatSheet();    
    }

    void Update () {
        
    }
}
