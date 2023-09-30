using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    public AbilityData mAbilityData;
    public KeyCode mActivateKey = KeyCode.X;
    
    private AbilityState mState = AbilityState.READY_TO_USE;
    private BasicMouseMovement mMover;

    private bool mIsAiming = false;

    enum AbilityState
    {
        READY_TO_USE,
        ACTIVE,
        COOLDOWN
    }

    void Awake()
    {
        mMover = GetComponent<BasicMouseMovement>();
    }


    // Update is called once per frame
    void Update()
    {
        switch (mAbilityData.mAbilityType)
        {
            case AbilityData.AbilityType.ACTIVE:
                UseActiveAbility();
                break;

            case AbilityData.AbilityType.PASSIVE:
                mAbilityData.ActivateAbility(gameObject);
                break;

            case AbilityData.AbilityType.AIMED:
                UseAimedAbility();
                break;

            default:
                Debug.LogError("ERROR: Ability Type Unknown, please check ability");
                break;
        }
    }

    private IEnumerator Active()
    {
        mState = AbilityState.ACTIVE;
        yield return new WaitForSeconds(mAbilityData.mActiveTime);
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        mState = AbilityState.COOLDOWN;
        yield return new WaitForSeconds(mAbilityData.mCooldown);
        mAbilityData.DeactivateAbility(gameObject);
        mState = AbilityState.READY_TO_USE;
    }

    private void UseActiveAbility()
    {
        if (Input.GetKeyDown(mActivateKey) && mMover.IsSelected && mState == AbilityState.READY_TO_USE)
        {
            mAbilityData.ActivateAbility(gameObject);
            StartCoroutine(Active());
        }
    }

    private void UseAimedAbility()
    {
        if (Input.GetKeyDown(mActivateKey) && mMover.IsSelected && mState == AbilityState.READY_TO_USE && !mIsAiming)      { mIsAiming = true; }
        else if (!mIsAiming) { return; }

        if (mAbilityData.ActivateAbility(gameObject))
        {
            mIsAiming = false;
            StartCoroutine(Active());
        }
    }
}
