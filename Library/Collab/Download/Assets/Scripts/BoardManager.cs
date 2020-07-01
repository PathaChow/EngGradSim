
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    private int count1 = 1;
    private int count2 = 1;
    private int count3 = 1;
    public GameObject work;
    public GameObject fun;
    public GameObject sleep;
    public GameObject eventSprite;
    private float timer = 0.0f;
    private float eventTimer = 0.0f;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        eventTimer += Time.deltaTime;

        if (timer > 1.0)
        {
            HashSet<Tuple<int,int>> map = new HashSet<Tuple<int, int>>(); ;
            int x1= Random.Range(9, 10), x2= Random.Range(9, 10), x3 = Random.Range(9, 10), x4 = Random.Range(9, 10);
            int y1 = Random.Range(-3, 3), y2 = Random.Range(-3, 3), y3 = Random.Range(-3, 3), y4 = Random.Range(-3, 3);

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

            if (eventTimer > 5f)
            {
                eventTimer = 0f;
                Tuple<int, int> tuple4 = Tuple.Create(x4, y4);
                while (map.Contains(tuple4))
                {
                    x4 = Random.Range(9, 10);
                    y4 = Random.Range(-3, 3);
                    tuple4 = Tuple.Create(x4, y4);
                }
                map.Add(tuple4);
                Vector3 spawnPos = new Vector3(x4, y4, 0);
                Instantiate(eventSprite, spawnPos, Quaternion.identity);
            }
        }
    }
}
