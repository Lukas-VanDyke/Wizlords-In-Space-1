using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBlock : MonoBehaviour {

	public int blockNum;
    private enum blocks { STONE, BOX, LOG, EMPTY, LEFT, RIGHT};

    private void OnMouseDown()
    {
        switch (blockNum)
        {
            case (int)blocks.STONE:
                ReplaceableBlock.blockType = "Stone";
                break;
            case (int)blocks.BOX:
                ReplaceableBlock.blockType = "Box";
                break;
            case (int)blocks.LOG:
                ReplaceableBlock.blockType = "Log";
                break;
            case (int)blocks.EMPTY:
                ReplaceableBlock.blockType = "Empty";
                break;
            case (int)blocks.LEFT:
                ReplaceableBlock.blockType = "Left";
                break;
            case (int)blocks.RIGHT:
                ReplaceableBlock.blockType = "Right";
                break;
        }
    }
}
