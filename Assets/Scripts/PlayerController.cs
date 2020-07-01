using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;            //speed per second
    public LayerMask blockingLayer;            //collision layer
    
    private BoxCollider2D boxCollider; 
    private Rigidbody2D rb2D;
    private GameObject myGM;
    private GameObject canvas;
    private EventMenu eventScript;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        myGM = GameObject.Find("GameManager");
        canvas = GameObject.Find("Canvas");
        eventScript = canvas.GetComponent<EventMenu>();
    }

    void Update()
    {
        float horizontal = 0.0f;
        float vertical = 0.0f;
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && rb2D.position.y <= 3.0)
            vertical = speed;
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && rb2D.position.y >= -3.0)
            vertical = -speed;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            horizontal = -speed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            horizontal = speed;
        Vector2 velocity = new Vector2(horizontal, vertical);
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "fun")
        {
            myGM.GetComponent<ScoreManager>().GiveRange();
            myGM.GetComponent<CollectionData>().addToCollection("intrest", 1);
        }
        if(other.tag == "work")
        {
            myGM.GetComponent<ScoreManager>().AddToGPA(0.05f);
            myGM.GetComponent<CollectionData>().addToCollection("engineering", 1);
        }
        if(other.tag == "sleep")
        {
            myGM.GetComponent<ScoreManager>().GiveHealth();
            myGM.GetComponent<CollectionData>().addToCollection("physical", 1);
        }
        if (other.tag == "event")
        {
            eventScript.Pause();
        }
        
        Destroy(other.gameObject);
    }
}
