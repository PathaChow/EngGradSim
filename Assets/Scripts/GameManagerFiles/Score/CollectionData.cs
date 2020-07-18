using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// keeps track of everything player collects in a day or week
// most of these functions are called in DayWeekManager

public class CollectionData : MonoBehaviour
{
    // keeps track of what player collects in a day/week
    Dictionary<string, int> DayCollection = new Dictionary<string, int>();
    Dictionary<string, int> WeekCollection = new Dictionary<string, int>();

    private int missedWork;
    private bool missedExam;

    // Start is called before the first frame update
    void Start()
    {
        // initialize day
        DayCollection.Add("physical", 0);      // affects health
        DayCollection.Add("mental", 0);        // affects mental health
        DayCollection.Add("engineering", 0);   // affects gpa
        DayCollection.Add("intrest", 0);       // affects range
        // week
        WeekCollection.Add("physical", 0);      // affects health
        WeekCollection.Add("mental", 0);        // affects mental health
        WeekCollection.Add("engineering", 0);   // affects gpa
        WeekCollection.Add("intrest", 0);       // affects range

    }

    void OnEnable()
    {
        EventManager.StartListening("DailyDataCheck", DailyDataCheck);
        EventManager.StartListening("WeeklyDataCheck", WeeklyDataCheck);
        EventManager.StartListening("GPACheck", GPACheck);
        EventManager.StartListening("addMissedWork", addMissedWork);
        EventManager.StartListening("MissedExam", MissedExam);

        EventManager.StartListening("resetDayData", resetDayData);
        EventManager.StartListening("resetWeekData", resetWeekData);
    }
    void OnDisable()
    {
        EventManager.StopListening("DailyDataCheck", DailyDataCheck);
        EventManager.StopListening("WeeklyDataCheck", WeeklyDataCheck);
        EventManager.StopListening("GPACheck", GPACheck);
        EventManager.StopListening("addMissedWork", addMissedWork);

        EventManager.StopListening("resetDayData", resetDayData);
        EventManager.StopListening("resetWeekData", resetWeekData);
        EventManager.StopListening("MissedExam", MissedExam);
    }

    // resets all collection values to 0, perserves keys
    // called by DayWeekManager
    public void resetDayData()
    {
        DayCollection.Keys.ToList().ForEach(x => DayCollection[x] = 0);
    }

    public void resetWeekData()
    {
        WeekCollection.Keys.ToList().ForEach(x => WeekCollection[x] = 0);
    }

    // updates collection, to be called on collision with objects
    public void addToCollection(string type, int toAdd)
    {
        // incriments counter for type of marker collected
        DayCollection[type] += toAdd;
        WeekCollection[type] += toAdd;
    }

    // daily checks to be called from day week manager
    // should maybe move these to a better place
    public void DailyDataCheck() // daily check for physical health
    {
        if (DayCollection["physical"] >= 1)
        {
            // healthy
            // probably play some animation etc. 
            // health is green
            Debug.Log("healthy");
            //gameObject.GetComponent<ScoreManager>().makeHealthy();
            EventManager.TriggerEvent("makeHealthy");
        }
        else
        {
            // unhealthy
            // health in the red
            // start decreasing health
            Debug.Log("unhealthy ;-;");
            //gameObject.GetComponent<ScoreManager>().makeUnhealthy();
            EventManager.TriggerEvent("makeUnhealthy");
        }
    }
    public void WeeklyDataCheck()
    {
        if (WeekCollection["mental"] >= 2)
        {
            // good mental health
            // not sure what to do here
        }
        else
        {
            // if less than 2 mental healths collected a week player becomes unhealthy
            // mental and normal health both start degrading
            //gameObject.GetComponent<ScoreManager>().makeUnhealthy();
            EventManager.TriggerEvent("makeUnhealthy");
        }
    }
    public void GPACheck()
    {
        // check for missed work
        for(int i=0; i < missedWork; i++)
        {
            gameObject.GetComponent<ScoreManager>().SubtractGPA(.05f);
        }
        missedWork = 0;
        // check for missed exam
        if (missedExam)
        {
            gameObject.GetComponent<ScoreManager>().SubtractGPA(.50f);
            missedExam = false;
        }


    }
    public void addMissedWork()
    {
        missedWork++;
    }
    public void MissedExam()
    {
        missedExam = true;
    }
}
