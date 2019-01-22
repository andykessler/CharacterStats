using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimedEffect : Effect
{
    public float duration;
    public float tickSpeed; // Tick needs to know deltaTick?

    public List<Effect> applyEffects;
    public List<Effect> tickEffects;
    public List<Effect> expireEffects;

    private float ticks;
    private float elapsed;

    private const float TURN_TICK = 1f; // 1 tick == 1 turn

    public override void Apply(Transform target)
    {
        base.Apply(target);
        if (applyEffects.Count > 0) applyEffects.ForEach(e => e.Apply(target));
    }

    public override void Tick() // pass deltaTick here? Time.deltaTime vs. TURN_TICK?
    {
        base.Tick();
        if (tickSpeed > 0 && tickEffects.Count > 0) {
            if (ticks >= tickSpeed) {
                tickEffects.ForEach(e => e.Apply(target));
                ticks = 0;
            }
            else {
                ticks += TURN_TICK;
            }
        }

        // duration == 0 is infinite time; means is managed or something external must remove.
        if (duration > 0) 
        {
            elapsed += TURN_TICK;
            if (elapsed >= duration)
            {
                Expire();
            }
        }

    }

    public override void Expire()
    {
        base.Expire();
        if (applyEffects.Count > 0) applyEffects.ForEach(e => e.Expire());
        if (expireEffects.Count > 0) expireEffects.ForEach(e => e.Apply(target));
    }

}
