using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// attached to UI text element with final score
public class GetPlayerScore : MonoBehaviour
{
    float playerGPA;

    void OnEnable()
    {
        if (PlayerPrefs.GetInt("EndState") == 1) // if player completes level
        {
            playerGPA = PlayerPrefs.GetFloat("GPA");
            gameObject.GetComponent<Text>().text += playerGPA;
        }

        if (PlayerPrefs.GetInt("EndState") == 0) // if player died
        {
            gameObject.GetComponent<Text>().text = "you're exhausted... x_x";
        }

    }
}
