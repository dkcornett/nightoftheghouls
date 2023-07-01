using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDirectorScript : MonoBehaviour
{
    public List<Vector3> podPos = new List<Vector3>();
    public List<GameObject> zombies = new List<GameObject>();
    int podCount = 0;

    public List<GameObject> units = new List<GameObject>();

    public float detRange = 1;

    private IEnumerator directZom;

    // Start is called before the first frame update
    void Start()
    {
        //finds all player units
        foreach (GameObject found in GameObject.FindGameObjectsWithTag("PlayerUnit"))
        {
            units.Add(found);
        }

        //finds all zombies and counts their pods
        foreach (GameObject find in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            ZombieActorScript temp;
            temp = find.GetComponent<ZombieActorScript>();
            zombies.Add(find);
            if (temp.podGroup > podCount)
            {
                podCount = temp.podGroup;
            }
        }

        //creates average positions of all pods
        for (int i = 1; i <= podCount; i ++)
        {
            Vector3 avgPos = Vector3.zero;
            int numGroup = 0;
            

            foreach (GameObject podObj in zombies)
            {
                ZombieActorScript tempTwo;
                tempTwo = podObj.GetComponent<ZombieActorScript>();
                if (tempTwo.podGroup == i)
                {
                    numGroup++;
                    avgPos += podObj.transform.position;
                }
            }

            avgPos /= numGroup;

            podPos.Add(avgPos);
        }

        //directZom = DirectorHandler(1f);

        //StartCoroutine(directZom);
    }

    //checks player units to activate pods
    private IEnumerator DirectorHandler(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            foreach (GameObject check in units)
            {
                int tempos = 0;

                foreach (Vector3 pos in podPos)
                {
                    

                    if (Vector3.Distance(check.transform.position, pos) <= detRange)
                    {
                        foreach (GameObject quad in zombies)
                        {

                            ZombieActorScript tempis = quad.GetComponent<ZombieActorScript>();
                            if ((Vector3.Distance(check.transform.position, pos) <= tempis.detRange))
                             { if (tempis.podGroup - 1 == tempos)
                              {
                                 tempis.Activate();
                              }
                             }
                        
                        }
                    }

                    tempos++;
                }
            }
        }
    }


    //safely removes zombies on second death
    public void removeDead(GameObject unUndead)
    {
        zombies.Remove(unUndead);
        Destroy(unUndead);
    }

    //safely removes humans on first death
    public void removeHuman(GameObject dead)
    {
        units.Remove(dead);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject walker in zombies)
            {
                walker.SendMessage("Activate"); //for test purposes only
            }
        }
    }

    public void ActivateTrigger(int podTrig)
    {
        foreach (GameObject ghoul in zombies)
        {
            ZombieActorScript romero = ghoul.GetComponent<ZombieActorScript>();
            if (romero.podGroup == podTrig)
            {
                romero.SendMessage("Activate");
            }
        }
    
    }
}
