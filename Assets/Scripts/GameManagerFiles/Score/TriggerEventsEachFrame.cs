using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventsEachFrame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        EventManager.TriggerEvent("CheckUnhealthy"); // check if player is unhealthy - ScoreManager
        EventManager.TriggerEvent("CheckDeath"); // check if player is dead - ScoreManager
    }
}
