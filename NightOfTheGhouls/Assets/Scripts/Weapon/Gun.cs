using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Data")]
    [SerializeField] private int mCurrentAmmo;
    [SerializeField] private int mCurrentMag;
    [SerializeField] private float mCurrentCooldown;

    [Header("Dependencies")]
    [SerializeField] private GunState mGunState;
    public GunData mGunData;

    private void Start()
    {
        mCurrentMag = mGunData.mMagSize;
    }

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
        if (!CanFire()) { return; }

        mGunState = GunState.FIRING;
        FindTargets();
    }


    private void FindTargets()
    {
        float targetDist = -1;
        Collider target = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, mGunData.mRange);
        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.tag != "Zombie") { continue; }

            float dist = Vector3.Distance(transform.position, collider.transform.position);
            if (dist < targetDist)
            {
                target = collider;
                targetDist = dist;
            }
        }

        FireGun(target);
    }

    private void FireGun(Collider target)
    {
        if (target == null) { return; }

        transform.LookAt(target.transform);

        Health targetHealth = target.GetComponent<Health>();
        targetHealth.dealDamage(mGunData.mDamage, gameObject);

        mGunState = GunState.SHOT_COOLDOWN;
        Debug.Log("FIRE");

        mCurrentMag--;
        if (mCurrentMag < 1) { Reload(); }
    }

    private void Reload()
    {

    }

    private void ReloadFinished()
    {
        mGunState = GunState.READY_TO_FIRE;
    }

    private bool CanFire()
    {
        return (mGunState == GunState.READY_TO_FIRE);
    }
}
