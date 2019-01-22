using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Currently is just AreaEffect
public class TargetEffect : Effect
{
    // TODO Extract to TargetSystem objects?
    public float radius;
    public int maxTargets;

    public List<Effect> effects;

    public override void Apply(Transform target)
    {
        base.Apply(target);
        Collider[] hitColliders = Physics.OverlapSphere(target.position, radius);
        int numTargets = 0;
        foreach(Collider c in hitColliders) {
            if(numTargets >= maxTargets) {
                break; // Reached max number of targets. Exit loop early.
            }
            effects.ForEach(e => c.SendMessage("ApplyEffect", e)); // TODO Implement 'ApplyEffect' messsage.
            numTargets++;
        }
    }
}
