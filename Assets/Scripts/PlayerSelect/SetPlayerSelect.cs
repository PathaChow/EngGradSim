using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerSelect : MonoBehaviour
{
    public void SetCharacterChoice(int num)
    {
        PlayerPrefs.SetInt("Character", num);
    }
}
