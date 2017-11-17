using System;
using UnityEngine;

public class CharSelectAnimator : MonoBehaviour {
    public int player;
    string spriteSheet;
    CharacterSelectManager charman;

    void LateUpdate()
    {
        charman = CharacterSelectManager.CharManager;
        if (player == 1)
            spriteSheet = charman.player1sprite;
        else if (player == 2)
            spriteSheet = charman.player2sprite;
        else
        {
            if (Persistent.winner == 1)
                spriteSheet = Persistent.P1Sprite;
            else if (Persistent.winner == 2)
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
