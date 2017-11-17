using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager = null;
    public List<GameObject> players = new List<GameObject>();
    public GameObject Fire;
    private float fireMaxSpeed = 15f;
    public GameObject Ice;
    private float iceMaxSpeed = 5f;
    public GameObject Lightning;
    private float lightningMaxSpeed = 5f;
    public GameObject Wind;
    private float windMaxSpeed = 15f;
    private enum projSpells {FIRE, ICE, LIGHTNING, WIND};
    private enum items {FEATHER, HERB, EYE};
    private Timer itemSpawn;
    private const int itemSpawnTime = 1500;
    public static List<GameObject> spawnLocations = new List<GameObject>();
    public GameObject Feather;
    public GameObject Herb;
    public GameObject Eye;
    public static List<GameObject> player1Display;
    public static List<GameObject> player2Display;
    public static List<GameObject> p1health;
    public static List<GameObject> p2health;
    private System.Random itemGen;
    public GameObject pauseMenu;
    public GameObject resume;
    public GameObject exit;
    public GameObject controls;
    private string pauseGame = "Pause";
    private string exitGame = "Exit";
    private bool paused = false;
    private int itemTimeRemaining = 0;

    // Use this for initialization
    void Start () {
        if (gameManager == null)
            gameManager = this;
        itemSpawn = new Timer(itemSpawnTime);
        itemGen = new System.Random();
        pauseMenu.GetComponent<Renderer>().enabled = false;
        resume.GetComponent<Renderer>().enabled = false;
        exit.GetComponent<Renderer>().enabled = false;
        controls.GetComponent<Renderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(pauseGame))
            PauseGame();
        if (!paused)
            spawnItem();
        else
        {
            if (Input.GetButtonDown(exitGame))
            {
                Persistent.persistent.playEffect((int)Persistent.SoundEffects.EXITGAME);
                Time.timeScale = 1.0f;
                LoadOnClick.LoadScene(0);
            }
        }
	}

    public void spawnItem()
    {
        if (itemSpawn.hasElapsed())
        {
            int randLoc = itemGen.Next(spawnLocations.Count);
            Rigidbody2D newItemLoc = spawnLocations[randLoc].GetComponent<Rigidbody2D>();

            int itemType = itemGen.Next(Enum.GetNames(typeof(items)).Length);
            switch (itemType)
            {
                case (int)items.FEATHER:
                    Instantiate(Feather, newItemLoc.position, Quaternion.identity);
                    break;
                case (int)items.HERB:
                    Instantiate(Herb, newItemLoc.position, Quaternion.identity);
                    break;
                case (int)items.EYE:
                    Instantiate(Eye, newItemLoc.position, Quaternion.identity);
                    break;
            }
            itemSpawn.reset(itemSpawnTime);
        }
    }

    public void spawnProjectile(Vector3 playerPosition, bool playerDirection, int spell)
    {
        Vector3 spawnPos;
        if (playerDirection)
            spawnPos = new Vector3(playerPosition.x - 1.1f, playerPosition.y, 0);
        else
            spawnPos = new Vector3(playerPosition.x + 1.1f, playerPosition.y, 0);

        if (spell == (int)projSpells.ICE)
        {
            spawnPos.y -= 0.7f;
            if (playerDirection)
                spawnPos.x -= 0.5f;
            else
                spawnPos.x += 0.5f;
        }

        if (spell == (int)projSpells.LIGHTNING)
        {
            if (playerDirection)
                spawnPos.x -= 0.5f;
            else
                spawnPos.x += 0.5f;
        }

        float maxSpeed = 10f;
        GameObject projectile;
        switch (spell)
        {
            case (int)projSpells.FIRE:
                projectile = Instantiate(Fire, spawnPos, Quaternion.identity);
                maxSpeed = fireMaxSpeed;
                break;
            case (int)projSpells.ICE:
                projectile = Instantiate(Ice, spawnPos, Quaternion.identity);
                maxSpeed = iceMaxSpeed;
                break;
            case (int)projSpells.LIGHTNING:
                projectile = Instantiate(Lightning, spawnPos, Quaternion.identity);
                maxSpeed = lightningMaxSpeed;
                break;
            case (int)projSpells.WIND:
                projectile = Instantiate(Wind, spawnPos, Quaternion.identity);
                maxSpeed = windMaxSpeed;
                break;
            default:
                projectile = null;
                break;
        }
        Rigidbody2D projbody = projectile.GetComponent<Rigidbody2D>();
        if (playerDirection)
            projbody.velocity = new Vector2((-1) * maxSpeed, projbody.velocity.y);
        else
            projbody.velocity = new Vector2(maxSpeed, projbody.velocity.y);
    }

    public void endGame(int playerNum)
    {
        if (playerNum == 1)
        {
            Persistent.roundsWon[1] += 1;
            if (Persistent.roundsWon[1] == Persistent.rounds)
            {
                Persistent.winner = 2;
                LoadOnClick.LoadScene(7);
            }
            else
            {
                Persistent.currentRound += 1;
                LoadOnClick.LoadScene(5);
            }
        }
        else
        {
            Persistent.roundsWon[0] += 1;
            if (Persistent.roundsWon[0] == Persistent.rounds)
            {
                Persistent.winner = 1;
                LoadOnClick.LoadScene(7);
            }
            else
            {
                Persistent.currentRound += 1;
                LoadOnClick.LoadScene(5);
            }
        }
    }

    private void PauseGame()
    {
        if (paused)
        {
            Time.timeScale = 1.0f;
            pauseMenu.GetComponent<Renderer>().enabled = false;
            resume.GetComponent<Renderer>().enabled = false;
            exit.GetComponent<Renderer>().enabled = false;
            controls.GetComponent<Renderer>().enabled = false;
            paused = false;
            itemSpawn.reset(itemTimeRemaining);
            PlayerController.player1.unPause();
            PlayerController.player2.unPause();
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.CLOSEMENU);
        } else
        {
            Time.timeScale = 0.0f;
            pauseMenu.GetComponent<Renderer>().enabled = true;
            resume.GetComponent<Renderer>().enabled = true;
            exit.GetComponent<Renderer>().enabled = true;
            controls.GetComponent<Renderer>().enabled = true;
            paused = true;
            itemTimeRemaining = itemSpawn.timeRemaining();
            PlayerController.player1.onPause();
            PlayerController.player2.onPause();
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.OPENMENU);
        }
    }
}
