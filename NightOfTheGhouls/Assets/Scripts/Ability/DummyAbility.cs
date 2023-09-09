using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Dummy")]
public class DummyAbility : AbilityData
{
    public override bool ActivateAbility(GameObject user)
    {
        Debug.Log("Dummy ability active");
        return true;
    }

    public override void DeactivateAbility(GameObject user)
    {
        Debug.Log("Dummy ability active");
    }
}