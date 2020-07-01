using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string NewLevelName;
    [SerializeField] float WaitTime;
    float timer;

    public bool isNotTimed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // by default, level will change after specified amount of time, 
        // unless isNotTimed is true

        if (!isNotTimed)
        {
            timer += Time.deltaTime;
            if (timer >= WaitTime)
            {
                timer = 0.0f;
                ChangeScene(NewLevelName);
            }
        }
    }

    public void ChangeScene(string NextLevelName)
    {
        SceneManager.LoadScene(NextLevelName);
    }

    // on scene change save current level name in PlayerPrefs
    private void OnDisable()
    {
        PlayerPrefs.SetString("LastLevelName", SceneManager.GetActiveScene().name);
    }
}
