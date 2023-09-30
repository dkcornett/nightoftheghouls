using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Lined Attack")]
public class LinedShotAbility : AbilityData
{
    private float mShotMaxDist = 25;
    private float mDamage = 10;
    //public LineRenderer mLR;

    private Gun mUserGun;

    public override bool ActivateAbility(GameObject user)
    {
        if (mUserGun == null)   { mUserGun = user.GetComponent<Gun>(); }

        // Look in mouse direction
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookAtPos = new Vector3(hit.point.x, user.transform.position.y, hit.point.z);
            user.transform.LookAt(lookAtPos);
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return false;
        }

        // Fire ability
        RaycastHit[] lineAttackHits = Physics.RaycastAll(user.transform.position, user.transform.forward, mShotMaxDist);
        foreach(RaycastHit abilityHit in lineAttackHits)
        {
            if (abilityHit.collider.CompareTag("Zombie"))
            {
                abilityHit.collider.GetComponent<Health>().dealDamage(mDamage, user);
            }
        }

        return true;
    }
}
