using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, ITargetable
{
    [SerializeField]
    TargetType targetType;
    public TargetType TargetType
    {
        get {
            return targetType;
        }
    }
}
