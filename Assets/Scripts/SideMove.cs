using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class SideMove : MonoBehaviour
{
    public float speed;
    public bool dontDestroy;

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = SpeedManager.speed;

        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;
        gameObject.transform.position = new Vector3(x + speed * Time.deltaTime, y, z);

        if(x < -12f && !dontDestroy)
        {
            Destroy(gameObject);
        }
    }
}
