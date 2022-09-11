using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject monsterPrefeb;

    public float interval = 3;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnNext", interval, interval);
    }

    void SpawnNext()
    {
        //Instantiate(monsterPrefeb, transform.position, Quaternion.identity);
    }
}
