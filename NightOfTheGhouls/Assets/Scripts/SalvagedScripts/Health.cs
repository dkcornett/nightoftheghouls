using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //bool showHealthBar = false;
    public float maxHealth = 1;
    public float health;
    ZombieDirectorScript director;

    //public GameObject corpseObj;

    //public GameObject bloodFX;

    bool zombie = false;
    bool human = false;

    // Start is called before the first frame update, gets director info, sets own info for unit type
    void Start()
    {
        health = maxHealth;
        director = GameObject.FindGameObjectWithTag("ZombieDirector").GetComponent<ZombieDirectorScript>();

        if (gameObject.tag == "Zombie")
        {
            zombie = true;
        }
        else if (gameObject.tag == "PlayerUnit")
        {
            human = true;
        }
        
    }

    //damages self, alerts proper scripts if dead
    public void dealDamage(float chunk, GameObject hurty)
    {
        health -= chunk;

        Quaternion rot = Quaternion.LookRotation(transform.position - hurty.transform.position, Vector3.up);
        //Instantiate(bloodFX, transform.position, rot);

        if (health <= 0)
        {
            if (zombie)
            {
                director.removeDead(gameObject);
            }
            else if (human)
            {
                director.removeHuman(gameObject);
                gameObject.SendMessage("bepis");

                Vector3 sPos = transform.position;
                sPos.y -= 1.7f;

                Quaternion sRot = Quaternion.identity;
                //sRot.y = Random.Range(0, 360f);
                //Debug.Log(sRot);
                //Instantiate(corpseObj, sPos, sRot);

                /*
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("UIObj"))
                {
                    go.SendMessage("VibeCheck", gameObject);
                }
                */

                Destroy(gameObject);

            }
            else 
            {
                //placeholder for creeps, npcs, and barricades
                Debug.Log("You forgot the tag you goofus");
            }
        }
    }

    
}
