using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArenaLoader : MonoBehaviour {
    public GameObject stoneBlock;
    public GameObject leftSpike;
    public GameObject rightSpike;
    public GameObject woodBox;
    public GameObject log;
    public GameObject itemSpawn;
    public GameObject player;
    public List<GameObject> ItemSpawnLocs = new List<GameObject>();
    private List<GameObject> PlayerSpawnLocs = new List<GameObject>();
    private enum blocks { EMPTY, STONE, BOX, LOG, LEFT, RIGHT};

    // Use this for initialization
    void Start () {
        spawnOutside();
        spawnInside();
        GameManager.spawnLocations = ItemSpawnLocs;
        spawnPlayer();
	}

    private void spawnOutside()
    {
        float x;
        float y = -8;
        Vector2 blockLoc;
        for (x = -20; x <= 20; x += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(stoneBlock, blockLoc, Quaternion.identity);
        }

        x = -20;
        for (y = -7.2f; y <= 8; y += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(stoneBlock, blockLoc, Quaternion.identity);
        }

        y = 8;
        for (x = -20; x <= 20; x += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(stoneBlock, blockLoc, Quaternion.identity);
        }

        x = 20;
        for (y = -7.2f; y <= 8; y += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(stoneBlock, blockLoc, Quaternion.identity);
        }

        x = -19.2f;
        for (y = -7.2f; y <= 8; y += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(leftSpike, blockLoc, Quaternion.identity);
        }

        x = 19.2f;
        for (y = -7.2f; y <= 8; y += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(rightSpike, blockLoc, Quaternion.identity);
        }
    }

    private void spawnInside()
    {
        System.IO.StreamReader file = new System.IO.StreamReader("Assets/Maps/" + Persistent.map);
        string column = file.ReadLine();
        char block;
        float x = -18.4f;
        float y;
        Vector2 blockLoc;
        while (column != null)
        {
            y = -7.2f;
            for (int i = 0; i < column.Length; i++)
            {
                block = column[i];
                blockLoc = new Vector2(x, y);
                switch (block)
                {
                    case '0':
                        ItemSpawnLocs.Add(Instantiate(itemSpawn, blockLoc, Quaternion.identity));
                        break;
                    case '1':
                        Instantiate(stoneBlock, blockLoc, Quaternion.identity);
                        break;
                    case '2':
                        Instantiate(woodBox, blockLoc, Quaternion.identity);
                        break;
                    case '3':
                        Instantiate(log, blockLoc, Quaternion.identity);
                        break;
                    case '4':
                        Instantiate(leftSpike, blockLoc, Quaternion.identity);
                        break;
                    case '5':
                        Instantiate(rightSpike, blockLoc, Quaternion.identity);
                        break;
                    case '6':
                        PlayerSpawnLocs.Add(Instantiate(itemSpawn, blockLoc, Quaternion.identity));
                        break;
                }
                y += 0.8f;
            }
            x += 0.8f;
            column = file.ReadLine();
        }
        file.Close();
    }

    private void spawnPlayer()
    {
        Vector2 playerLoc = PlayerSpawnLocs[0].GetComponent<Rigidbody2D>().position;
        Instantiate(player, playerLoc, Quaternion.identity);

        playerLoc = PlayerSpawnLocs[1].GetComponent<Rigidbody2D>().position;
        Instantiate(player, playerLoc, Quaternion.identity);

        List<GameObject> player1DislayLocs = new List<GameObject>();
        List<GameObject> player2DislayLocs = new List<GameObject>();
        Vector2 itemLoc;
        float x = -16.5f;
        float y = -9f;

        for (int i = 0; i < 5; i++)
        {
            itemLoc = new Vector2(x, y);
            player1DislayLocs.Add(Instantiate(itemSpawn, itemLoc, Quaternion.identity));
            x += 1f;
        }
        GameManager.player1Display = player1DislayLocs;

        player1DislayLocs = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            itemLoc = new Vector2(x, y);
            player1DislayLocs.Add(Instantiate(itemSpawn, itemLoc, Quaternion.identity));
            x +=0.5f;
        }
        GameManager.p1health = player1DislayLocs;

        x = 13.5f;
        for (int i = 0; i < 5; i++)
        {
            itemLoc = new Vector2(x, y);
            player2DislayLocs.Add(Instantiate(itemSpawn, itemLoc, Quaternion.identity));
            x += 1f;
        }
        GameManager.player2Display = player2DislayLocs;

        player2DislayLocs = new List<GameObject>();
        for (int i = 0; i < 3; i++)
        {
            itemLoc = new Vector2(x, y);
            player2DislayLocs.Add(Instantiate(itemSpawn, itemLoc, Quaternion.identity));
            x += 0.5f;
        }
        GameManager.p2health = player2DislayLocs;
    }
}
