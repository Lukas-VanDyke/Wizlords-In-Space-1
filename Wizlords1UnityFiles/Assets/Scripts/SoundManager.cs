using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource MenuSource;
    public AudioSource BattleSource;
    public AudioSource VictorySource;
    public static SoundManager soundmanager = null;
    private int currentSong = 0;

    // Use this for initialization
    void Awake()
    {
        if (soundmanager == null)
            soundmanager = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void changeMusic(int music)
    {
        if (music != currentSong)
        {
            BattleSource.Stop();
            MenuSource.Stop();
            VictorySource.Stop();

            currentSong = music;

            if (music == 1)
                MenuSource.Play();
            else if (music == 2)
                BattleSource.Play();
            else
                VictorySource.Play();
        }
    }
}
