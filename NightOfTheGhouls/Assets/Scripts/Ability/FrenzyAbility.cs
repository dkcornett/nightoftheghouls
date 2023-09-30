using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Frenzy")]
public class FrenzyAbility : AbilityData
{
    private Gun mPlayerUnitGun = null;

    // Dramatically increases melee attack speed (40%, stacking), but cannot use ranged weapon, 6 sec duration
    public override bool ActivateAbility(GameObject user)
    {
        if (mPlayerUnitGun == null) { mPlayerUnitGun = user.GetComponent<Gun>(); }

        float initialMeleeFireRate = mPlayerUnitGun.CurrentMeleeData.mSwingRate;
        mPlayerUnitGun.SetNewMeleeFireRate(IncreaseByPercentage(40, initialMeleeFireRate));
        mPlayerUnitGun.LockWeapon(false, true);

        Debug.Log("Frenzy Active");
        return true;
    }

    public override void DeactivateAbility(GameObject user)
    {
        if (mPlayerUnitGun == null) { mPlayerUnitGun = user.GetComponent<Gun>(); }

        float initialMeleeFireRate = mPlayerUnitGun.CurrentMeleeData.mSwingRate;
        mPlayerUnitGun.SetNewMeleeFireRate(DecreaseByPercentage(40, initialMeleeFireRate));
        mPlayerUnitGun.LockWeapon(true, false);

        Debug.Log("Frenzy Deactive");
    }

    private float IncreaseByPercentage(float percentage, float val)
    {
        if (percentage <= 0) { return val; }

        return val * (1 + percentage);
    }

    private float DecreaseByPercentage(float percentage, float val)
    {
        if (percentage <= 0) { return val; }

        return val * percentage;
    }
}
