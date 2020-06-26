using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string NextLevelName;
    [SerializeField] float WaitTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= WaitTime)
        {
            timer = 0.0f;
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(NextLevelName);
    }
}
