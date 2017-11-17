using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectDial : MonoBehaviour {

    public int player;
    string left;
    string right;
    string ready;
    Animator anim;
    CharacterSelectManager charman;
    Timer countdown = null;
    bool done = false;

    // Use this for initialization
    void Start() {
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
    void Update() {
        if (countdown == null || countdown.hasElapsed())
        {
            anim.Play("DialIdle");
            charman = CharacterSelectManager.CharManager;
            countdown = null;

            if (Input.GetButtonDown(left) && !done)
            {
                anim.Play("DialLeft");
                charman.leftSelect(player);
                countdown = new Timer(100);
            }
            else if (Input.GetButtonDown(right) && !done)
            {
                anim.Play("DialRight");
                charman.rightSelect(player);
                countdown = new Timer(100);
            }
            else if (Input.GetButtonDown(ready))
            {
                anim.Play("DialReady");
                charman.readyUp(player);
                countdown = new Timer(100);
                if (done)
                    done = false;
                else
                    done = true;
            }
        }
    }
}
