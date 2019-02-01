using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu()]
public class TargetEffect : Effect
{
    public TargetSystem targetSystem;

    public List<Effect> effects;

    public override void Apply(Transform target)
    {
        base.Apply(target);
        List<Transform> targets = targetSystem.AcquireTargets(target.position);
        foreach(Transform t in targets) {
            IEffectable e = t.GetComponent<IEffectable>();
            e.ApplyEffects(effects.ToArray());
        }
    }
}
