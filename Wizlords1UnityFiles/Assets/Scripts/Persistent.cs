using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent : MonoBehaviour {
    public static Persistent persistent;
    public static string P1Sprite;
    public static string P2Sprite;
    public static int winner = 0;
    public static int currentRound = 0;
    public static List<int> roundsWon = new List<int>();
    public static string map;
    public static int rounds;
    public static List<string> p1spells;
    public static List<string> p2spells;
    public enum SoundEffects {FIRE, GRASS, STONE, WIND, ICE, LIGHTNING, FAIL, ITEMCOLLECT, JUMP, BACK, START, READY, OPENMENU, CLOSEMENU, EXITGAME, NEWROUND, TAKEDAMAGE, SELECT}

    public List<AudioClip> sounds = new List<AudioClip>();

    private AudioSource source;

    // Use this for initialization
    void Awake()
    {
        if (persistent == null)
            persistent = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(transform.gameObject);
        roundsWon.Add(0);
        roundsWon.Add(0);
        source = GetComponent<AudioSource>();
    }

    public void playEffect(int effect)
    {
        source.PlayOneShot(sounds[effect]);
    }
}
