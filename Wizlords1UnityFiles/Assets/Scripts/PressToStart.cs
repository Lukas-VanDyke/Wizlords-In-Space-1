using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToStart : MonoBehaviour {

    private bool flipped = false;
    private int flipTime = 500;
    private Timer pressFlip;
    private Timer afterPress = null;
    private int transitionTimer = 2000;
    private bool pressed = false;

    void Start()
    {
        pressFlip = new Timer(flipTime);
        SoundManager.soundmanager.changeMusic(1);
    }
	
	// Update is called once per frame
	void Update () {
        if (pressFlip.hasElapsed())
        {
            pressFlip.reset(flipTime);
            if (flipped)
            {
                GetComponent<Renderer>().enabled = true;
                flipped = false;
            }
            else
            {
                GetComponent<Renderer>().enabled = false;
                flipped = true;
            }
        }

        if (Input.anyKeyDown && !pressed)
        {
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.START);
            flipTime = flipTime / 4;
            afterPress = new Timer(transitionTimer);
            pressed = true;
        }

        if (afterPress != null)
        {
            if (afterPress.hasElapsed())
                LoadOnClick.LoadScene(1);
        }
	}
}
