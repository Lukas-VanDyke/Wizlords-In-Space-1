using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempStoneBlock : MonoBehaviour {
    private Rigidbody2D rb;
    public GameObject stoneBlock;
    private Timer countdown = new Timer(5000);

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		if (countdown.hasElapsed())
        {
            Vector2 newBlockPos = rb.position;
            Destroy(gameObject);
            Instantiate(stoneBlock, newBlockPos, Quaternion.identity);
        }
	}
}
