using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicMouseMovement : MonoBehaviour
{
    //public GameObject selCirc;
    public bool selected = false;
    public bool mIsMoving = false;
    NavMeshAgent agent;

    Vector3 move;
    //Vector3[] moveLine = new Vector3[2];

    //LineRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        //selCirc.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        //rend = GetComponent<LineRenderer>();
        //moveLine[0] = transform.position;
        move = transform.position;
        //rend.SetPosition(0, transform.position);
        //rend.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        mIsMoving = agent.velocity != Vector3.zero;

        if (selected && Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                move.x = hit.point.x;
                move.z = hit.point.z;
                move.y = transform.position.y;
            }

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                selected = false;
                //selCirc.SetActive(false);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            selected = false;
            //selCirc.SetActive(false);
        }

        //if (transform.position != move)
        //{
        //    //moveLine[0] = transform.position;
        //    //moveLine[1] = move;
        //    //rend.SetPositions(moveLine);
        //}
    }

    private void OnMouseDown()
    {
        //selCirc.SetActive(true);
        selected = true;
    }
}
