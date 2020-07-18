
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
    public GameObject mentalhealth;
    public GameObject eventSprite;
    public LayerMask layer;
    private GameObject myGM;
    private float timer1 = 0.0f;
    private float timer2 = 0.0f;
    private float timer3 = 0.0f;
    private float timer4 = 0.0f;
    private float speed = 0.0f;
    private Camera cam;

    private float workStartTime = 1.5f;
    private float workEndTime = 2.0f;
    private float funStartTime = 1.5f;
    private float funEndTime = 2.0f;
    private float healthStartTime = 1.5f;
    private float healthEndTime = 2.0f;

    private bool isMidtermSeason;
    private float midtermTimer;
    private float midtermFrequency = 20f;
    private float midtermDuration = 10f;

    void OnEnable()
    {
        //EventManager.StartListening("SetDensity", SetDensity);
    }

    void OnDisable()
    {
        //EventManager.StopListening("SetDensity", SetDensity);
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        myGM = GameObject.Find("GameManager");
        //layer = LayerMask.GetMask("Blocking");

        isMidtermSeason = false;
        /*workStartTime = 1.5f;
        workEndTime = 2.0f;
        funStartTime = 1.5f;
        funEndTime = 2.0f;
        healthStartTime = 1.5f;
        healthEndTime = 2.0f;
        midtermTimer = 0f;*/
    }

    // Update is called once per frame
    void Update()
    {
        speed = myGM.GetComponent<SpeedManager>().getSpeed();
        SpawnT1();
        SpawnT2();
        SpawnT3();
        //SpawnBar();

        midtermTimer += Time.deltaTime;
        if (!isMidtermSeason)
        {
            if (midtermTimer >= midtermFrequency)
            {
                midtermTimer = 0f;
                CheckMidtermSeason();
            }
        }
        else
        {
            if (midtermTimer >= midtermDuration)
            {
                midtermTimer = 0f;
                CheckMidtermSeason();
            }
        }
    }
    private void SpawnT1() // spawn work sprite
    {
        speed = myGM.GetComponent<SpeedManager>().getSpeed();
        timer1 += Time.deltaTime;
        if (timer1 * speed > Random.Range(workStartTime, workEndTime))
        {
            timer1 = 0.0f;
            float x1 = Random.Range(9.0f, 10.0f);
            float y1 = Random.Range(-1.0f, 1.0f);
            Vector3 spawnPos = new Vector3(x1, y1, 0);

            // overlap check
            int count = 0;
            Collider2D overlap = Physics2D.OverlapCircle(spawnPos, .2f);

            // while overlap try a new position
            while (overlap != null && count < 20)
            {
                x1 = Random.Range(9.0f, 10.0f);
                y1 = Random.Range(-1.0f, 1.0f);
                spawnPos = new Vector3(x1, y1, 0);
                overlap = Physics2D.OverlapCircle(spawnPos, .2f);
                count++;
            }
            // will not spawn if check failed 20 times
            if (overlap == null)
            Instantiate(work, spawnPos, Quaternion.identity);
        }
    }
    private void SpawnT2() // spawn event (green) sprite - for pause events
    {
        speed = myGM.GetComponent<SpeedManager>().getSpeed();
        timer2 += Time.deltaTime;
        if (timer2 * speed > Random.Range(funStartTime, funEndTime))
        {
            timer2 = 0.0f;
            float x1 = Random.Range(9.0f, 10.0f);
            float y1 = Random.Range(-3.0f, -1.0f);
            Vector3 spawnPos = new Vector3(x1, y1, 0);

            // overlap check
            int count = 0;
            Collider2D overlap = Physics2D.OverlapCircle(spawnPos, .2f);

            // while overlap try a new position
            while (overlap != null && count < 20)
            {
                x1 = Random.Range(9.0f, 10.0f);
                y1 = Random.Range(-3.0f, -1.0f);
                spawnPos = new Vector3(x1, y1, 0);
                overlap = Physics2D.OverlapCircle(spawnPos, .2f);
                count++;
            }
            // will not spawn if check failed 20 times
            if (overlap == null)
            {
                if (myGM.GetComponent<ScoreManager>().GetRange() > 5 && Random.Range(0.0f, 1.0f) > 0.7f)
                {
                Instantiate(eventSprite, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(fun, spawnPos, Quaternion.identity);
            }
        }
    }
    }
    private void SpawnT3() // spawn sleep and mentalhealth sprite
    {
        speed = myGM.GetComponent<SpeedManager>().getSpeed();
        timer3 += Time.deltaTime;
        if (timer3 * speed > Random.Range(healthStartTime, healthEndTime))
        {
            timer3 = 0.0f;
            float x1 = Random.Range(9.0f, 10.0f);
            float y1 = Random.Range(1.0f, 3.0f);
            Vector3 spawnPos = new Vector3(x1, y1, 0);

            // overlap check
            int count = 0;
            Collider2D overlap = Physics2D.OverlapCircle(spawnPos, .2f);

            // while overlap try a new position
            while (overlap != null && count < 20)
            {
                x1 = Random.Range(9.0f, 10.0f);
                y1 = Random.Range(1.0f, 3.0f);
                spawnPos = new Vector3(x1, y1, 0);
                overlap = Physics2D.OverlapCircle(spawnPos, .2f);
                count++;
            }
            // will not spawn if check failed 20 times
            if (overlap == null)
            {
            if (Random.Range(0.0f, 1.0f) > 0.7f)
            {
                Instantiate(mentalhealth, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(sleep, spawnPos, Quaternion.identity);
            }
        }
    }
    }
	
    private void SpawnBar()//spawns random L shaped barriers
    {
        speed = myGM.GetComponent<SpeedManager>().getSpeed();
        timer4 += Time.deltaTime;
        if (timer4 * speed > Random.Range(1.5f, 2.0f))
        {
            timer4 = 0.0f;
            int side = Random.Range(0, 4);
            Vector2[] vertices;
            Vector2 ptr1 = new Vector2(0.0f, Random.Range(0.0f, 2.0f));
            Vector2 ptr2 = new Vector2(Random.Range(0.0f, 2.0f), 0.0f);
            //vertices = new Vector2[] { new Vector2(ptr1.x, ptr1.y), new Vector2(ptr1.x+0.2f, ptr1.y), new Vector2(0,0),
                //new Vector2(0.2f, 0.2f), new Vector2(ptr2.x, ptr2.y), new Vector2(ptr2.x, ptr2.y+0.2f)};
            vertices = new Vector2[] { new Vector2(ptr1.x, ptr1.y), new Vector2(ptr1.x+0.2f, ptr1.y), new Vector2(0.2f, 0.2f),
                new Vector2(ptr2.x, ptr2.y+0.2f), new Vector2(ptr2.x, ptr2.y), new Vector2(0,0)};
            ushort[] triangles = new ushort[] { 0, 1, 5, 1, 2, 5, 2, 3, 5, 3, 4, 5 };
            GameObject Bar = new GameObject();
            SpriteRenderer sr = Bar.AddComponent<SpriteRenderer>();
            Bar.AddComponent<SideMove>();
            PolygonCollider2D barCol = Bar.AddComponent<PolygonCollider2D>();
            barCol.points = vertices;
            barCol.SetPath(0, vertices);
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

    private void CheckMidtermSeason()
    {
        if (!isMidtermSeason)
        {
            Debug.Log("start midterm season");
            isMidtermSeason = true;

            workStartTime = 0.8f;
            workEndTime = 1.3f;
            funStartTime = 2.0f;
            funEndTime = 2.5f;
            healthStartTime = 2.0f;
            healthEndTime = 2.5f;
        }
        else
        {
            Debug.Log("end midterm season");
            isMidtermSeason = false;

            workStartTime = 1.5f;
            workEndTime = 2.0f;
            funStartTime = 1.5f;
            funEndTime = 2.0f;
            healthStartTime = 1.5f;
            healthEndTime = 2.0f;
        }
    }

    //this function is only called once on the setting page. Use UpdateDensity for the in-game density update.
    public void SetDensity(float percentage)//0.5 is the original percentage, for the interval starting at 1.5 and ends at 2.0
    {
        float newStart = 3.0f-percentage * 3.0f;
        workStartTime = newStart;
        workEndTime = workStartTime + 0.5f;
        funStartTime = newStart;
        funEndTime = workStartTime + 0.5f;
        healthStartTime = newStart;
        healthEndTime = workStartTime + 0.5f;
}

    //update density with percentage in game
    private void UpdateDensity(float percentage,string type)
{
        if (string.Equals(type,"work"))
        {
            workStartTime = workStartTime * percentage;
            workEndTime = workStartTime + 0.5f;
}
        if (string.Equals(type, "fun"))
        {
            funStartTime = funStartTime * percentage;
            funEndTime = funStartTime + 0.5f;
        }
        if (string.Equals(type, "health"))
        {
            healthStartTime = healthStartTime * percentage;
            healthEndTime = healthStartTime + 0.5f;
        }
    }
}


