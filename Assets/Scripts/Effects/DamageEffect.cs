using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class DamageEffect : Effect
{
    public float damage = 1f;
    public DamageType damageType;

    public override void OnGUI()
    {
        damage = EditorGUILayout.FloatField("Damage", damage);
        damageType = (DamageType) EditorGUILayout.EnumPopup("Type", damageType);
    }
    
    public override void Apply(Transform target)
    {
        base.Apply(target); // what is there to do in super class for an instant effect?
        Health health = target.GetComponent<Health>();
        if(health != null)
        {
            health.Damage(damage);
        }
    }
}
