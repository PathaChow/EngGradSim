using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// magnet effect
// attatch to object you want to move toward the player
// object must have a rigidbody2d attached
public class Magnet : MonoBehaviour
{
    public float intensity;
    public float magnetRadius;

    Transform player;
    Rigidbody2D rb;
    private float distance;

    private float origIntensity;
    private float origRadius;

    private void OnEnable()
    {
        EventManager.StartListening("Boost", ActiveBoost);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Boost", ActiveBoost);
    }

    void Start()
    {
        // get settings
        intensity = PlayerPrefs.GetFloat("MagnetIntensity",22f);
        magnetRadius = PlayerPrefs.GetFloat("MagnetRadius", 1.5f);
        origIntensity = intensity;
        origRadius = magnetRadius;

        // if magnet option is false then set to 0
        if (PlayerPrefs.GetInt("Magnet") == 0)
        {
            intensity = 0f;
            magnetRadius = 0f;
        }

        // init
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (this.transform.position - player.transform.position).sqrMagnitude;
        if (distance < magnetRadius)
        {

            rb.AddForce((player.transform.position - transform.position) * intensity);
        }

    }
    private void ActiveBoost()
    {
        intensity = origIntensity;
        magnetRadius = origRadius;

        StopAllCoroutines();
        StartCoroutine("MagnetBoost");
    }

    IEnumerator MagnetBoost()
    {

        intensity = 40f;
        magnetRadius = 3.5f;

        yield return new WaitForSeconds(4); // lasts 4 seconds

        intensity = origIntensity;
        magnetRadius = origRadius;

        yield break;
    }
}
