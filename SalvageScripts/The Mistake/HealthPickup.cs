using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthWorth = 0;



    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with " + other.gameObject);

        if (other.gameObject.tag == "PlayerUnit")
        {
            Health con = other.gameObject.GetComponent<Health>();
            if (con.health < con.maxHealth)
            {
                con.health += healthWorth;
                if (con.health > con.maxHealth)
                {
                    con.health = con.maxHealth;

                }
                Destroy(gameObject);
            }
        }

    }
}
