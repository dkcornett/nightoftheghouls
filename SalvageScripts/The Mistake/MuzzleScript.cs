using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleScript : MonoBehaviour
{

    private IEnumerator selfDest;

    public float timeKill = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        selfDest = killMe(timeKill);
        StartCoroutine(selfDest);
    }

    private IEnumerator killMe(float wait)
    {
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }
}
