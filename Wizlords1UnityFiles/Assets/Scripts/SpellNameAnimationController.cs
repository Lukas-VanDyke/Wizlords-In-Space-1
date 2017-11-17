using System;
using UnityEngine;

public class SpellNameAnimationController : MonoBehaviour {

    public string spriteSheet;

    void LateUpdate()
    {
        UnityEngine.Object[] subSprites = Resources.LoadAll("SpellSelect/" + spriteSheet);

        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            string spriteName = renderer.sprite.name;
            var newSprite = Array.Find(subSprites, item => item.name == spriteName);

            if (newSprite)
                renderer.sprite = (Sprite)newSprite;
        }
    }
}
