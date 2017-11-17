using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject iceBlock;
    public GameObject fireBlock;
    public GameObject lightningBlock;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Fire")
        {
            Vector2 newBlockPos = rb.position;
            Destroy(gameObject);
            Instantiate(fireBlock, newBlockPos, Quaternion.identity);
        } else if (coll.gameObject.tag == "Ice")
        {
            Vector2 newBlockPos = rb.position;
            Destroy(gameObject);
            Instantiate(iceBlock, newBlockPos, Quaternion.identity);
        }
        else if (coll.gameObject.tag == "Lightning")
        {
            Vector2 newBlockPos = rb.position;
            Destroy(gameObject);
            Instantiate(lightningBlock, newBlockPos, Quaternion.identity);
        }
    }
}
