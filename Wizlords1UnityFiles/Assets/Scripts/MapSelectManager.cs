using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapSelectManager : MonoBehaviour {

    public static MapSelectManager mapman = null;
    private List<string> maps = new List<string>();
    private int currentMap = 0;
    public GameObject displayedMap;
    private SpriteRenderer displayedSprite;
    public List<Sprite> mapSprites;
    private string back = "Back";

    // Use this for initialization
    void Start () {
        if (mapman == null)
            mapman = this;
        string path = "Assets/Maps/";
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            if (file.Extension == ".txt")
                maps.Add(file.Name);
        }
        displayedSprite = displayedMap.GetComponent<SpriteRenderer>();
        displayedSprite.sprite = mapSprites[currentMap];
    }

    void Update()
    {
        if (Input.GetButtonDown(back))
        {
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.BACK);
            LoadOnClick.LoadScene(2);
        }
    }
	
    public void readyUp()
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.READY);
        Persistent.currentRound = 1;
        Persistent.map = maps[currentMap];
        LoadOnClick.LoadScene(4);
    }

    public void leftSelect()
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.SELECT);
        currentMap -= 1;
        if (currentMap < 0)
            currentMap = maps.Count - 1;
        displayedSprite.sprite = mapSprites[currentMap];
    }

    public void rightSelect()
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.SELECT);
        currentMap += 1;
        if (currentMap == maps.Count)
            currentMap = 0;
        displayedSprite.sprite = mapSprites[currentMap];
    }
}
