using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelectManager : MonoBehaviour {

    public static SpellSelectManager SpellMan;
    public GameObject P1Ready;
    public GameObject P2Ready;
    private bool player1ready = false;
    private bool player2ready = false;
    private enum spells {HERB, FEATHER, EYE};
    private List<string> herb = new List<string>();
    private List<string> feather = new List<string>();
    private List<string> eye = new List<string>();
    private List<List<string>> possibleSpells = new List<List<string>>();
    private int p1currentspell = 0;
    private int p1currentselect = 0;
    private int p1notselect = 1;
    private int p2currentspell = 0;
    private int p2currentselect = 0;
    private int p2notselect = 1;
    public List<GameObject> p1herb = new List<GameObject>();
    public List<GameObject> p1feather = new List<GameObject>();
    public List<GameObject> p1eye = new List<GameObject>();
    public List<GameObject> p2herb = new List<GameObject>();
    public List<GameObject> p2feather = new List<GameObject>();
    public List<GameObject> p2eye = new List<GameObject>();
    private List<string> p1spells = new List<string>();
    private List<string> p2spells = new List<string>();
    private string back = "Back";

    // Use this for initialization
    void Start () {
        if (SpellMan == null)
            SpellMan = this;

        herb.Add("Fire");
        herb.Add("Grass");
        feather.Add("Stone");
        feather.Add("Wind");
        eye.Add("Ice");
        eye.Add("Lightning");

        possibleSpells.Add(herb);
        possibleSpells.Add(feather);
        possibleSpells.Add(eye);

        P1Ready.GetComponent<Renderer>().enabled = false;
        P2Ready.GetComponent<Renderer>().enabled = false;

        p1herb[p1currentselect].GetComponent<Animator>().SetBool("Selected", true);
        p2herb[p1currentselect].GetComponent<Animator>().SetBool("Selected", true);
    }
	
	// Update is called once per frame
	void Update () {
        if (player1ready && player2ready)
        {
            Persistent.p1spells = p1spells;
            Persistent.p2spells = p2spells;
            LoadOnClick.LoadScene(3);
        }
        if (Input.GetButtonDown(back))
        {
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.BACK);
            LoadOnClick.LoadScene(1);
        }
    }

    public void newSelect(int player)
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.SELECT);
        if (player == 1)
        {
            if (p1currentselect == 0)
            {
                p1currentselect = 1;
                p1notselect = 0;
            }
            else
            {
                p1currentselect = 0;
                p1notselect = 1;
            }
            switch (p1currentspell)
            {
                case (int)spells.HERB:
                    p1herb[p1currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    p1herb[p1notselect].GetComponent<Animator>().SetBool("Selected", false);
                    break;
                case (int)spells.FEATHER:
                    p1feather[p1currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    p1feather[p1notselect].GetComponent<Animator>().SetBool("Selected", false);
                    break;
                case (int)spells.EYE:
                    p1eye[p1currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    p1eye[p1notselect].GetComponent<Animator>().SetBool("Selected", false);
                    break;
            }
        }
        else
        {
            if (p2currentselect == 0)
            {
                p2currentselect = 1;
                p2notselect = 0;
            }
            else
            {
                p2currentselect = 0;
                p2notselect = 1;
            }
            switch (p2currentspell)
            {
                case (int)spells.HERB:
                    p2herb[p2currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    p2herb[p2notselect].GetComponent<Animator>().SetBool("Selected", false);
                    break;
                case (int)spells.FEATHER:
                    p2feather[p2currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    p2feather[p2notselect].GetComponent<Animator>().SetBool("Selected", false);
                    break;
                case (int)spells.EYE:
                    p2eye[p2currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    p2eye[p2notselect].GetComponent<Animator>().SetBool("Selected", false);
                    break;
            }
        }
    }

    public void readyUp(int player)
    {
        if (player == 1)
        {
            p1spells.Add(possibleSpells[p1currentspell][p1currentselect]);
            p1currentselect = 0;
            if (p1currentspell == (int)spells.EYE)
            {
                player1ready = true;
                Persistent.persistent.playEffect((int)Persistent.SoundEffects.READY);
                P1Ready.GetComponent<Renderer>().enabled = true;
            } else
                Persistent.persistent.playEffect((int)Persistent.SoundEffects.SELECT);

            p1currentspell += 1;
            switch (p1currentspell)
            {
                case (int)spells.FEATHER:
                    p1feather[p1currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    break;
                case (int)spells.EYE:
                    p1eye[p1currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    break;
            }
        }
        else
        {
            p2spells.Add(possibleSpells[p2currentspell][p2currentselect]);
            p2currentselect = 0;
            if (p2currentspell == (int)spells.EYE)
            {
                Persistent.persistent.playEffect((int)Persistent.SoundEffects.READY);
                player2ready = true;
                P2Ready.GetComponent<Renderer>().enabled = true;
            } else
                Persistent.persistent.playEffect((int)Persistent.SoundEffects.SELECT);

            p2currentspell += 1;
            switch (p2currentspell)
            {
                case (int)spells.FEATHER:
                    p2feather[p2currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    break;
                case (int)spells.EYE:
                    p2eye[p2currentselect].GetComponent<Animator>().SetBool("Selected", true);
                    break;
            }
        }
    }

    public bool playerDone(int player)
    {
        if (player == 1)
        {
            return player1ready;
        } else
        {
            return player2ready;
        }
    }

    public void reset (int player)
    {
        if (player == 1)
        {
            player1ready = false;
            P1Ready.GetComponent<Renderer>().enabled = false;
            p1spells = new List<string>();
            p1currentspell = 0;
            p1currentselect = 0;
            p1notselect = 1;
            p1herb[0].GetComponent<Animator>().SetBool("Selected", true);
            p1herb[1].GetComponent<Animator>().SetBool("Selected", false);
            p1feather[0].GetComponent<Animator>().SetBool("Selected", false);
            p1feather[1].GetComponent<Animator>().SetBool("Selected", false);
            p1eye[0].GetComponent<Animator>().SetBool("Selected", false);
            p1eye[1].GetComponent<Animator>().SetBool("Selected", false);
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.BACK);
        }
        else
        {
            player2ready = false;
            P2Ready.GetComponent<Renderer>().enabled = false;
            p2spells = new List<string>();
            p2currentspell = 0;
            p2currentselect = 0;
            p2notselect = 1;
            p2herb[0].GetComponent<Animator>().SetBool("Selected", true);
            p2herb[1].GetComponent<Animator>().SetBool("Selected", false);
            p2feather[0].GetComponent<Animator>().SetBool("Selected", false);
            p2feather[1].GetComponent<Animator>().SetBool("Selected", false);
            p2eye[0].GetComponent<Animator>().SetBool("Selected", false);
            p2eye[1].GetComponent<Animator>().SetBool("Selected", false);
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.BACK);
        }
    }
}
