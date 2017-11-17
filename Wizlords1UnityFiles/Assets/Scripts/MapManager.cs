using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public GameObject stoneBlockUnmoveable;
    public GameObject stoneBlock;
    public GameObject leftSpikeUnmoveable;
    public GameObject rightSpikeUnmoveable;
    public GameObject leftSpike;
    public GameObject rightSpike;
    public GameObject woodBox;
    public GameObject log;
    public GameObject empty;
    public static MapManager map = null;
    public List<List<GameObject>> inside = new List<List<GameObject>>();

	// Use this for initialization
	void Start () {
        if (map == null)
            map = this;

        float x;
        float y = -8;
        Vector2 blockLoc;
        for (x = -20; x <= 20; x += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(stoneBlockUnmoveable, blockLoc, Quaternion.identity);
        }

        x = -20;
        for (y = -7.2f; y <= 8; y += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(stoneBlockUnmoveable, blockLoc, Quaternion.identity);
        }

        y = 8;
        for (x = -20; x <= 20; x += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(stoneBlockUnmoveable, blockLoc, Quaternion.identity);
        }

        x = 20;
        for (y = -7.2f; y <= 8; y += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(stoneBlockUnmoveable, blockLoc, Quaternion.identity);
        }

        x = -19.2f;
        for(y = -7.2f; y <= 8; y += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(leftSpikeUnmoveable, blockLoc, Quaternion.identity);
        }

        x = 19.2f;
        for (y = -7.2f; y <= 8; y += 0.8f)
        {
            blockLoc = new Vector2(x, y);
            Instantiate(rightSpikeUnmoveable, blockLoc, Quaternion.identity);
        }

        List<GameObject> col;
        for (x = -18.4f; x < 18.4; x += 0.8f)
        {
            col = new List<GameObject>();
            for (y = -7.2f; y <= 8; y += 0.8f)
            {
                blockLoc = new Vector2(x, y);
                col.Add(Instantiate(empty, blockLoc, Quaternion.identity));
            }
            inside.Add(col);
        }
    }
	
	public void save()
    {
        System.IO.StreamWriter file = new System.IO.StreamWriter("Assets/Maps/Map1.txt");
        List<GameObject> col;
        string column;

        for (int i = 0; i < inside.Count; i++)
        {
            col = inside[i];
            column = "";
            for (int j = 0; j < col.Count; j++)
            {
                GameObject block = col[j];
                string tag = block.gameObject.tag;
                switch (tag)
                {
                    case "Stone":
                        column += "1";
                        break;
                    case "Box":
                        column += "2";
                        break;
                    case "Log":
                        column += "3";
                        break;
                    case "LeftSpike":
                        column += "4";
                        break;
                    case "RightSpike":
                        column += "5";
                        break;
                    case "Empty":
                        column += "0";
                        break;
                }
            }
            column += "\n";
            file.Write(column);
        }
        file.Close();
        Debug.Log("File Saved");
    }
}
