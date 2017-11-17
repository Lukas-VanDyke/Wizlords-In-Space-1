using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButton : MonoBehaviour {

    private void OnMouseDown()
    {
        MapManager.map.save();
    }
}
