using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireSpell : MonoBehaviour {

    private float maxSpeed = 15f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update() {
        if (rb.velocity.x < 0)
        {
            sprite.flipX = true;
            rb.velocity = new Vector2((-1) * maxSpeed, rb.velocity.y);
        } else
        {
            sprite.flipX = false;
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "LavaFloor" || coll.gameObject.tag == "IceFloor" || coll.gameObject.tag == "LightningFloor")
            Destroy(gameObject);
    }
}
