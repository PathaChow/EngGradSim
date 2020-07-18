using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float GPA;
    [SerializeField] int HEALTH;
    [SerializeField] int MHEALTH;
    [SerializeField] int RANGE;
    [SerializeField] int MaxHealth;
    [SerializeField] int MaxRange;
    [SerializeField] float TimeToTakeHealth; // health decreases over this interval

    bool unhealthy; // player did not collect enough health/mentalhealth daily/weekly
    bool dead; // true when health is 0

    float healthTimer;

    void OnEnable()
    {
        EventManager.StartListening("CheckUnhealthy", CheckUnhealthy);
        EventManager.StartListening("CheckDeath", CheckDeath);

        EventManager.StartListening("makeHealthy", makeHealthy);
        EventManager.StartListening("makeUnhealthy", makeUnhealthy);

        EventManager.StartListening("giveRange", GiveRange);
        EventManager.StartListening("giveHealth", GiveHealth);
        EventManager.StartListening("giveMHealth", GiveMHealth);

        EventManager.StartListening("TakeHealth", TakeHealth);
        EventManager.StartListening("TakeMHealth", TakeMHealth);
        //EventManager.StartListening("TakeRange", TakeRange);
    }
    void OnDisable()
    {
        EventManager.StopListening("CheckUnhealthy", CheckUnhealthy);
        EventManager.StopListening("CheckDeath", CheckDeath);

        EventManager.StopListening("makeHealthy", makeHealthy);
        EventManager.StopListening("makeUnhealthy", makeUnhealthy);

        EventManager.StopListening("giveRange", GiveRange);
        EventManager.StopListening("giveHealth", GiveHealth);
        EventManager.StopListening("giveMHealth", GiveMHealth);

        EventManager.StopListening("TakeHealth", TakeHealth);
        EventManager.StopListening("TakeMHealth", TakeMHealth);
        //EventManager.StopListening("TakeRange", TakeRange);

        // on scene change save current score in PlayerPrefs
        if (!dead)
            PlayerPrefs.SetInt("EndState", 1); // 1 is level complete
        else
            PlayerPrefs.SetInt("EndState", 0); // 0 is levelfailed

        PlayerPrefs.SetFloat("GPA", GPA);
        PlayerPrefs.SetFloat("Range", RANGE);
        PlayerPrefs.SetFloat("MHealth", MHEALTH);
        PlayerPrefs.SetFloat("Health", HEALTH);
    }

    private void Update()
    {
        DisplayScores();
    }

    // gives scores to UI text objects
    void DisplayScores()
    {
        GameObject.FindGameObjectWithTag("UIGPA").GetComponent<Text>().text = System.Math.Round(GPA, 2).ToString("F2");
        GameObject.FindGameObjectWithTag("UIRANGE").GetComponent<Text>().text = RANGE.ToString();
    }

    // score updating functions
    public void TakeHealth()
    {
        if (HEALTH - (MaxHealth * 2 / 100) > 0)
            HEALTH -= MaxHealth * 2 / 100; // take 1% of health
        else
            HEALTH = 0;
    }

    public void GiveHealth()
    {
        if (HEALTH + (MaxHealth * 10 / 100) < MaxHealth)
            HEALTH += MaxHealth * 10/100; // give 10% of health;
        else
            HEALTH = MaxHealth;
        
    }

    public void TakeMHealth()
    {
        if (MHEALTH - (MaxHealth * 2 / 100) > 0)
            MHEALTH -= MaxHealth * 2 / 100; // take 2% mental health
        else
            MHEALTH = 0;
    }
    public void GiveMHealth()
    {
        if (MHEALTH + (MaxHealth * 20 / 100) < MaxHealth)
            MHEALTH += MaxHealth * 20 / 100; // give 20% mental health
        else
            MHEALTH = MaxHealth;
    }

    public void TakeRange() // currently not used
    {
        if (RANGE > 0)
            RANGE--;
    }

    public void GiveRange()
    {
        if (RANGE < MaxRange)
            RANGE++;
    }

    public void AddToGPA(float points) // didn't refactor to event system since it has an argument
    {
        // gpa can't go past 4.0
        if (GPA + points < 4.00f)
            GPA += points;
        else
            GPA = 4f;
    }
    public void SubtractGPA(float points) // didn't refactor to event system since it has an argument
    {
        // gpa can't go under 0.0
        if (GPA - points > 0.00f)
            GPA -= points;
        else
            GPA = 0f;
    }

    // these functions probably shouldn't be here
    public void makeHealthy()
    {
        unhealthy = false;
    }

    public void makeUnhealthy()
    {
        unhealthy = true;
        CheckLowHealth();
    }


    // checks if player health is 0, and end level if dead
    public void CheckDeath()
    {
        if (HEALTH <= 0)
        {
            dead = true;
            // on death change scene
            gameObject.GetComponent<LevelManager>().ChangeScene("LevelComplete");
        }
    }

    public void CheckUnhealthy() // unhealthy event
    {
        if (unhealthy)
        {
            TakeHealthOverTime(TimeToTakeHealth);
        }

    }

    void TakeHealthOverTime(float duration)
    {
        // take health every x seconds
        healthTimer += Time.deltaTime;
        if (healthTimer > duration)
        {
            healthTimer = 0.0f;
            EventManager.TriggerEvent("TakeHealth");
            EventManager.TriggerEvent("TakeMHealth");
        }
    }

    // low health event (health is lower than 50%)
    public void CheckLowHealth()
    {
        if (HEALTH <= MaxHealth/2)
        {
            EventManager.TriggerEvent("LowHealth");
        }
    }


    // getters
    public int GetHealth()
    {
        return HEALTH;
    }
    public int GetMHealth()
    {
        return MHEALTH;
    }
    public int GetRange()
    {
        return RANGE;
    }
    public float GetGPA()
    {
        return GPA;
    }
    public float GetMaxHealth()
    {
        return MaxHealth;
    }
}
