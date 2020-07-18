using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMissedExam : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("miss"))
        {
            Debug.Log("missed exam");
            //GameObject.Find("GameManager").GetComponent<CollectionData>().addMissedWork();
            EventManager.TriggerEvent("MissedExam");
        }
    }
}
