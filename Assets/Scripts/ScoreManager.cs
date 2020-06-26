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

    // Start is called before the first frame update
    void Start()
    {
        // initialize start of level values
        HEALTH = MaxHealth;
        RANGE = 0;
    }

    private void Update()
    {
        DisplayScores();
    }

    // gives scores to UI text objects
    void DisplayScores()
    {
        // FIXME will have bars instead of number values for health/range
        GameObject.FindGameObjectWithTag("UIGPA").GetComponent<Text>().text = "GPA " + System.Math.Round(GPA,2);
        GameObject.FindGameObjectWithTag("UIHEALTH").GetComponent<Text>().text = "HEALTH " + HEALTH; 
        GameObject.FindGameObjectWithTag("UIRANGE").GetComponent<Text>().text = "RANGE " + RANGE;
    }

    // score updating functions
    public void TakeHealth()
    {
        if (HEALTH > 0)
            HEALTH--;
    }

    public void GiveHealth()
    {
        if (HEALTH < MaxHealth)
            HEALTH++;
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
        if (GPA - points < 4.00f)
            GPA += points;
    }
    public void SubtractGPA(float points)
    {
        // gpa can't go under 0.0
        if (GPA - points > 0.00f)
            GPA -= points;
    }
}
