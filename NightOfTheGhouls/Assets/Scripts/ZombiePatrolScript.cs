using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePatrolScript : MonoBehaviour
{
    public BiterType biterType;
    public GameObject targetObject;
    Collider targetCol;
    public Transform[] patrolPoints;
    [SerializeField] private int patrolIndex = 1;
    // Start is called before the first frame update
    void Start()
    {
        //MAKE SURE YOUR FIRST PATROL POINT OBJECT IS WHERE YOU WANT THIS ZOMBIE TO START THE GAME
        gameObject.transform.position = patrolPoints[0].transform.position;
     
        //setup patrol target, which moves every time it is reached
        Transform target = gameObject.transform.Find("patrolTargetObject");
        targetObject = target.gameObject;
        targetCol = target.GetComponent<Collider>();
        targetObject.transform.position = patrolPoints[1].transform.position;
    
        patrolIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("patrol point reached!");
        if (other== targetCol) NextPatrolPoint();
        Debug.Log("patrol index reset");
    }

    public void NextPatrolPoint() 
    {
        Debug.Log("resetting patrol point");
        /*//go to next patrol point in list
        if (patrolIndex < patrolPoints.Length - 1)
        {
            patrolIndex++;
        }
        //if at end of list, go back to patrol point at beginning of list
        else if (patrolIndex >= patrolPoints.Length - 1)
        {
            patrolIndex = 0;
        }*/
        patrolIndex++;

        if (patrolIndex >= patrolPoints.Length)
        {
            patrolIndex = 0;
        }

        //now make the target object the new patrol point
        targetObject.transform.position = patrolPoints[patrolIndex].position;
        Debug.Log("setting patrol point to point " + patrolIndex);
    }
}
