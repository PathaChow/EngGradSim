using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSelection : MonoBehaviour
{
    [SerializeField] int numberOfCharacters = 3;
    int num; // current selection

    public void Increment(int amount) // amount = 1 forwards, amount = -1 backwards
    {

        num = (num + amount) % numberOfCharacters;

        if (num < 0) // c# doesn't have negative mod T-T
            num = numberOfCharacters - 1;

        PlayerPrefs.SetInt("Character", num);
    }
}
