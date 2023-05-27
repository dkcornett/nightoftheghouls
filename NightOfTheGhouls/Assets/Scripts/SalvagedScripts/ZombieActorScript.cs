using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieActorScript : MonoBehaviour
{
    public int podGroup = 1;
    public int activateType = 0; //0 is test, 1 is triggered, add more as I create them
    //public int zombType = 1;



    GameObject curTarg;

    //activates the zombie's unique ai type, holds zombie podgroup for ai controller
    public void Activate()
    {
        GameObject[] targetOptions;
        targetOptions = GameObject.FindGameObjectsWithTag("PlayerUnit");
        int dice = Random.Range(0, targetOptions.Length);
        curTarg = targetOptions[dice];
        gameObject.SendMessage("ZombTypeActivate", curTarg);

    }
}
