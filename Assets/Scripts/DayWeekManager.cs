using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// deals with events that happen every day and every week
// right now a work week is 5 days
public class DayWeekManager : MonoBehaviour
{
    [SerializeField] float DayDuration;

    float dayTimer;
    float weekTimer;

    // Update is called once per frame
    void Update()
    {
        dayTimer += Time.deltaTime;
        weekTimer += Time.deltaTime;

        if(dayTimer >= DayDuration)
        {
            // perform day calculations
            gameObject.GetComponent<CollectionData>().DailyDataCheck();

            // reset
            dayTimer = 0.0f;
            gameObject.GetComponent<CollectionData>().resetDayData();
            Debug.Log("new day");
        }
        if (weekTimer >= DayDuration * 5) // 5 work days in a week?
        {
            // perform week calculations
            gameObject.GetComponent<CollectionData>().WeeklyDataCheck();

            // reset
            weekTimer = 0.0f;
            gameObject.GetComponent<CollectionData>().resetWeekData();
            Debug.Log("new week");
        }
    }

}
