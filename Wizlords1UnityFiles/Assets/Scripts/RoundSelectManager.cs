using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSelectManager : MonoBehaviour {

    public static RoundSelectManager roundman = null;
    private List<int> numOfRounds = new List<int>();
    private int currentRound = 0;
    public GameObject displayedRound;
    private SpriteRenderer displayedSprite;
    public List<Sprite> roundSprites;
    private string back = "Back";

    // Use this for initialization
    void Start () {
        if (roundman == null)
            roundman = this;

        numOfRounds.Add(1);
        numOfRounds.Add(2);
        numOfRounds.Add(3);

        displayedSprite = displayedRound.GetComponent<SpriteRenderer>();
        displayedSprite.sprite = roundSprites[currentRound];
    }

    void Update()
    {
        if (Input.GetButtonDown(back))
        {
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.BACK);
            LoadOnClick.LoadScene(3);
        }
    }

    public void readyUp()
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.READY);
        Persistent.rounds = numOfRounds[currentRound];
        LoadOnClick.LoadScene(5);
    }

    public void leftSelect()
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.SELECT);
        currentRound -= 1;
        if (currentRound < 0)
            currentRound = numOfRounds.Count - 1;
        displayedSprite.sprite = roundSprites[currentRound];
    }

    public void rightSelect()
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.SELECT);
        currentRound += 1;
        if (currentRound == numOfRounds.Count)
            currentRound = 0;
        displayedSprite.sprite = roundSprites[currentRound];
    }
}
