using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUIScript : MonoBehaviour
{
    public GameObject unitClamp;
    public Slider hBar;
    public Slider aBar;
    public GameObject camObj;
    //public Sprite hBarBack;
    //public GameObject ammoPip;

    

    Health unitHP;
    BasicGun unitGun;

    float maxHP;

    // Start is called before the first frame update
    void Start()
    {
        camObj = Camera.main.gameObject;

        unitHP = unitClamp.GetComponent<Health>();
        maxHP = unitHP.maxHealth;
        //     unitGun = unitClamp.GetComponent<BasicGun>();

        if (unitClamp.CompareTag("Zombie"))
        {
            if (hBar) { hBar.maxValue = maxHP; }
            {
                if (unitClamp.GetComponent<BiterType>().showHealth == true) { hBar.gameObject.SetActive(true); }
                if (unitClamp.GetComponent<BiterType>().showHealth == false) { hBar.gameObject.SetActive(false); }
            }
            if (aBar) { aBar.gameObject.SetActive(false); }
        }
        if (unitClamp.CompareTag("PlayerUnit"))
        {
            if (aBar) { aBar.maxValue = unitGun.ammoMax + unitGun.magCur; }
            unitGun = unitClamp.GetComponent<BasicGun>();
        }
 
        if (unitClamp.GetComponent<BasicGun>() == true)
        {
            unitGun = unitClamp.GetComponent<BasicGun>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = unitClamp.transform.position;
        transform.rotation = Quaternion.Euler(0, camObj.transform.eulerAngles.y, 0);

        if (hBar) { hBar.value = unitHP.health; }
        if (aBar) { aBar.value = unitGun.ammoCur + unitGun.magCur; }
        
    }

    public void VibeCheck(GameObject check)
    {
        if (check == unitClamp)
        {
            Destroy(gameObject);
        }
    }

}
