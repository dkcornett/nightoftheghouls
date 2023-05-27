using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoWorth = 0;

 
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with " + other.gameObject);

        if (other.gameObject.tag == "PlayerUnit")
        {
            BasicGun gun = other.gameObject.GetComponent<BasicGun>();
            if (gun.ammoCur < gun.ammoMax)
            {
                gun.ammoCur += ammoWorth;
                if (gun.ammoCur > gun.ammoMax)
                {
                    gun.ammoCur = gun.ammoMax;
                    
                }

                gun.status = 1;
                Destroy(gameObject);
            }
        }
            
    }
}
