using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDensity : MonoBehaviour
{
    public void SetDayDuration(float num)
    {
        PlayerPrefs.SetFloat("DayDuration", num);
    }
}
