using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// attached to UI text element with final score
public class GetPlayerScore : MonoBehaviour
{
    private float pGPA, pRange, pMHealth, pHealth;

    void OnEnable()
    {
        if (PlayerPrefs.GetInt("EndState") == 1) // if player completes level
        {
            // get end scores
            pGPA = PlayerPrefs.GetFloat("GPA");
            pRange = PlayerPrefs.GetFloat("Range");
            pMHealth = PlayerPrefs.GetFloat("MHealth");
            pHealth = PlayerPrefs.GetFloat("Health");

            // set up custom messages 
            // these are just placeholders for now
            string endMessage;

            if (pGPA >= 4f)
                endMessage = "\n* perfect student!!! *";
            else if(pGPA >= 3f)
                endMessage = "\n* good student *";
            else if (pGPA < 3f)
                endMessage = "\n* questionable student... *";
            else
                endMessage = "\n* ???!!! *";

            // display final scores
            gameObject.GetComponent<Text>().text = "Level Complete!!!\n" + endMessage + "\n\nGPA: " + pGPA.ToString("F2") + "\nHealth: " + pHealth + "\nMental Health: " + pMHealth + "\nRange: " + pRange;

            }

        if (PlayerPrefs.GetInt("EndState") == 0) // if player died
        {
            gameObject.GetComponent<Text>().text = "you're exhausted... x_x";
        }

    }
}
