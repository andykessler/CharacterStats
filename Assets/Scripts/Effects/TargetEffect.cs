using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu()]
public class TargetEffect : Effect
{
    public TargetSystem targetSystem;

    public List<Effect> effects;

    public override void OnGUI()
    {
        targetSystem.radius = EditorGUILayout.FloatField(targetSystem.radius);
        targetSystem.distance = EditorGUILayout.FloatField(targetSystem.distance);
        targetSystem.arc = EditorGUILayout.FloatField(targetSystem.arc);
        targetSystem.maxTargets = EditorGUILayout.IntField(targetSystem.maxTargets);
        targetSystem.targetTypes = (TargetType) EditorGUILayout.EnumFlagsField(targetSystem.targetTypes);
        effects.ForEach(e => {
            e.OnGUI();
            // TODO Add/remove effects
        });
    }

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
