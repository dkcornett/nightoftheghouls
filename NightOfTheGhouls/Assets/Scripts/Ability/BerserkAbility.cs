using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Berserk")]
public class BerserkAbility : AbilityData
{
    private Gun mPlayerUnitGun = null;
    private Health mPlayerUnitHealth = null;

    public override void ActivateAbility(GameObject user)
    {
        if (mPlayerUnitGun == null)     { mPlayerUnitGun = user.GetComponent<Gun>(); }
        if (mPlayerUnitHealth == null)  { mPlayerUnitHealth = user.GetComponent<Health>(); }

        float currentHealth = mPlayerUnitHealth.health;
        float maxHealth = mPlayerUnitHealth.maxHealth;
        float initialGunFireRate = mPlayerUnitGun.CurrentGunData.mFireRate;
        float initialMeleeFireRate = mPlayerUnitGun.CurrentMeleeData.mSwingRate;
        float initialReloadTime = mPlayerUnitGun.CurrentGunData.mReloadTime;

        float scalar = ModifierScale(0, maxHealth, 0.40f, 0, currentHealth);

        mPlayerUnitGun.SetNewGunFireRate(IncreaseByPercentage(scalar, initialGunFireRate));
        mPlayerUnitGun.SetNewMeleeFireRate(IncreaseByPercentage(scalar, initialMeleeFireRate));
        mPlayerUnitGun.SetNewReloadTime(DecreaseByPercentage(scalar, initialReloadTime));
    }

    private float ModifierScale(float healthMin, float healthMax, float maxModifier, float minModifier, float scalar)
    {
        float range1 = healthMax - healthMin;
        float range2 = minModifier - maxModifier;

        return (((scalar - healthMin) * range2) / range1) + maxModifier;
    }

    private float IncreaseByPercentage(float percentage, float val)
    {
        if (percentage <= 0)    { return val; }

        return val * (1 + percentage);
    }

    private float DecreaseByPercentage(float percentage, float val)
    {
        if (percentage <= 0)    { return val; }

        return val * percentage;
    }
}
