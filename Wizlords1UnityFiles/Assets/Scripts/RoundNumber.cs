using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundNumber : MonoBehaviour {
    public List<GameObject> numbers = new List<GameObject>();
    private Timer countdown = new Timer(3000);
    public GameObject startCountdown;

    // Use this for initialization
    void Start () {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Instantiate(numbers[Persistent.currentRound - 1], rb.position, Quaternion.identity);
        startCountdown.GetComponent<Animator>().Play("Countdown");
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.NEWROUND);
        SoundManager.soundmanager.changeMusic(2);
    }

    void Update()
    {
        if (countdown.hasElapsed())
        {
            LoadOnClick.LoadScene(6);
        }
    }
}
