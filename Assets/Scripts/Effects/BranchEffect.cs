using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu()]
public class BranchEffect : Effect
{
    [Serializable]
    public class Branch
    {
        public List<TargetType> targetTypes;
        public List<Effect> effects;
    }

    public List<Branch> branches;

    public override void Apply(Transform target)
    {
        base.Apply(target);
        TargetType targetType = target.GetComponent<ITargetable>().TargetType;
        branches.ForEach(b => {
            if (b.targetTypes.Contains(targetType))
                b.effects.ForEach(e => e.Apply(target));
        });
    }
}