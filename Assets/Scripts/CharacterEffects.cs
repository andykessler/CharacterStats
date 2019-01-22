using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour {

    private List<Effect> effects;

    private DamageEffect damageEffect;
    private TimedEffect timedEffect;

	// Use this for initialization
	void Start () {
        damageEffect = ScriptableObject.CreateInstance<DamageEffect>();
        damageEffect.damage = 10;

        timedEffect = ScriptableObject.CreateInstance<TimedEffect>();
        timedEffect.duration = 10;
        timedEffect.tickSpeed = 1;
        timedEffect.tickEffects.Add(damageEffect);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Alpha8))
        {
            damageEffect.Apply(transform);
        }

        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            timedEffect.Apply(transform);
        }
    }

    
}
