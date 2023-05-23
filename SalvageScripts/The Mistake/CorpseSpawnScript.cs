using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseSpawnScript : MonoBehaviour
{
    public GameObject gore;
    private IEnumerator stopMunch;
    GameObject zomDir;

    public float sTimer = 4;
    public float range = 5;

    // Start is called before the first frame update
    void Start()
    {
        zomDir = GameObject.FindGameObjectWithTag("ZombieDirector");

        transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));

        stopMunch = reTargTimer(sTimer);
        StartCoroutine(stopMunch);

        Collider[] gibletsAndBits = Physics.OverlapSphere(transform.position, range);
        foreach (Collider buffetCustomer in gibletsAndBits)
        {

            if (buffetCustomer.gameObject.tag == "Zombie")
            {
                Vector3 pos = transform.position;
                pos.y++;
                Quaternion rot = Quaternion.LookRotation(buffetCustomer.transform.position - transform.position);

                Instantiate(gore, pos, rot);
            }
        }
    }

    private IEnumerator reTargTimer(float wait)
    {
        yield return new WaitForSeconds(wait);
        ZombieDirectorScript director = zomDir.GetComponent<ZombieDirectorScript>();
        if (director.units.Count > 0)
        {
            Collider[] snacktime = Physics.OverlapSphere(transform.position, range);
            foreach (Collider zedhead in snacktime)
            {
                float tempRand = Random.Range(0, 10);

                if (zedhead.gameObject.tag == "Zombie" && tempRand < 9)
                {
                    zedhead.gameObject.SendMessage("Activate");
                }
                else if (zedhead.tag == "Zombie")
                {
                    Vector3 pos = transform.position;
                    pos.y++;
                    Quaternion rot = Quaternion.LookRotation(zedhead.transform.position - transform.position);

                    Instantiate(gore, pos, rot);
                }
            }
        }
    }
}
