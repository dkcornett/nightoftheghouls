using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamMove : MonoBehaviour
{
    public Camera cam;
    public float sizeAct;

    public float speed;
    public float rotS;
    public float zoomMax;
    public float zoomMin;
    public float zoomSpeed;

    Quaternion goal;

    bool rotCD = false;

    IEnumerator rotDelay()
    {
        yield return new WaitForSeconds(0.65f);
        rotCD = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        goal = transform.rotation;

        sizeAct = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, speed * Time.deltaTime * -1f, Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(speed * Time.deltaTime * -1f, 0, 0, Space.Self);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !rotCD)
        {
            rotCD = true;
            StartCoroutine(rotDelay());
            goal = Quaternion.AngleAxis(45, transform.up) * transform.rotation;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !rotCD)
        {
            rotCD = true;
            StartCoroutine(rotDelay());
            goal = Quaternion.AngleAxis(-45, transform.up) * transform.rotation;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, goal, Time.deltaTime * rotS);

        if (Input.mouseScrollDelta.y > 0 && sizeAct > zoomMin)
        {
            
            cam.orthographicSize -= zoomSpeed * Time.deltaTime;
            sizeAct = cam.orthographicSize;
        }
        else if (Input.mouseScrollDelta.y < 0 && sizeAct < zoomMax)
        {
            cam.orthographicSize += zoomSpeed * Time.deltaTime;
            sizeAct = cam.orthographicSize;
        }
    }
}
