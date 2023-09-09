using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    public AbilityData mAbilityData;
    public KeyCode mActivateKey = KeyCode.X;
    
    private AbilityState mState = AbilityState.READY_TO_USE;
    private BasicMouseMovement mMover;

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
        if (mAbilityData.mAbilityType == AbilityData.AbilityType.ACTIVE
            && Input.GetKeyDown(mActivateKey)
            && mMover.IsSelected
            && mState == AbilityState.READY_TO_USE)
        {
            StartCoroutine(Active());
        }
        else if (mAbilityData.mAbilityType == AbilityData.AbilityType.PASSIVE)
        {
            mAbilityData.ActivateAbility(gameObject);
        }
        else if (mAbilityData.mAbilityType == AbilityData.AbilityType.INVALID_TYPE)
        {
            Debug.LogError("ERROR: Ability Type Unknown, please check ability");
        }
    }

    private IEnumerator Active()
    {
        if (mAbilityData.ActivateAbility(gameObject))
        {
            mState = AbilityState.ACTIVE;
            yield return new WaitForSeconds(mAbilityData.mActiveTime);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        mState = AbilityState.COOLDOWN;
        yield return new WaitForSeconds(mAbilityData.mCooldown);
        mAbilityData.DeactivateAbility(gameObject);
        mState = AbilityState.READY_TO_USE;
    }
}
