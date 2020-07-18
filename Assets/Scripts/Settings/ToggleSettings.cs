using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSettings : MonoBehaviour
{
    public GameObject accl1;
    public GameObject accl2;
    public void SetRubberBand(bool status)
    {
        int intStatus = status ? 1 : 0;
        PlayerPrefs.SetInt("RubberBand", intStatus);
    }

    public void SetAccleration1(bool status)
    {
        int intStatus = status ? 1 : 0;
        if (PlayerPrefs.GetInt("Accleration2") == 1)//you could only choose 1 between 2 acclerations
        {
            PlayerPrefs.SetInt("Accleration2", 0);
            accl2.GetComponent<Toggle>().isOn = false;
        }
        PlayerPrefs.SetInt("Accleration1", intStatus);
    }

    public void SetAccleration2(bool status)
    {
        int intStatus = status ? 1 : 0;
        if (PlayerPrefs.GetInt("Accleration1") == 1)//you could only choose 1 between 2 acclerations
        {
            PlayerPrefs.SetInt("Accleration1", 0);
            accl1.GetComponent<Toggle>().isOn = false;
        }
        PlayerPrefs.SetInt("Accleration2", intStatus);
    }


    public void MagnetOn(bool status)
    {
        int intStatus = status ? 1 : 0;
        PlayerPrefs.SetInt("Magnet", intStatus);
    }
}
