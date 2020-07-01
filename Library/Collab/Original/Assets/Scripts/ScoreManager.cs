using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float GPA;
    [SerializeField] int HEALTH;
    [SerializeField] int RANGE;
    [SerializeField] int MaxHealth;
    [SerializeField] int MaxRange;
    [SerializeField] float TimeToTakeHealth; // health decreases over this interval
    [SerializeField] float WaitTime; // time to wait until health starts decreasing

    bool dead; // true when health is 0
    float healthTimer;
    float waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        // initialize start of level values if any
    }

    private void Update()
    {
        waitTimer += Time.deltaTime;

        DisplayScores();
        CheckDeath();

        if (waitTimer > WaitTime)
            TakeHealthOverTime(TimeToTakeHealth);

    }

    // gives scores to UI text objects
    void DisplayScores()
    {
        // will have bars instead of number values for health/range
        GameObject.FindGameObjectWithTag("UIGPA").GetComponent<Text>().text = "GPA " + System.Math.Round(GPA, 2);
        GameObject.FindGameObjectWithTag("UIHEALTH").GetComponent<Text>().text = "HEALTH " + HEALTH;
        GameObject.FindGameObjectWithTag("UIRANGE").GetComponent<Text>().text = "RANGE " + RANGE;
    }

    public int getRange()
    {
        return RANGE;
    }

    // score updating functions
    public void TakeHealth()
    {
        if (HEALTH > 0)
            HEALTH--;

        // change to red
        GameObject.FindGameObjectWithTag("UIHEALTH").GetComponent<Text>().color = new Color(1, 0, 0);
    }

    public void GiveHealth()
    {
        if (HEALTH < MaxHealth)
            HEALTH++;

        // change to green
        GameObject.FindGameObjectWithTag("UIHEALTH").GetComponent<Text>().color = new Color(0, 1, 0);
    }

    public void TakeRange()
    {
        if (RANGE > 0)
            RANGE--;
    }

    public void GiveRange()
    {
        if (RANGE < MaxRange)
            RANGE++;
    }

    public void AddToGPA(float points)
    {
        // gpa can't go past 4.0
        if (GPA + points < 4.00f)
            GPA += points;
    }
    public void SubtractGPA(float points)
    {
        // gpa can't go under 0.0
        if (GPA - points > 0.00f)
            GPA -= points;
    }

    // checks if player health is 0, and end level if dead
    void CheckDeath()
    {
        if (HEALTH <= 0)
        {
            dead = true;
            // on death change scene
            gameObject.GetComponent<LevelManager>().ChangeScene("LevelComplete");
        }
    }

    void TakeHealthOverTime(float duration)
    {
        // take health every second
        healthTimer += Time.deltaTime;
        if (healthTimer > duration)
        {
            healthTimer = 0.0f;
            TakeHealth();
        }
    }

    // on scene change save current score in PlayerPrefs
    void OnDisable()
    {
        if (!dead)
            PlayerPrefs.SetInt("EndState", 1); // 1 is level complete
        else
            PlayerPrefs.SetInt("EndState", 0); // 0 is levelfailed

        PlayerPrefs.SetFloat("GPA", GPA);
    }
}
