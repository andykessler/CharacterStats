using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ITargetable
{
    [SerializeField]
    [EnumFlags]
    TargetType targetType;
    public TargetType TargetType
    {
        get {
            return targetType;
        }
    }
}
