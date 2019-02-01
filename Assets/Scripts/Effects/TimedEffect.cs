using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class TimedEffect : Effect
{
    public float duration;
    public float tickSpeed;

    public List<Effect> applyEffects;
    public List<Effect> tickEffects;
    public List<Effect> expireEffects;

    private const float TURN_TICK = 1f; // 1 tick == 1 turn

    private float ticks;
    private float elapsed;

    private IEffectable effectable; // what about multiple effectables?

    public TimedEffect()
    {
        duration = 0;
        tickSpeed = 0;
        applyEffects = new List<Effect>();
        tickEffects = new List<Effect>();
        expireEffects = new List<Effect>();
    }

    public override void Apply(Transform target)
    {
        base.Apply(target);
        effectable = target.GetComponent<IEffectable>();
        effectable.RegisterEffect(this);
        if (applyEffects.Count > 0) applyEffects.ForEach(e => e.Apply(target));
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        if (tickSpeed > 0 && tickEffects.Count > 0) {
            ticks += deltaTime;
            if (ticks >= tickSpeed) {
                tickEffects.ForEach(e => e.Apply(target));
                ticks = 0;
            }
        }

        // duration == 0 is infinite time; means is managed or something external must remove.
        if (duration > 0) 
        {
            elapsed += deltaTime;
            if (elapsed >= duration)
            {
                Expire();
            }
        }
    }

    public override void Expire()
    {
        base.Expire();
        if (applyEffects.Count > 0) applyEffects.ForEach(e => e.Expire()); // what about those that don't need to be expired, but are applied at begining?
        if (expireEffects.Count > 0) expireEffects.ForEach(e => e.Apply(target));
        effectable.RemoveEffect(this);
    }

}
