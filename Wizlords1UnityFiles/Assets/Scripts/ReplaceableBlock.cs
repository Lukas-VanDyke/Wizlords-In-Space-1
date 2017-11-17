using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceableBlock : MonoBehaviour {

    private Rigidbody2D rb;
    public static string blockType = "Stone";
    public GameObject stoneBlock;
    public GameObject leftSpike;
    public GameObject rightSpike;
    public GameObject woodBox;
    public GameObject log;
    public GameObject empty;
    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        Vector2 blockLoc = rb.position;
        float xPos = blockLoc.x;
        float yPos = blockLoc.y;
        int i = 0;
        int j = 0;
        for (float x = -18.4f; x < xPos; x += 0.8f)
            i++;
        for (float y = -7.2f; y < yPos; y += 0.8f)
            j++;
        Destroy(gameObject);
        if (blockType.Equals("Stone"))
            MapManager.map.inside[i][j] = Instantiate(stoneBlock, blockLoc, Quaternion.identity);
        else if (blockType.Equals("Box"))
            MapManager.map.inside[i][j] = Instantiate(woodBox, blockLoc, Quaternion.identity);
        else if (blockType.Equals("Log"))
            MapManager.map.inside[i][j] = Instantiate(log, blockLoc, Quaternion.identity);
        else if (blockType.Equals("Left"))
            MapManager.map.inside[i][j] = Instantiate(leftSpike, blockLoc, Quaternion.identity);
        else if (blockType.Equals("Right"))
            MapManager.map.inside[i][j] = Instantiate(rightSpike, blockLoc, Quaternion.identity);
        else
            MapManager.map.inside[i][j] = Instantiate(empty, blockLoc, Quaternion.identity);
    }
}
