using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPlayer : MonoBehaviour {
    private Animator anim;
    private Timer walking;
    private Rigidbody2D rb;
    private float maxSpeed = 2.5f;
    private float firemaxSpeed = 10f;
    private bool doneWalking = false;
    public GameObject Fire;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        walking = new Timer(2000);
	}
	
	// Update is called once per frame
	void Update () {
		if (!walking.hasElapsed())
        {
            anim.SetBool("Grounded", true);
            anim.SetFloat("Speed", maxSpeed);
            rb.velocity = new Vector2(maxSpeed, 0);
        } else if (walking.hasElapsed() && !doneWalking)
        {
            anim.SetFloat("Speed", 0);
            rb.velocity = new Vector2(0, 0);
            anim.Play("CastSuccess");
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.FIRE);
            Vector3 spawnPos = new Vector3(rb.position.x + 1.1f, rb.position.y, 0);
            Rigidbody2D projbody = Instantiate(Fire, spawnPos, Quaternion.identity).GetComponent<Rigidbody2D>();
            projbody.velocity = new Vector2(firemaxSpeed, projbody.velocity.y);
            doneWalking = true;
        }
	}
}
