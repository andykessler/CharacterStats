using System.Collections.Generic;
using UnityEngine;

public class CharacterEffects : MonoBehaviour, IEffectable, ITickable {

    [SerializeField]
    private List<Effect> effects;
    public List<Effect> Effects {
        get {
            return effects;
        }
    }



    private Queue<Effect> removalQueue;
    
	void Start () {
        effects = new List<Effect>();
        removalQueue = new Queue<Effect>();
    }
	
	public void Tick(float deltaTime) {
        foreach(Effect e in Effects){
            e.Tick(1);
        }
        ClearRemovalQueue(); // delay removals to not modify enumerator above
    }

    public void ApplyEffects(Effect[] effects)
    {
        foreach(Effect e in effects)
        {
            ApplyEffect(e);
        }
    }

    public void ApplyEffect(Effect effect)
    {
        effect.Apply(transform);
    }

    public void RemoveEffect(Effect effect)
    {
        removalQueue.Enqueue(effect);
    }

    public void RegisterEffect(Effect effect)
    {
        Effects.Add(effect);
    }

    private void ClearRemovalQueue()
    {
        while(removalQueue.Count > 0)
        {
            Effect e = removalQueue.Dequeue();
            if(!Effects.Remove(e))
            {
                Debug.Log("Error while removing " + e.name);
            }
        }
    }
}
