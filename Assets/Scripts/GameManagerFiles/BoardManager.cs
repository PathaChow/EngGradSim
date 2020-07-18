
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    /*public GameObject work;
    public GameObject fun;
    public GameObject sleep;
    public GameObject mentalhealth;
    public GameObject eventSprite;
    public GameObject Midterm;
    public GameObject MidtermArea;
    public GameObject Boost;
    public GameObject Bummer;*/

    public float DayDuration;

    private GameObject myGM;
    private float speed = 0.0f;
    private Camera cam;

    private float workStartTime;
    private float workEndTime;
    private float funStartTime;
    private float funEndTime;
    private float healthStartTime;
    private float healthEndTime;

    private float inMonthTimer;
    private int monthCount;

    private bool isMidtermSeason;
    private float midtermTimer;
    //private float midtermFrequency = 20f;
    //private float midtermDuration = 10f;
    private float examPosition = 20f;
    private float examLength = 20f;
    private List<Artifacts> mArtifacts;
    private Artifacts bummer, boost;

    void OnEnable()
    {
        //EventManager.StartListening("SetDensity", SetDensity);

        bummer = new Artifacts("bummer", -.3f, 0f, .3f, 0f, true);
        boost = new Artifacts("boost", -.3f, 0f, .3f, 0f, true);
        EventManager.StartListening("LowHealth", bummer.SpecialSpawn);
        EventManager.StartListening("makeHealthy", boost.SpecialSpawn);
    }

    void OnDisable()
    {
        //EventManager.StopListening("SetDensity", SetDensity);

        EventManager.StopListening("LowHealth", bummer.SpecialSpawn);
        EventManager.StopListening("makeHealthy", boost.SpecialSpawn);
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        myGM = GameObject.Find("GameManager");

        isMidtermSeason = false;

        inMonthTimer = 0f;
        monthCount = 0;
        /*workStartTime = 1.5f;
        workEndTime = 2.0f;
        funStartTime = 1.5f;
        funEndTime = 2.0f;
        healthStartTime = 1.5f;
        healthEndTime = 2.0f;
        midtermTimer = 0f;*/
        SetDensity();
        if (PlayerPrefs.GetInt("Exam") != 1)
        {
            NoExam();
        }
        else if (PlayerPrefs.GetInt("Exam") == 2)
        {
            //to be adjusted
            examPosition = 20f;
            examLength = 5f;
        }
        DayDuration = 0.2f;
        mArtifacts = new List<Artifacts>();
        //mArtifacts.Add(new Artifacts("work", -1.7f, 0f, 1.7f, DayDuration * 1));
        //mArtifacts.Add(new Artifacts("fun", -3.5f, 3.2f, 0.3f, DayDuration * 1));
        //mArtifacts.Add(new Artifacts("sleep", -3.0f, 2.0f, 1.0f, DayDuration * 1));
        //mArtifacts.Add(new Artifacts("mentalhealth", -3.5f, 2.5f, 1.0f, DayDuration * 1));
        //mArtifacts.Add(new Artifacts("eventSprite", -3.0f, -3.0f, 0.1f, DayDuration * 10));
        mArtifacts.Add(new Artifacts("mentorship", -3.5f, 3.2f, 0.3f, DayDuration * 10, true));
        mArtifacts.Add(new Artifacts("cohortmeeting", -3.5f, 3.2f, 0.3f, DayDuration * 15, true));
        mArtifacts.Add(new Artifacts("interdisciplinary", -3.5f, 3.2f, 0.3f, DayDuration * 15, true));

        mArtifacts.Add(new Artifacts("friendship", -3.5f, 2.5f, 1.0f, DayDuration * 7, true));
        mArtifacts.Add(new Artifacts("flirting", -3.5f, 2.5f, 1.0f, DayDuration * 30, true));
        mArtifacts.Add(new Artifacts("hobbies", -3.5f, 2.5f, 1.0f, DayDuration * 7, true));

        mArtifacts.Add(new Artifacts("sleep", -3.0f, 2.0f, 1.0f, DayDuration * 7, true));
        mArtifacts.Add(new Artifacts("convenientmeal", -3.0f, 2.0f, 1.0f, DayDuration * 7, true));
        mArtifacts.Add(new Artifacts("healthymeal", -3.0f, 2.0f, 1.0f, DayDuration * 14, true));
        mArtifacts.Add(new Artifacts("exercise", -3.0f, 2.0f, 1.0f, DayDuration * 14, true));

        mArtifacts.Add(new Artifacts("academicadvising", -1.7f, 0f, 1.7f, DayDuration * 30, true));
        mArtifacts.Add(new Artifacts("assignments", -1.7f, 0f, 1.7f, DayDuration * 15, true));
        mArtifacts.Add(new Artifacts("lab", -1.7f, 0f, 1.7f, DayDuration * 15, true));
        mArtifacts.Add(new Artifacts("exam", -1.7f, 0f, 1.7f, DayDuration * 10, false));

    }

    // Update is called once per frame
    void Update()
    {
        speed = myGM.GetComponent<SpeedManager>().getSpeed();
        //SpawnT1();
        //SpawnT2();
        //SpawnT3();
        //SpawnBar();
        /*if(PlayerPrefs.GetInt("Exam") == 2)
        {
            midtermTimer += Time.deltaTime;
            if (!isMidtermSeason)
            {
                if (midtermTimer*speed >= examPosition)
                {
                    midtermTimer = 0f;
                    UnityEngine.Debug.Log("enter midterm season");
                    CheckMidtermSeason();
                }
            }
            else
            {
                if (midtermTimer * speed >= examLength)
                {
                    midtermTimer = 0f;
                    UnityEngine.Debug.Log("exit midterm season");
                    CheckMidtermSeason();
                }
            }
        }*/
        inMonthTimer += Time.deltaTime;
        if (inMonthTimer > DayDuration * 30)
        {
            monthCount += 1;
            inMonthTimer = 0;
        }
        if (monthCount == 5)
        {
            monthCount = 0;
            inMonthTimer = 0;
        }
        if (monthCount == 2)
        {
            mArtifacts[mArtifacts.Count - 1].enabled = true;
            mArtifacts[mArtifacts.Count - 1].period = 10 * DayDuration;
        }
        else if (monthCount == 4)
        {
            mArtifacts[mArtifacts.Count - 1].enabled = true;
            mArtifacts[mArtifacts.Count - 1].period = 6 * DayDuration;
        }
        else
        {
            mArtifacts[mArtifacts.Count - 1].enabled = false;
        }
        foreach (Artifacts obj in mArtifacts)
        {
            obj.SpawnSelf();
        }
    }

    private void NoExam()
    {
        // for mutiple exams and exam areas
        GameObject[] exams;
        exams = GameObject.FindGameObjectsWithTag("exam");
        foreach (GameObject exam in exams)
        {
            Destroy(exam);
        }
        //Destroy(Midterm);
        //Destroy(MidtermArea);
    }
    private void CheckMidtermSeason()
    {
        if (!isMidtermSeason)
        {
            //Debug.Log("start midterm season");
            isMidtermSeason = true;
            UpdateDensity(0.5f, "work");
            UpdateDensity(2.0f, "fun");
            UpdateDensity(2.0f, "health");
        }
        else
        {
            //Debug.Log("end midterm season");
            isMidtermSeason = false;
            SetDensity();
        }
    }

    public static void NoOverlapSpawn(float ymin, float ymax, GameObject spawnObj)
    {
        float x1 = Random.Range(9.0f, 10.0f);
        float y1 = Random.Range(ymin, ymax);
        Vector3 spawnPos = new Vector3(x1, y1, 0);

        // overlap check
        int count = 0;
        Collider2D overlap = Physics2D.OverlapCircle(spawnPos, .2f);

        // while overlap try a new position
        while (overlap != null && count < 20)
        {
            x1 = Random.Range(9.0f, 10.0f);
            y1 = Random.Range(ymin, ymax);
            spawnPos = new Vector3(x1, y1, 0);
            overlap = Physics2D.OverlapCircle(spawnPos, .2f);
            count++;
        }
        // will not spawn if check failed 20 times
        if (overlap == null)
        {
            Instantiate(spawnObj, spawnPos, Quaternion.identity);
        }
    }

    //this function is only called on the setting page and reset to original after exams. Use UpdateDensity for flexible density update.
    public void SetDensity()//0.5 is the original percentage, for the interval starting at 1.5 and ends at 2.0
    {
        float percentage = PlayerPrefs.GetFloat("SpawnConstant");
        //UnityEngine.Debug.Log(percentage);
        float newStart = 5.0f - percentage * 5.0f;
        workStartTime = newStart;
        workEndTime = workStartTime + 0.5f;
        funStartTime = newStart;
        funEndTime = workStartTime + 0.5f;
        healthStartTime = newStart;
        healthEndTime = workStartTime + 0.5f;
    }

    //update density with percentage in game
    private void UpdateDensity(float percentage, string type)
    {
        if (string.Equals(type, "work"))
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

public class Artifacts
{
    public string prefab;
    public float yStart1;
    public float yStart2;
    public float width;
    public float period;
    public float currInterval;
    public float timer;
    public bool afterFlag;
    public bool enabled;
    public Artifacts(string prefab, float yStart1, float yStart2, float width, float period, bool enabled)
    {
        this.prefab = prefab;
        this.yStart1 = yStart1;
        this.yStart2 = yStart2;
        this.width = width;
        this.period = period;
        this.enabled = enabled;
        this.currInterval = Random.Range(0f, period);
        this.timer = 0f;
        this.afterFlag = false;
    }
    public void SpawnSelf()
    {
        timer += Time.deltaTime;
        if (timer > period)
        {
            currInterval = Random.Range(0f, period);
            timer = 0f;
            afterFlag = false;
        }
        if (timer > currInterval && !afterFlag)
        {
            GameObject mInstance = Resources.Load("Prefabs/" + prefab, typeof(GameObject)) as GameObject;
            afterFlag = true;
            if (!this.enabled)
            {
                return;
            }
            if (Random.Range(0.0f, 1.0f) > 0.5f)
            {
                BoardManager.NoOverlapSpawn(yStart1, yStart1 + width, mInstance);
            }
            else
            {
                BoardManager.NoOverlapSpawn(yStart2, yStart2 + width, mInstance);
            }
        }
    }
    public void SpecialSpawn() // day independant spawn
    {
        GameObject mInstance = Resources.Load("Prefabs/" + prefab, typeof(GameObject)) as GameObject;
        // has a 50% chance of not spawning
        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            if (Random.Range(0.0f, 1.0f) > 0.5f)
                BoardManager.NoOverlapSpawn(yStart1, yStart1 + width, mInstance);
            else
                BoardManager.NoOverlapSpawn(yStart2, yStart2 + width, mInstance);
        }

    }
}