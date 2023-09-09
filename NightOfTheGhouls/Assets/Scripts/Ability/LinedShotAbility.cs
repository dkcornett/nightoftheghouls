using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability/Lined Attack")]
public class LinedShotAbility : AbilityData
{
    private float mShotMaxDist = 100;
    public LineRenderer mLR;


    public override bool ActivateAbility(GameObject user)
    {
        if (Physics.Raycast(user.transform.position, user.transform.forward, out RaycastHit hit))
        {
            
        }   
        
        return false;
    }
}
