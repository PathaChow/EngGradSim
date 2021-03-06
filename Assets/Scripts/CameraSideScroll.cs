﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSideScroll : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;
        float z = gameObject.transform.position.z;
        gameObject.transform.position = new Vector3(x + speed * Time.deltaTime, y, z);
    }
}
