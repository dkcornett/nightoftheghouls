using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGun : MonoBehaviour
{
    public float range;
    public float damage;
    public Collider target;

    public GameObject flashPref;

    public bool alive = true;

    Vector3 oldPos;
    bool moving = false;

    public int ammoMax;
    public int ammoCur;
    public int magCur;
    public int magMax;
    public int reloadIntervalsCur;
    public int reloadIntervalsReal;
    public int firerateIntervalReal;
    public int firerateIntervalsCur;
    public int status; //0 is doing nothing or ready, 1 is currently firing, 2 is fire cooldown, 3 is reloading, 4 is empty ammo, 5 is total empty, 6 is moving

    private IEnumerator coroutine;
    

    // Start is called before the first frame update
    void Start()
    {
        //LineRenderer lRend = flashObj.GetComponent<LineRenderer>();

        oldPos = transform.position;

        coroutine = IntervalHandler(0.2f);
        StartCoroutine(coroutine);
        status = 0;
        reloadIntervalsCur = reloadIntervalsReal;
        firerateIntervalsCur = firerateIntervalReal;
    }

    private void Update()
    {
        if (transform.position != oldPos)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        

        oldPos = transform.position;


    }

    public void bepis()
    {
        alive = false;
    }

    private IEnumerator IntervalHandler(float waitTime)
    {
        while (alive)
        {
            yield return new WaitForSeconds(waitTime);
            checkTargs();
        }
    }

    // Update is called once per frame
    public void checkTargs()
    {
        float closeDist = range;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (var hitCollider in hitColliders)
        {
            RaycastHit hit;
            if (hitCollider.gameObject.tag == "Zombie")
            {

                if (Physics.Raycast(transform.position, hitCollider.transform.position - transform.position, out hit, range))
                {
                    if (hit.collider == hitCollider && Vector3.Distance(transform.position, hitCollider.transform.position) < closeDist)
                    {
                        target = hitCollider;
                        closeDist = Vector3.Distance(transform.position, hitCollider.transform.position);
                    }
                }
            }
        }

        fireGun();
    }

    public void fireGun()
    {
        if (status == 5)
        {
            return;
        }
        else if (moving)
        {
            return;
        }
        else if (magCur < 1)
        {
            reload();
            return;
        }
        else if (status == 2)
        {
            firerateIntervalsCur--;
            if (firerateIntervalsCur <= 0)
            {
                status = 0;
                firerateIntervalsCur = firerateIntervalReal;
            }
        }
        else
        {
            status = 1;
            fireActual();
        }
    }

    public void reload()
    {
        if (status == 4)
        {
            firerateIntervalsCur = firerateIntervalReal;
            reloadIntervalsCur = reloadIntervalsReal;
            status = 5;
            return;
        }

        status = 3;
        if (reloadIntervalsCur <= 0)
        {
            if (magMax < ammoCur)
            {
                magCur = magMax;
                ammoCur -= magMax;
                status = 0;
                reloadIntervalsCur = reloadIntervalsReal;
                return;
            }
            else
            {
                status = 4;
                magCur = ammoCur;
                ammoCur = 0;
                reloadIntervalsCur = reloadIntervalsReal;
                return;
            }

        }
        else 
        {
            reloadIntervalsCur --;
            status = 0;
        }
    }

    public void fireActual()
    {
        
        if (target != null)
        {
            Health targHP = target.GetComponent<Health>();

            transform.LookAt(target.transform);

            Vector3 pos = transform.position;
            Quaternion rot = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);

            Instantiate(flashPref, pos, rot);

            targHP.dealDamage(damage, gameObject);
            //do the roar
            status = 2;
            magCur--;

            target = null;
        }
    }
}
