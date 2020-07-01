
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    
    public GameObject work;
    public GameObject fun;
    public GameObject sleep;
    private float timer1 = 0.0f;
    private float timer2 = 0.0f;
    private float timer3 = 0.0f;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        /*timer += Time.deltaTime;
        if (timer > 1.0)
        {
            HashSet<Tuple<int,int>> map = new HashSet<Tuple<int, int>>(); ;
            int x1= Random.Range(9, 10), x2= Random.Range(9, 10), x3 = Random.Range(9, 10);
            int y1 = Random.Range(-3, 3), y2 = Random.Range(-3, 3), y3 = Random.Range(-3, 3);

            timer = 0.0f;
            for (int i = 0; i < count1; i++)
            {
                Tuple<int, int> tuple1 = Tuple.Create(x1,y1);
                while (map.Contains(tuple1))
               {
                  x1 = Random.Range(9, 10);
                  y1 = Random.Range(-3, 3);
                  tuple1 = Tuple.Create(x1, y1);
                }
                map.Add(tuple1);
                Vector3 spawnPos = new Vector3(x1, y1, 0);
                Instantiate(work, spawnPos, Quaternion.identity);
            }
            for (int i = 0; i < count2; i++)
            {
                Tuple<int, int> tuple2 = Tuple.Create(x2, y2);
                while (map.Contains(tuple2))
                {
                    x2 = Random.Range(9, 10);
                    y2 = Random.Range(-3, 3);
                    tuple2 = Tuple.Create(x2, y2);
                }
                map.Add(tuple2);
                Vector3 spawnPos = new Vector3(x2, y2, 0);
                Instantiate(fun, spawnPos, Quaternion.identity);
            }
            for (int i = 0; i < count3; i++)
            {
                Tuple<int, int> tuple3 = Tuple.Create(x3, y3);
                while (map.Contains(tuple3))
                {
                    x3 = Random.Range(9, 10);
                    y3 = Random.Range(-3, 3);
                    tuple3 = Tuple.Create(x3, y3);
                }
                map.Add(tuple3);
                Vector3 spawnPos = new Vector3(x3, y3, 0);
                Instantiate(sleep, spawnPos, Quaternion.identity);
            }

        }*/
        SpawnT1();
        SpawnT2();
        SpawnT3();
    }
    private void SpawnT1()
    {
        timer1 += Time.deltaTime;
        if(timer1> Random.Range(1.5f, 2.0f))
        {
            timer1 = 0.0f;
            float x1 = Random.Range(9.0f, 10.0f);
            float y1 = Random.Range(-3.0f, -1.0f);
            Vector3 spawnPos = new Vector3(x1, y1, 0);
            Instantiate(work, spawnPos, Quaternion.identity);
        }
    }
    private void SpawnT2()
    {
        timer2 += Time.deltaTime;
        if (timer2 > Random.Range(1.5f, 2.0f))
        {
            timer2 = 0.0f;
            float x1 = Random.Range(9.0f, 10.0f);
            float y1 = Random.Range(-1.0f, 1.0f);
            Vector3 spawnPos = new Vector3(x1, y1, 0);
            Instantiate(fun, spawnPos, Quaternion.identity);
        }
    }
    private void SpawnT3()
    {
        timer3 += Time.deltaTime;
        if (timer3 > Random.Range(1.5f, 2.0f))
        {
            timer3 = 0.0f;
            float x1 = Random.Range(9.0f, 10.0f);
            float y1 = Random.Range(1.0f, 3.0f);
            Vector3 spawnPos = new Vector3(x1, y1, 0);
            Instantiate(sleep, spawnPos, Quaternion.identity);
        }
    }
}
