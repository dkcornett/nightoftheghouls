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
        unitHP = unitClamp.GetComponent<Health>();
        unitGun = unitClamp.GetComponent<BasicGun>();

        maxHP = unitHP.maxHealth;
        hBar.maxValue = maxHP;
        aBar.maxValue = unitGun.ammoMax + unitGun.magCur;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = unitClamp.transform.position;
        transform.rotation = Quaternion.Euler(0, camObj.transform.eulerAngles.y, 0);

        hBar.value = unitHP.health;
        aBar.value = unitGun.ammoCur + unitGun.magCur;
        
    }

    public void VibeCheck(GameObject check)
    {
        if (check == unitClamp)
        {
            Destroy(gameObject);
        }
    }

}
