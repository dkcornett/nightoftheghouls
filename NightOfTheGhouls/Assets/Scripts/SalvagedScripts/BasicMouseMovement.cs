using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicMouseMovement : MonoBehaviour
{
    private bool mSelected = false;
    private bool mIsMoving = false;
    private NavMeshAgent agent;
    private Vector3 move;

    public bool IsMoving => mIsMoving;
    public bool IsSelected => mSelected;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        move = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mIsMoving = agent.velocity != Vector3.zero;

        if (mSelected && Input.GetMouseButtonDown(1))
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
                mSelected = false;
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            mSelected = false;
        }
    }

    private void OnMouseDown()
    {
        mSelected = true;
    }
}
