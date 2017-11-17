using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlatform : MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject tempBox;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Fire" || coll.gameObject.tag == "Ice")
        {
            Vector2 newBlockPos = rb.position;
            Destroy(gameObject);
            Instantiate(tempBox, newBlockPos, Quaternion.identity);
        }
    }
}
