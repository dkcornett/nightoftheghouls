using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData : ScriptableObject
{
    public string mName;

    [Header("Stats")]
    public float mCooldown;
    public float mActiveTime;
    public AbilityType mAbilityType;

    public enum AbilityType
    {
        ACTIVE,
        PASSIVE,
        AIMED,
        INVALID_TYPE = -1
    }

    public virtual bool ActivateAbility(GameObject user) { return false; }
    public virtual void DeactivateAbility(GameObject user) { }
}
