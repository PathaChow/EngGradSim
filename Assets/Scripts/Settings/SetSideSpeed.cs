using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSideSpeed : MonoBehaviour
{
    public void SetScrollSpeed(float num)
    {
        PlayerPrefs.SetFloat("ScrollSpeed", num);
    }
}
