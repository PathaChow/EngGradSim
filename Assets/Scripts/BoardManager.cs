
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public int count1 = 5;
    public int count2 = 5;
    public int count3 = 5;
    public GameObject work;
    public GameObject fun;
    public GameObject sleep;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<count1; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-2,2), Random.Range(-2, 2),0);
            Instantiate(work,spawnPos,Quaternion.identity);
        }
        for (int i = 0; i < count2; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
            Instantiate(fun, spawnPos, Quaternion.identity);
        }
        for (int i = 0; i < count3; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
            Instantiate(sleep, spawnPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
