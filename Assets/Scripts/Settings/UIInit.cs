using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInit : MonoBehaviour
{
    private float spawnConstant;
    private float scrollSpeed;
    private int rubberBand;
    private int acceleration1;
    private int acceleration2;
    private int exam;
    private int magnet;
    private float mRadius, mIntensity;

    public GameObject speedSlider;
    public GameObject densitySlider;
    public GameObject intensitySlider;
    public GameObject radiusSlider;
    public GameObject accl1Toggle;
    public GameObject accl2Toggle;
    public GameObject rubberBandToggle;
    public GameObject magnetToggle;

    private void OnEnable()
    {
        spawnConstant = PlayerPrefs.GetFloat("SpawnConstant", .5f);
        scrollSpeed = PlayerPrefs.GetFloat("ScrollSpeed", 3f);
        rubberBand = PlayerPrefs.GetInt("RubberBand", 1);
        acceleration1 = PlayerPrefs.GetInt("Accleration1", 1);
        acceleration2 = PlayerPrefs.GetInt("Accleration2", 0);
        //exam = PlayerPrefs.GetInt("Exam", 1);
        magnet = PlayerPrefs.GetInt("Magnet", 0);
        mRadius = PlayerPrefs.GetFloat("MagnetRadius", 2f);
        mIntensity = PlayerPrefs.GetFloat("MagnetIntensity", 22f);
        // update UI
        SetValues();
    }

    // this is to show the number values of speed and density
    // lazy so i'm gonna call this in update
    private void Update()
    {
        densitySlider.GetComponentInChildren<Text>().text = "Density: " + PlayerPrefs.GetFloat("SpawnConstant");
        speedSlider.GetComponentInChildren<Text>().text = "Speed: " + PlayerPrefs.GetFloat("ScrollSpeed");

        if (PlayerPrefs.GetInt("Magnet") == 1) // on
        {
            intensitySlider.SetActive(true);
            radiusSlider.SetActive(true);
            radiusSlider.GetComponentInChildren<Text>().text = "Reach: " + PlayerPrefs.GetFloat("MagnetRadius");
            intensitySlider.GetComponentInChildren<Text>().text = "Intensity: " + PlayerPrefs.GetFloat("MagnetIntensity");
        }
        else
        {
            intensitySlider.SetActive(false);
            radiusSlider.SetActive(false);
        }

    }

    void SetValues()
    {
        // board sliders
        speedSlider.GetComponent<Slider>().value = scrollSpeed;
        densitySlider.GetComponent<Slider>().value = spawnConstant;
        // magnet sliders
        intensitySlider.GetComponent<Slider>().value = mIntensity;
        radiusSlider.GetComponent<Slider>().value = mRadius;

        // slider text
        densitySlider.GetComponentInChildren<Text>().text = "Density: " + scrollSpeed;
        speedSlider.GetComponentInChildren<Text>().text = "Speed: " + spawnConstant;
        radiusSlider.GetComponentInChildren<Text>().text = "Reach: " + mIntensity;
        intensitySlider.GetComponentInChildren<Text>().text = "Intensity: " + mRadius;

        // toggles
        switch (acceleration1)
        {
            case 1:
                accl1Toggle.GetComponent<Toggle>().isOn = true;
                accl2Toggle.GetComponent<Toggle>().isOn = false;
                break;
            case 0:
                accl1Toggle.GetComponent<Toggle>().isOn = false;
                accl2Toggle.GetComponent<Toggle>().isOn = true;

                break;
        }
        switch (rubberBand)
        {
            case 1:
                rubberBandToggle.GetComponent<Toggle>().isOn = true;
                break;
            case 0:
                rubberBandToggle.GetComponent<Toggle>().isOn = false;
                break;
        }
        switch (magnet)
        {
            case 1:
                magnetToggle.GetComponent<Toggle>().isOn = true;
                break;
            case 0:
                magnetToggle.GetComponent<Toggle>().isOn = false;
                break;
        }

    }

    // initializes for the first time, should only happen once
    public void DefaultInit()
    {
        // default values
        PlayerPrefs.SetFloat("SpawnConstant", .5f);
        PlayerPrefs.SetFloat("ScrollSpeed", 3f);
        PlayerPrefs.SetInt("RubberBand", 1);
        PlayerPrefs.SetInt("Accleration1", 1);
        PlayerPrefs.SetInt("Accleration2", 0);
        //PlayerPrefs.SetInt("Exam", 1);
        PlayerPrefs.SetInt("Magnet", 0);
        PlayerPrefs.SetFloat("MagnetRadius", 2f);
        PlayerPrefs.SetFloat("MagnetIntensity", 22f);

        OnEnable();

    }

}

