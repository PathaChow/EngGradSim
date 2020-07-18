using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;            //speed per second
    public LayerMask blockingLayer;            //collision layer

    public bool rotate;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private GameObject myGM;
    private GameObject canvas;

    private float oldSpeed;
    //private EventMenu eventScript;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        myGM = GameObject.Find("GameManager");
        canvas = GameObject.Find("Canvas");

        oldSpeed = speed;
        //eventScript = canvas.GetComponent<EventMenu>();
    }

    void Update()
    {
        float horizontal = 0.0f;
        float vertical = 0.0f;
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && rb2D.position.y <= 3.0)
        {
            vertical = speed;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && rb2D.position.y >= -3.0)
        {
            vertical = -speed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (PlayerPrefs.GetInt("RubberBand") == 1 && rb2D.position.x <= -5.0f)//rubber band effect!
            {
                horizontal = (-rb2D.position.x - 5.0f) / 5 * speed - speed;
            }
            else
            {
                horizontal = -speed;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (PlayerPrefs.GetInt("RubberBand") == 1 && rb2D.position.x >= 0.0f)//rubber band effect!
            {
                horizontal = speed - (rb2D.position.x / 10.0f) * speed;
            }
            else
            {
                horizontal = speed;
            }
        }
        Vector2 velocity = new Vector2(horizontal, vertical);
        if (PlayerPrefs.GetInt("Accleration1") == 1)
        {
            if (Input.GetKey(KeyCode.B))
            {
                velocity = velocity * 2.0f;
            }
            if (Input.GetKey(KeyCode.M))
            {
                velocity = velocity * 0.5f;
            }
        }

        if (PlayerPrefs.GetInt("Accleration2") == 1)
        {
            rb2D.AddForce(velocity);
            rb2D.AddForce(-rb2D.velocity);
        }
        else
        {
            rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
        }
        if (transform.position.x < -10f)
        {
            //avoid from moving off the screen
            transform.position += Vector3.right * 0.5f;
        }
        if (rotate)
        {
            // rotates player to face direction of movement
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.fixedDeltaTime * 5.0f);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "fun")
        {
            //myGM.GetComponent<ScoreManager>().GiveRange();
            EventManager.TriggerEvent("giveRange");
            myGM.GetComponent<CollectionData>().addToCollection("intrest", 1);
        }
        if (other.tag == "work")
        {
            //myGM.GetComponent<ScoreManager>().AddToGPA(0.05f);
            //EventManager.TriggerEvent("addToGPA");
            myGM.GetComponent<CollectionData>().addToCollection("engineering", 1);
        }
        if (other.tag == "sleep")
        {
            //myGM.GetComponent<ScoreManager>().GiveHealth();
            EventManager.TriggerEvent("giveHealth");
            myGM.GetComponent<CollectionData>().addToCollection("physical", 1);
        }
        if (other.tag == "mentalhealth")
        {
            //myGM.GetComponent<ScoreManager>().GiveMHealth();
            EventManager.TriggerEvent("giveMHealth");
            myGM.GetComponent<CollectionData>().addToCollection("mental", 1);
        }
        if (other.tag == "event")
        {
            //eventScript.Pause();
            EventManager.TriggerEvent("PauseEvent");
        }
        if (other.tag == "boost")
        {
            EventManager.TriggerEvent("Boost");
        }
        if (other.tag == "bummer")
        {
            EventManager.TriggerEvent("Bummer");
        }

        Destroy(other.gameObject);
    }


    // events
    private void OnEnable()
    {
        EventManager.StartListening("Boost", PlayerBoost);
        EventManager.StartListening("Bummer", PlayerBummer);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Boost", PlayerBoost);
        EventManager.StopListening("Bummer", PlayerBummer);
    }

    // BOOST
    private void PlayerBoost()
    {
        speed = oldSpeed;
        gameObject.GetComponent<Animator>().speed = 1f;
        StopAllCoroutines();
        StartCoroutine("SpeedBoost");
    }

    IEnumerator SpeedBoost()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(.7f, .9f, 1f); // just to show effect for now
        gameObject.GetComponent<Animator>().speed *= 2.5f;
        speed *= 2f;

        yield return new WaitForSeconds(4); // lasts 4 seconds

        speed = oldSpeed;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        gameObject.GetComponent<Animator>().speed /= 2.5f;
        yield break;
    }

    // BUMMER
    private void PlayerBummer()
    {
        speed = oldSpeed;
        gameObject.GetComponent<Animator>().speed = 1f;
        StopAllCoroutines();
        StartCoroutine("SpeedBummer");
    }

    IEnumerator SpeedBummer()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, .7f, .9f); // just to show effect for now
        speed /= 3f;
        gameObject.GetComponent<Animator>().speed /= 2.5f;

        yield return new WaitForSeconds(4); // lasts 4 seconds

        speed = oldSpeed;
        gameObject.GetComponent<Animator>().speed *= 2.5f;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        yield break;
    }
}
