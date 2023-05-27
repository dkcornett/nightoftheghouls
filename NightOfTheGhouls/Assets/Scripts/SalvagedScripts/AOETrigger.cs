using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETrigger : MonoBehaviour
{
    public int podTrig;
    GameObject director;

    // Start is called before the first frame update
    void Start()
    {
        director = GameObject.FindGameObjectWithTag("ZombieDirector");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerUnit")
        {
            director.SendMessage("ActivateTrigger", podTrig);
            Destroy(gameObject);
        }
    }
}
