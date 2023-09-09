using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float mTimePassed = 0.0f;

    [Header("Weapon Data")]
    [SerializeField] private int mCurrentAmmo;
    [SerializeField] private int mCurrentMag;
    [SerializeField] private float mCurrentGunFireRate;
    [SerializeField] private float mCurrentMeleeSwingRate;
    [SerializeField] private float mCurrentReloadTime;
    [SerializeField] private bool mIsInfiniteMag;
    [SerializeField] private bool mIsInfiniteAmmo;

    [Header("Unit Weapons")]
    public GunData mGunData;
    public MeleeData mMeleeData;

    private bool mGunOrMelee;
    private bool mLockWeapon;

    [Header("Dependencies")]
    [SerializeField] private GunState mGunState;
    public GameObject mGunFlash;
    public BasicMouseMovement mMovement;

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
    public MeleeData CurrentMeleeData => mMeleeData;
    public float CurrentAmmo => mCurrentAmmo;

    private void Awake()
    {
        mCurrentMag = mGunData.mMagSize;
        mCurrentAmmo = mGunData.mMaxAmmo;
        mCurrentGunFireRate = mGunData.mFireRate;
        mCurrentMeleeSwingRate = mMeleeData.mSwingRate;
        mCurrentReloadTime = mGunData.mReloadTime;
        mGunState = GunState.READY_TO_FIRE;
        mGunOrMelee = true;
        mLockWeapon = false;
    }

    private void Update()
    {
        mTimePassed += Time.deltaTime;
        if (mGunOrMelee ? (mTimePassed > mCurrentGunFireRate) : (mTimePassed > mCurrentMeleeSwingRate))
        {
            Shoot();
        }
    }

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

        if (!mLockWeapon)   { mGunOrMelee = (targetDist <= mMeleeData.mRange); }

        Attack(target);
    }

    private void Attack(Collider target)
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

        if (CalculateAccuracy())
        {
            Health targetHealth = target.GetComponent<Health>();
            targetHealth.dealDamage(CalculateDamage(), gameObject);
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
            yield return new WaitForSeconds(mCurrentReloadTime);

            mGunState = GunState.READY_TO_FIRE;
            mCurrentMag = mGunData.mMagSize;
            mCurrentAmmo -= mGunData.mMagSize;
        }
    }

    private bool CanFire()
    {
        return (mGunState == GunState.READY_TO_FIRE) && (!mMovement.IsMoving);
    }

    private bool CalculateAccuracy()
    {
        return (mGunData.mAccuracy == 100) || (Random.Range(0, 100) < mGunData.mAccuracy);
    }

    private float CalculateDamage()
    {
        float newDamage = mGunOrMelee ? mGunData.mDamage : mMeleeData.mDamage;
        float critChange = mGunOrMelee ? mGunData.mCritChance : mMeleeData.mCritChance;
        float critMod = mGunOrMelee ? mGunData.mCritModifier : mMeleeData.mCritModifier;

        if ((critChange == 100) || (Random.Range(0, 100) < critChange))     { newDamage *= critMod; }
        return newDamage;
    }

    public void SetNewReloadTime(float newReloadTime)
    {
        mCurrentReloadTime = newReloadTime;
    }

    public void SetNewGunFireRate(float newGunFireRate)
    {
        mCurrentGunFireRate = newGunFireRate;
    }

    public void SetNewMeleeFireRate(float newMeleeSwingRate)
    {
        mCurrentMeleeSwingRate = newMeleeSwingRate;
    }

    public void LockWeapon(bool isRanged, bool isLocked)
    {
        mGunOrMelee = isRanged;
        mLockWeapon = isLocked;
    }
}
