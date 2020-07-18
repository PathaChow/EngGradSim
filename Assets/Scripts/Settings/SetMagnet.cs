using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMagnet : MonoBehaviour
{
    public void SetMagnetRadius(float num)
    {
        PlayerPrefs.SetFloat("MagnetRadius", num);

    }
    public void SetMagnetIntn(float num)
    {
        PlayerPrefs.SetFloat("MagnetIntensity", num);

    }
}
