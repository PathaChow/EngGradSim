using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static float speed = -2f;
    //private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        speed = -PlayerPrefs.GetFloat("ScrollSpeed");
        //timer += Time.deltaTime;

        //if (timer >= 15f)
        //{
        //    speed *= 1.5f;
        //    timer = 0f;
        //}
    }
    public float getSpeed()
    {
        return -speed;
    }
}
