using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// go back to the previous level
public class TryAgain : MonoBehaviour
{
    public void ChangeScene()
    {
        string NextLevelName = PlayerPrefs.GetString("LastLevelName");
        SceneManager.LoadScene(NextLevelName);
    }
}
