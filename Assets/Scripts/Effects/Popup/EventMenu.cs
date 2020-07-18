using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI;

public class EventMenu : MonoBehaviour
{
    public GameObject eventUI;
    public Image img;
    private bool isPaused;
    private static int counter = 0; // 0 = popup hasn't been shown yet; 1 = popup has been shown, so don't show again

    void OnEnable()
    {
        EventManager.StartListening("PauseEvent", Pause);
    }
    void OnDisable()
    {
        EventManager.StopListening("PauseEvent", Pause);
    }

    void Start()
    {
        // start with popup menu open
        if (counter == 0)
        {
            eventUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            counter++;
        }
        else
        {
            eventUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            img.enabled = false;
        }
    }

    void Update()
    {
        if (isPaused)
        {
            if (Input.GetKey("space"))
            {
                img.enabled = false;
                Resume();
            }
        }
    }

    public void Resume() // called in Canvas > PauseMenu > Button (in inspector)
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