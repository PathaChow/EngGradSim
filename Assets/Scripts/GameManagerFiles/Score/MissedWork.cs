using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedWork : MonoBehaviour
{
    private void Awake()
    {
        createLeftCollider();
    }

    void createLeftCollider()
    {
        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        // create collider
        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();

        // set up
        col.size = new Vector3(0.1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y)*2f, 0f);
        col.offset = new Vector2(col.size.x / 2f, col.size.y / 2f);

        gameObject.transform.position = new Vector3(((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f) - col.size.x - .2f, bottomLeftScreenPoint.y, 0f);
    }
}
