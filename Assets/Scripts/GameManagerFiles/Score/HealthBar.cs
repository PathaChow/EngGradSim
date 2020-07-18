using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// modified to be SMOOOOTH
// why was this so hard
public class HealthBar : MonoBehaviour
{
    Image healthBar; // max at 91.3% of the bar
    Image mHealthBar; // max at 91.3% of the bar
    float maxPercent = 0.913f;

    float maxHealth;
    float health;
    float mHealth;

    private float step; // for smoothing

    // Start is called before the first frame update
    void Start()
    {
        healthBar = this.GetComponent<Image>();
        mHealthBar = GameObject.Find("MHEALTH bar").GetComponent<Image>();
        maxHealth = GameObject.Find("GameManager").GetComponent<ScoreManager>().GetMaxHealth();

        // smoothly updates health bars
        // I just found out about this function and its the best thing ever
        InvokeRepeating("StartHealth", 0f, .1f);
        InvokeRepeating("StartMHealth", 0f, .1f);
    }

    // old stuff!!
    // Update is called once per frame

    /*void Update()
    {
        health = GameObject.Find("GameManager").GetComponent<ScoreManager>().GetHealth();
        mHealth = GameObject.Find("GameManager").GetComponent<ScoreManager>().GetMHealth();

        mHealthBar.fillAmount = mHealth / maxHealth * maxPercent;
        healthBar.fillAmount = health / maxHealth * maxPercent;

    }*/

    // for transition
    private IEnumerator LerpFillHealth(bool mental, float startingFill, float endingFill, float time)
    {
        float inversedTime = 1 / time;
        for (step = 0.0f; step < 1.0f; step += Time.deltaTime * inversedTime)
        {
            if (mental)
                mHealthBar.fillAmount = Mathf.Lerp(startingFill, endingFill, step);
            else
                healthBar.fillAmount = Mathf.Lerp(startingFill, endingFill, step);
            yield return null;
        }
        yield break;
    }

    // start the transitions
    void StartMHealth()
    {
        mHealth = GameObject.Find("GameManager").GetComponent<ScoreManager>().GetMHealth();
        float origMFill = mHealthBar.fillAmount;
        float newMFill = mHealth / maxHealth * maxPercent;

        //StopAllCoroutines(); // why? idk
        StartCoroutine(LerpFillHealth(true, origMFill, newMFill, 1f));
    }
    void StartHealth()
    {
        health = GameObject.Find("GameManager").GetComponent<ScoreManager>().GetHealth();
        float origFill = healthBar.fillAmount;
        float newFill = health / maxHealth * maxPercent;

        StopAllCoroutines(); // it only works here, its a mystery
        StartCoroutine(LerpFillHealth(false, origFill, newFill, 1f));
    }

}
