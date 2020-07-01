using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMenu : MonoBehaviour
{
    public GameObject eventUI;

    void Start()
    {
        eventUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        eventUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        eventUI.SetActive(true);
        Time.timeScale = 0f;
    }
}