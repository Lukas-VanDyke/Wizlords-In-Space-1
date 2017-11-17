using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterSelectManager : MonoBehaviour {
    public static CharacterSelectManager CharManager = null;
    private List<string> sprites = new List<string>();
    public string player1sprite;
    public string player2sprite;
    private int player1spriteloc;
    private int player2spriteloc;
    private bool player1ready = false;
    private bool player2ready = false;
    public GameObject P1Ready;
    public GameObject P2Ready;
    private string back = "Back";

	// Use this for initialization
	void Start () {
        if (CharManager == null)
            CharManager = this;

        Persistent.winner = 0;
        Persistent.roundsWon[0] = 0;
        Persistent.roundsWon[1] = 0;

        string path = "Assets/Resources/Characters/";
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".png")
            {
                sprites.Add(file.Name.Replace(".png", ""));
            }
        }
        player1spriteloc = 0;
        player2spriteloc = 1;

        player1sprite = sprites[player1spriteloc];
        player2sprite = sprites[player2spriteloc];

        P1Ready.GetComponent<Renderer>().enabled = false;
        P2Ready.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        if (player1ready && player2ready)
        {
            Persistent.P1Sprite = player1sprite.Replace(".png", "");
            Persistent.P2Sprite = player2sprite.Replace(".png", "");
            LoadOnClick.LoadScene(2);
        }
        if (Input.GetButtonDown(back))
        {
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.BACK);
            LoadOnClick.LoadScene(0);
        }
    }

    public void leftSelect(int playerNum)
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.SELECT);
        if (playerNum == 1)
        {
            player1spriteloc -= 1;
            if (player1spriteloc < 0)
                player1spriteloc = sprites.Count - 1;
            if (player1spriteloc == player2spriteloc)
                player1spriteloc -= 1;
            if (player1spriteloc < 0)
                player1spriteloc = sprites.Count - 1;
            player1sprite = sprites[player1spriteloc];
        } else
        {
            player2spriteloc -= 1;
            if (player2spriteloc < 0)
                player2spriteloc = sprites.Count - 1;
            if (player1spriteloc == player2spriteloc)
                player2spriteloc -= 1;
            if (player2spriteloc < 0)
                player2spriteloc = sprites.Count - 1;
            player2sprite = sprites[player2spriteloc];
        }
    }

    public void rightSelect(int playerNum)
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.SELECT);
        if (playerNum == 1)
        {
            player1spriteloc += 1;
            if (player1spriteloc == sprites.Count)
                player1spriteloc = 0;
            if (player1spriteloc == player2spriteloc)
                player1spriteloc += 1;
            if (player1spriteloc == sprites.Count)
                player1spriteloc = 0;
            player1sprite = sprites[player1spriteloc];
        }
        else
        {
            player2spriteloc += 1;
            if (player2spriteloc == sprites.Count)
                player2spriteloc = 0;
            if (player1spriteloc == player2spriteloc)
                player2spriteloc += 1;
            if (player2spriteloc == sprites.Count)
                player2spriteloc = 0;
            player2sprite = sprites[player2spriteloc];
        }
    }

    public void readyUp(int playerNum)
    {
        if (playerNum == 1)
        {
            if (player1ready)
            {
                player1ready = false;
                Persistent.persistent.playEffect((int)Persistent.SoundEffects.BACK);
                P1Ready.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                player1ready = true;
                Persistent.persistent.playEffect((int)Persistent.SoundEffects.READY);
                P1Ready.GetComponent<Renderer>().enabled = true;
            }
        }
        else
        {
            if (player2ready)
            {
                player2ready = false;
                Persistent.persistent.playEffect((int)Persistent.SoundEffects.BACK);
                P2Ready.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                player2ready = true;
                Persistent.persistent.playEffect((int)Persistent.SoundEffects.READY);
                P2Ready.GetComponent<Renderer>().enabled = true;
            }
        }
    }
}
