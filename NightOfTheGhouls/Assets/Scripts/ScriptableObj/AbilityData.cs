using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityData : ScriptableObject
{
    public string mName;

    [Header("Stats")]
    public float mCooldown;
    public float mActiveTime;
    public bool mIsPassive;

    public virtual void ActivateAbility(GameObject user) { }
    public virtual void DeactivateAbility(GameObject user) { }
}
