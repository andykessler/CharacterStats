using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CreateAssetMenu()]
public class BranchEffect : Effect
{
    [Serializable]
    public class Branch
    {
        public Branch(TargetType targetTypes, List<Effect> effects)
        {
            this.targetTypes = targetTypes;
            this.effects = effects;
        }

        public Branch(TargetType targetTypes, Effect effect)
        {
            this.targetTypes = targetTypes;
            effects = new List<Effect>();
            effects.Add(effect);
        }

        public TargetType targetTypes;
        public List<Effect> effects;
    }

    public List<Branch> branches;

    public override void OnGUI()
    {
        branches.ForEach(b =>
        {
            b.targetTypes = (TargetType) EditorGUILayout.EnumFlagsField("Targets", b.targetTypes);
            EditorGUI.indentLevel++;
            b.effects.ForEach(x => x.OnGUI());
            // TODO Add/remove effects
            EditorGUI.indentLevel--;
        });
        // TODO Add/remove branches
    }

    public override void Apply(Transform target)
    {
        base.Apply(target);
        TargetType targetType = target.GetComponent<ITargetable>().TargetType;
        branches.ForEach(b => {
            if ((b.targetTypes & targetType) != TargetType.None)
                b.effects.ForEach(e => e.Apply(target));
        });
    }
}