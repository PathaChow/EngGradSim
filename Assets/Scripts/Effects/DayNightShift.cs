using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// kind of shows sky changing from day to night
// lerps bg color of camera
public class DayNightShift : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Color Color1;
    [SerializeField] Color Color2;
    [SerializeField] float duration;
    private float step;

    private void Awake()
    {
        StartCoroutine(LerpColorsOverTime(Color1, Color2, duration));
    }

    private IEnumerator LerpColorsOverTime(Color startingColor, Color endingColor, float time)
    {
        float inversedTime = 1 / time;
        for (step = 0.0f; step < 1.0f; step += Time.deltaTime * inversedTime)
        {
            cam.backgroundColor = Color.Lerp(startingColor, endingColor, step);
            yield return null;
        }
    }
}
