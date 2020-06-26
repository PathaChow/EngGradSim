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

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        myGM = GameObject.Find("GameManager");
    }

    void Update()
    {
        float horizontal = 0.0f;
        float vertical = 0.0f;
        if (Input.GetKey(KeyCode.W))
            vertical = speed;
        if (Input.GetKey(KeyCode.S))
            vertical = -speed;
        if (Input.GetKey(KeyCode.A))
            horizontal = -speed;
        if (Input.GetKey(KeyCode.D))
            horizontal = speed;
        Vector2 velocity = new Vector2(horizontal, vertical);
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "fun")
        {
            myGM.GetComponent<ScoreManager>().GiveRange();
        }
        if(other.tag == "work")
        {
            myGM.GetComponent<ScoreManager>().AddToGPA(0.05f);
        }
        if(other.tag == "sleep")
        {
            myGM.GetComponent<ScoreManager>().GiveHealth();
        }
        
        Destroy(other.gameObject);
    }
}
