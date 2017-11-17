﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPlatform : MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject tempLog;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Fire")
        {
            Vector2 newBlockPos = rb.position;
            Destroy(gameObject);
            Instantiate(tempLog, newBlockPos, Quaternion.identity);
        }
    }
}
