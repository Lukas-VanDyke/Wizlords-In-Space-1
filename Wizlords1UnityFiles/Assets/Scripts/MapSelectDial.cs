using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectDial : MonoBehaviour {

    string left;
    string right;
    string ready;
    Animator anim;
    MapSelectManager mapman;
    Timer countdown = null;

    // Use this for initialization
    void Start () {
        mapman = MapSelectManager.mapman;
        left = "a";
        right = "d";
        ready = "w";
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (countdown == null || countdown.hasElapsed()){
            anim.Play("DialIdle");
            countdown = null;
            mapman = MapSelectManager.mapman;

            if (Input.GetButtonDown(left))
            {
                anim.Play("DialLeft");
                mapman.leftSelect();
                countdown = new Timer(100);
            }
            else if (Input.GetButtonDown(right))
            {
                anim.Play("DialRight");
                mapman.rightSelect();
                countdown = new Timer(100);
            }
            else if (Input.GetButtonDown(ready))
            {
                anim.Play("DialReady");
                mapman.readyUp();
                countdown = new Timer(100);
            }
        }
    }
}
