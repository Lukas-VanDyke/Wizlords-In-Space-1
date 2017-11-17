using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelectDial : MonoBehaviour {

    public int player;
    string left;
    string right;
    string ready;
    Animator anim;
    SpellSelectManager spellman;
    Timer countdown = null;
    bool done = false;

    // Use this for initialization
    void Start () {
        if (player == 1)
        {
            left = "a";
            right = "d";
            ready = "w";
        }
        else if (player == 2)
        {
            left = "left";
            right = "right";
            ready = "up";
        }
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (countdown == null || countdown.hasElapsed())
        {
            anim.Play("DialIdle");
            spellman = SpellSelectManager.SpellMan;
            countdown = null;

            if (Input.GetButtonDown(left) && !done)
            {
                anim.Play("DialLeft");
                spellman.newSelect(player);
                countdown = new Timer(100);
            }
            else if (Input.GetButtonDown(right) && !done)
            {
                anim.Play("DialRight");
                spellman.newSelect(player);
                countdown = new Timer(100);
            }
            else if (Input.GetButtonDown(ready))
            {
                anim.Play("DialReady");
                countdown = new Timer(100);
                if (done)
                {
                    done = false;
                    spellman.reset(player);
                } else
                    spellman.readyUp(player);

                if (spellman.playerDone(player))
                {
                    done = true;
                }
            }
        }
    }
}
