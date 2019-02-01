using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TargetSystem {

    // I think you can infer most target systems through these properties
    // TODO Make use of all these properties:
    public float distance = 0; // 0 is caster's position
    public float arc = 360; // 360 is full circle
    public float radius = 0; // 0 is point/single target
    //public int minTargets = 0;
    public int maxTargets = 1;
    public List<TargetType> targetTypes;

    public List<Transform> AcquireTargets(Vector3 origin)
    {
        List<Transform> targets = new List<Transform>();
        Collider[] hitColliders = Physics.OverlapSphere(origin, radius);
        foreach (Collider c in hitColliders)
        {
            if (targets.Count >= maxTargets)
            {
                break; // Reached max number of targets. Exit loop early.
            }
            ITargetable target = c.GetComponentInParent<ITargetable>();
            if(target != null && IsValidTarget(target))
            {
                targets.Add(c.transform.parent);
            }
        }
        return targets;
    }

    public bool IsValidTarget(ITargetable target) {
        return targetTypes.Contains(target.TargetType);
    }

}
