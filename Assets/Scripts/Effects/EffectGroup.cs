using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGroup : Effect
{
    public List<Effect> effects;

    public override void Apply(Transform target)
    {
        base.Apply(target);
        effects.ForEach(e => e.Apply(target));
    }

    public override void Tick()
    {
        base.Tick();
        effects.ForEach(e => e.Tick());
    }

    public override void Expire()
    {
        base.Expire();
        effects.ForEach(e => e.Expire());
    }

}
