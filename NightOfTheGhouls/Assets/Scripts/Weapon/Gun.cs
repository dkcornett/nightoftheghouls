using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Data")]
    [SerializeField] private int mCurrentAmmo;

    [Header("Dependencies")]
    [SerializeField] private GunState mGunState;
    private GunData mGunData;

    public enum GunState
    {
        READY_TO_FIRE,
        FIRING,
        SHOT_COOLDOWN,
        RELOAD,
        EMPTY_MAG,
        NO_AMMO,
        CANNOT_SHOOT,
        INVALID = -1
    }

    public GunState CurrentGunState => mGunState;
    public GunData CurrentGunData => mGunData;

    public void Shoot()
    {
        if (mGunState == GunState.READY_TO_FIRE)
        {
            Debug.Log("Gun Firing");
            
            mGunState = GunState.FIRING;
        }
    }
}
