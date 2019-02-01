using UnityEngine;

public abstract class Effect : ScriptableObject {

    protected Transform target;

    // What about the source of the effect? For example, damage modifiers?
    public virtual void Apply(Transform target)
    {
        this.target = target;
    }

    public virtual void Tick(float deltaTime)
    {

    }

    public virtual void Expire()
    {

    }
}
