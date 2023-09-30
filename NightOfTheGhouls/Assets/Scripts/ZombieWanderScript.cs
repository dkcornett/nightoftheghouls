using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieWanderScript : MonoBehaviour
{
    public BiterType biterType;
    public GameObject targetObject;
    Collider targetCol;

    // Start is called before the first frame update
    void Start()
    {
        Transform target;
        target = gameObject.transform.Find("wanderTargetObject");
        targetObject = target.gameObject;
        targetCol = targetObject.GetComponent<Collider>();

     //   biterType = gameObject.transform.parent.gameObject.GetComponent<BiterType>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == targetCol) SetWanderTarget();
    }
    public void SetWanderTarget()
    {
        Collider wanderCol = gameObject.GetComponent<Collider>();
        Vector3 wanderTarget = new Vector3(
            Random.Range(wanderCol.bounds.min.x, wanderCol.bounds.max.x),
            gameObject.transform.position.y,
            Random.Range(wanderCol.bounds.min.z, wanderCol.bounds.max.z)
            );
        targetObject.transform.position = wanderTarget;

      //  biterType.ZombTypeActivate(targetObject);

        //Debug.Log("resetting wander target!");
    }
}
