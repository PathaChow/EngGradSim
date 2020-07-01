
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public GameObject work;
    public GameObject fun;
    public GameObject sleep;
    public GameObject eventSprite;
    private GameObject myGM;
    private float timer1 = 0.0f;
    private float timer2 = 0.0f;
    private float timer3 = 0.0f;
    private float timer4 = 0.0f;
    private float eventTimer = 0.0f;
    private float speed = 0.0f;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        myGM = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        speed = myGM.GetComponent<SpeedManager>().getSpeed();
        SpawnT1();
        SpawnT2();
        SpawnT3();
        SpawnBar();
    }
    private void SpawnT1()
    {
        timer1 += Time.deltaTime;
        if (timer1 * speed > Random.Range(1.5f, 2.0f))
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
        if (timer2 * speed > Random.Range(1.5f, 2.0f))
        {
            timer2 = 0.0f;
            float x1 = Random.Range(9.0f, 10.0f);
            float y1 = Random.Range(-1.0f, 1.0f);
            Vector3 spawnPos = new Vector3(x1, y1, 0);
            if (myGM.GetComponent<ScoreManager>().GetRange() > 5 && Random.Range(0.0f, 1.0f)>0.7f)
            {
                Instantiate(eventSprite, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(fun, spawnPos, Quaternion.identity);
            }
            
        }
    }
    private void SpawnT3()
    {
        timer3 += Time.deltaTime;
        if (timer3 * speed > Random.Range(1.5f, 2.0f))
        {
            timer3 = 0.0f;
            float x1 = Random.Range(9.0f, 10.0f);
            float y1 = Random.Range(1.0f, 3.0f);
            Vector3 spawnPos = new Vector3(x1, y1, 0);
            Instantiate(sleep, spawnPos, Quaternion.identity);
        }
    }
    private void SpawnBar()
    {
        
        timer4 += Time.deltaTime;
        if (timer4 * speed > Random.Range(1.5f, 2.0f))
        {
            timer4 = 0.0f;
            int side = Random.Range(0, 4);
            Vector2[] vertices;
            Vector2 ptr1 = new Vector2(0.0f,Random.Range(0.0f,2.0f));
            Vector2 ptr2 = new Vector2(Random.Range(0.0f, 2.0f), 0.0f);
            vertices = new Vector2[] { new Vector2(ptr1.x, ptr1.y), new Vector2(ptr1.x+0.2f, ptr1.y), new Vector2(0,0),
                new Vector2(0.2f, 0.2f), new Vector2(ptr2.x, ptr2.y), new Vector2(ptr2.x, ptr2.y+0.2f)};
            ushort[] triangles = new ushort[] { 0, 1, 2, 1, 2, 3, 2, 3, 5, 2, 4, 5 };
            GameObject Bar = new GameObject();
            SpriteRenderer sr = Bar.AddComponent<SpriteRenderer>();
            Bar.AddComponent<SideMove>();
            PolygonCollider2D barCol = Bar.AddComponent<PolygonCollider2D>();
            barCol.points = vertices;
            Texture2D texture = new Texture2D(5, 5);
            sr.color = Color.blue;
            sr.sprite = Sprite.Create(texture, new Rect(0, 0, 5, 5), Vector2.zero, 1);
            sr.sprite.OverrideGeometry(vertices, triangles);
            float y1 = Random.Range(-3.0f, 3.0f);
            float x1 = Random.Range(9.0f, 10.0f);
            Vector3 spawnPos = new Vector3(10.0f, y1, 0);
            Quaternion q = Quaternion.AngleAxis(Random.Range(0, 4) * 90.0f, Vector3.forward);
            Instantiate(Bar, spawnPos, q);
            Destroy(Bar);
        }
    }
}

public class Barrier
{
    public float Ltop;
    public float Lright;
    public int direction;
}
