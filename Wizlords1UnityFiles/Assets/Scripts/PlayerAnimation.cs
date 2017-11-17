using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    string spriteSheet;
	
	void LateUpdate () {
        if (GetComponent<PlayerController>() == PlayerController.player1)
        {
            spriteSheet = Persistent.P1Sprite;
        } else
        {
            spriteSheet = Persistent.P2Sprite;
        }

        UnityEngine.Object[] subSprites = Resources.LoadAll("Characters/" + spriteSheet);

        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            string spriteName = renderer.sprite.name;
            var newSprite = Array.Find(subSprites, item => item.name == spriteName);

            if (newSprite)
                renderer.sprite = (Sprite)newSprite;
        }
    }
}
