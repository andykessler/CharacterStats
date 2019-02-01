using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour, ITickable {

    public List<Ability> abilities;

    void Update()
    {
        KeyCode key = KeyCode.Alpha1;
        foreach (Ability a in abilities)
        {
            if (key <= KeyCode.Alpha9 && Input.GetKeyUp(key))
            {
                Debug.Log("Casting " + a.name);
                a.Use(transform);
            }
            key++;
        }
    }

    public void Tick(float deltaTime)
    {
        foreach (Ability a in abilities)
        {
            a.Tick(deltaTime);
        }
    }
}
