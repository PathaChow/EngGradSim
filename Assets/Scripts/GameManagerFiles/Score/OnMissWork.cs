using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMissWork : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("miss"))
        {
            Debug.Log("missed work");
            //GameObject.Find("GameManager").GetComponent<CollectionData>().addMissedWork();
            EventManager.TriggerEvent("addMissedWork");
        }
    }
}
