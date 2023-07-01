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
    public GameObject mGunFlash;
    public BasicMouseMovement mMovement;

    private void Awake()
    {
        mCurrentMag = mGunData.mMagSize;
        mCurrentAmmo = mGunData.mMaxAmmo;
        mCurrentCooldown = mGunData.mFireRate;
        mGunState = GunState.READY_TO_FIRE;

        InvokeRepeating("Shoot", 0.0f, mCurrentCooldown);
    }

    public enum GunState
    {
        READY_TO_FIRE,
        FIRING,
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
        float targetDist = float.PositiveInfinity;
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
        if (target == null) 
        {
            mGunState = GunState.READY_TO_FIRE;
            return; 
        }

        transform.LookAt(target.transform);

        // [ALEX 6/10/23] TO-DO: Replace with a more efficiant vfx system once we have that in
        Quaternion rot = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
        Instantiate(mGunFlash, transform.position, rot);

        if ((mGunData.mAccuracy == 100) || (Random.Range(0, 100) < mGunData.mAccuracy))
        {
            Health targetHealth = target.GetComponent<Health>();
            float newDamage = mGunData.mDamage;
            if ((mGunData.mCritChance == 100) || (Random.Range(0, 100) < mGunData.mCritChance)) { newDamage *= mGunData.mCritModifier; }
            targetHealth.dealDamage(newDamage, gameObject);
        }

        mCurrentMag--;
        if (mCurrentMag < 1) 
        { 
            StartCoroutine(Reload());
            return;
        }
    }

    private IEnumerator Reload()
    {
        if ((mCurrentAmmo - mGunData.mMagSize) < 0)
        {
            mGunState = GunState.NO_AMMO;
        }
        else
        {
            mGunState = GunState.RELOAD;
            yield return new WaitForSeconds(mGunData.mReloadTime);

            mGunState = GunState.READY_TO_FIRE;
            mCurrentMag = mGunData.mMagSize;
            mCurrentAmmo -= mGunData.mMagSize;
        }
    }

    private bool CanFire()
    {
        return (mGunState == GunState.READY_TO_FIRE) && (!mMovement.mIsMoving);
    }
}
