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
    public Color debugColor = new Color(1.0f, 0.0f, 0.0f); //color for debug.draw line
    Vector3 upVector = new Vector3(0f, 0.5f, 0f); //vector for lifting debug.draw line off the ground
    GameObject currentTarg;
    public bool hasWander = false; // determines whether zombie has a wander range
    public ZombieWanderScript wanderRange;

    Health targetH;
    
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        uiScript = gameObject.GetComponent<UnitUIScript>();

       // if (gameObject.GetComponentInChildren < "wanderRangeObject" >  != null)
        if(wanderRange != null)
        {
            Debug.Log(gameObject.name + " has a wander range!");
            hasWander = true;
        }
    }

    // Update is called once per frame, checks to see if the zombie is active, if active targets player unit, if the player unit is near, attack them
    void LateUpdate()
    {
        if (active && currentTarg != null)
        {
            agent.SetDestination(currentTarg.transform.position);


            Debug.DrawLine(transform.position + upVector, agent.destination + upVector, debugColor);

            if (Vector3.Distance(transform.position, currentTarg.transform.position) <= biteRange && !biteCD && currentTarg.tag == "Player")
            {
                biteCD = true;
                targetH.dealDamage(biteDmg, gameObject);
                StartCoroutine(biteDelay());
            }

            if (hasWander && Vector3.Distance(transform.position, currentTarg.transform.position) <= biteRange && !biteCD && currentTarg.tag == "WanderTarget")
            {
                wanderRange.SetWanderTarget();
            }
 

        }
        else if (!active && currentTarg == null && hasWander)
        {
            wanderRange.SetWanderTarget();
            currentTarg = wanderRange.targetObject;

            active = true;
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

        if (target.GetComponent<Health>())
        { targetH = target.GetComponent<Health>(); }

        active = true;
    
    }
    /*
    public void setWanderTarget()
    {
        Collider wanderCol = wanderRange.GetComponent<Collider>();
        Vector3 wanderTarget = new Vector3(
            Random.Range(wanderCol.bounds.min.x, wanderCol.bounds.max.x),
            0f,
            Random.Range(wanderCol.bounds.min.y, wanderCol.bounds.max.y)
            );
        wanderRange.transform.position = wanderTarget;

    }
    */
}
