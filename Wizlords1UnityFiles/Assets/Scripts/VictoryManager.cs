using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour {
    public GameObject one;
    public GameObject two;
    private Timer wait;
	// Use this for initialization
	void Start () {
        SoundManager.soundmanager.changeMusic(3);
        if (Persistent.winner == 1)
            one.transform.position = new Vector3(one.transform.position.x, one.transform.position.y, 0);
        else
            two.transform.position = new Vector3(two.transform.position.x, one.transform.position.y, 0);

        wait = new Timer(6000);
	}
	
	// Update is called once per frame
	void Update () {
        if (wait.hasElapsed())
        {
            LoadOnClick.LoadScene(0);
        }
	}
}
