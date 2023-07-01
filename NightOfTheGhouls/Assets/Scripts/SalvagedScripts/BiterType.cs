using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BiterType : MonoBehaviour
{
    public float biteRange = 2;
    public float biteDmg = 1;
    public float biteCoolD = 3f;
    bool active = false;
    bool biteCD = false;

    public UnitUIScript uiScript;
    public bool showHealth = false;
    NavMeshAgent agent;

    GameObject currentTarg;

    Health targetH;
    
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        uiScript = gameObject.GetComponent<UnitUIScript>();
    }

    // Update is called once per frame, checks to see if the zombie is active, if active targets player unit, if the player unit is near, attack them
    void LateUpdate()
    {
        if (active && currentTarg != null)
        {
            agent.SetDestination(currentTarg.transform.position);

            if (Vector3.Distance(transform.position, currentTarg.transform.position) <= biteRange && !biteCD)
            {


                biteCD = true;
                targetH.dealDamage(biteDmg, gameObject);
                StartCoroutine(biteDelay());
            }
        }
    }

    //waits seconds equal to the bite delay before resetting cooldown
    IEnumerator biteDelay()
    { 
        yield return new WaitForSeconds(biteCoolD);
        biteCD = false;
    }

    //activates the zombie on external call
    public void ZombTypeActivate(GameObject target)
    {
        currentTarg = target;

        targetH = target.GetComponent<Health>();

        active = true;
    
    }
}
