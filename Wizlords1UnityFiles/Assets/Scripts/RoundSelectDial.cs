﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSelectDial : MonoBehaviour {

    string left;
    string right;
    string ready;
    Animator anim;
    RoundSelectManager roundman;
    Timer countdown = null;

    // Use this for initialization
    void Start () {
        roundman = RoundSelectManager.roundman;
        left = "a";
        right = "d";
        ready = "w";
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (countdown == null || countdown.hasElapsed())
        {
            anim.Play("DialIdle");
            countdown = null;
            roundman = RoundSelectManager.roundman;

            if (Input.GetButtonDown(left))
            {
                anim.Play("DialLeft");
                roundman.leftSelect();
                countdown = new Timer(100);
            }
            else if (Input.GetButtonDown(right))
            {
                anim.Play("DialRight");
                roundman.rightSelect();
                countdown = new Timer(100);
            }
            else if (Input.GetButtonDown(ready))
            {
                anim.Play("DialReady");
                roundman.readyUp();
                countdown = new Timer(100);
            }
        }
    }
}