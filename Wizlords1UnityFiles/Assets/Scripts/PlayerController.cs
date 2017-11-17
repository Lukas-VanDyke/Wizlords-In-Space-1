using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    public static PlayerController player1 = null;
    public static PlayerController player2 = null;
    private float maxSpeed = 7.5f;
    private float jumpForce = 800f;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    public int playerNumber;
    private string Horizontal;
    private string Jump;
    private string Cast;
    private const int BURNED = 3000;
    private int burnedFlip = 0;
    private const int ICED = 5000;
    private const int STONED = 5000;
    private const int SPIKED = 150;
    private const int PROTECT = 30000;
    private const int SHOCKED = 4500;
    private const int shockFlip = 1000;
    private const int CASTINGSUCCESS = 1000;
    private const int CASTINGFAIL = 2000;
    private List<Timer> cooldowns = new List<Timer>();
    private List<string> Status = new List<string>();
    private List<int> timeRemaining = new List<int>();
    private enum effects { FIRE, STONE, ICE, SPIKE, STONEPROTECTION, CAST, SHOCKED };
    private enum projSpells { FIRE, ICE, LIGHTNING, WIND};
    private int invSize = 5;
    private List<string> items;
    private int nextInvSlot = 0;
    public List<GameObject> invLocs = new List<GameObject>();
    public List<GameObject> displayedInv;
    public GameObject Herb;
    public GameObject Feather;
    public GameObject Eye;
    private enum damageAmounts { FIRE, ICE, SPIKES, LIGHTNING };
    private int health = 50;
    private const int fireDamage = 10;
    private const int iceDamage = 2;
    private const int spikeDamage = 1;
    private const int lightningDamage = 5;
    public List<GameObject> numbers = new List<GameObject>();
    private List<GameObject> healthDisplayLocs;
    private List<GameObject> displayedHealth = new List<GameObject>();
    private List<string> selectedSpells;
    public GameObject grass;

    // Use this for initialization
    void Start() {
        if (player1 == null)
        {
            player1 = this;
            playerNumber = 1;
            invLocs = GameManager.player1Display;
            healthDisplayLocs = GameManager.p1health;
            selectedSpells = Persistent.p1spells;
        }
        else
        {
            player2 = this;
            playerNumber = 2;
            invLocs = GameManager.player2Display;
            healthDisplayLocs = GameManager.p2health;
            selectedSpells = Persistent.p2spells;
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        switch (playerNumber)
        {
            case 1:
                Horizontal = "Horizontal";
                Jump = "Jump";
                Cast = "Cast";
                break;
            case 2:
                Horizontal = "Horizontal2";
                Jump = "Jump2";
                Cast = "Cast2";
                break;
        }
        items = new List<string>(invSize);
        displayedInv = new List<GameObject>(invSize);
        for (int i = 0; i < invSize; i++)
        {
            items.Add("Empty");
            displayedInv.Add(null);
        }
        for (int i = 0; i < Enum.GetNames(typeof(effects)).Length; i++) {
            cooldowns.Add(null);
            Status.Add("Nothing");
            timeRemaining.Add(0);
        }
        Vector2 displayLoc = healthDisplayLocs[0].GetComponent<Rigidbody2D>().position;
        displayedHealth.Add(Instantiate(numbers[1], displayLoc, Quaternion.identity));

        displayLoc = healthDisplayLocs[1].GetComponent<Rigidbody2D>().position;
        displayedHealth.Add(Instantiate(numbers[0], displayLoc, Quaternion.identity));

        displayLoc = healthDisplayLocs[2].GetComponent<Rigidbody2D>().position;
        displayedHealth.Add(Instantiate(numbers[0], displayLoc, Quaternion.identity));

        DisplayHealth();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (health == 0)
            GameManager.gameManager.endGame(playerNumber);
        //Player not locked
        if (!(anim.GetBool("Locked")))
            playerNotLocked();

        //Player on fire
        if (Status[(int)effects.FIRE].Equals("Fire"))
            fireEffect();

        if (Status[(int)effects.CAST].Equals("Cast"))
            CastCooldown();

        //Player stoned
        if (Status[(int)effects.STONE].Equals("Stone"))
            stoneEffect();

        //Player iced
        if (Status[(int)effects.ICE].Equals("Ice"))
            iceEffect();

        if (Status[(int)effects.SPIKE].Contains("Spike"))
            Spiked();

        if (Status[(int)effects.STONEPROTECTION].Equals("Protected"))
            Protected();

        if (Status[(int)effects.SHOCKED].Equals("Shock"))
            Shocked();
    }

    private void playerNotLocked()
    {
        //Horizontal movement
        float move = Input.GetAxis(Horizontal);
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);
        float velocity = rb.velocity.x;
        anim.SetFloat("Speed", Mathf.Abs(velocity));
        if (velocity > 0)
            sprite.flipX = false;
        else if (velocity == 0) { } // don't do anything 
        else
            sprite.flipX = true;

        //Vertical movement
        velocity = rb.velocity.y;
        anim.SetFloat("VertSpeed", velocity);
        if (Input.GetButtonDown(Jump) && anim.GetBool("Grounded"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpForce));
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.JUMP);
            anim.SetBool("Grounded", false);
        }

        //Set grounded if falling
        if (rb.velocity.y < -1)
            anim.SetBool("Grounded", false);

        //Casting
        if (Input.GetButtonDown(Cast))
        {
            castSpell();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Player")
            anim.SetBool("Grounded", true);
        else if (coll.gameObject.tag == "Fire" || coll.gameObject.tag == "LavaFloor")
        {
            if (Status[(int)effects.STONE] != "Stone")
            {
                AddEffect((int)effects.FIRE);
                takeDamage((int)damageAmounts.FIRE);
            }
        }
        else if (coll.gameObject.tag == "Ice" || coll.gameObject.tag == "IceFloor")
        {
            if (Status[(int)effects.STONE] != "Stone")
            {
                AddEffect((int)effects.ICE);
                takeDamage((int)damageAmounts.ICE);
            }
        }
        else if (coll.gameObject.tag == "Wind")
        {
            if (Status[(int)effects.STONE] != "Stone")
                clearInventory();
        }
        else if (coll.gameObject.tag == "Lightning" || coll.gameObject.tag == "LightningFloor")
        {
            if (Status[(int)effects.STONE] != "Stone")
            {
                AddEffect((int)effects.SHOCKED);
                anim.Play("Shocked");
            }
        }
        else if (coll.gameObject.tag == "Feather" || coll.gameObject.tag == "Eye" || coll.gameObject.tag == "Herb")
        {
            AddItem(coll.gameObject.tag);
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.ITEMCOLLECT);
        }
        else if (coll.gameObject.tag.Contains("Spike"))
        {
            if (Status[(int)effects.STONE] != "Stone")
            {
                takeDamage((int)damageAmounts.SPIKES);
                if (coll.gameObject.tag == "LeftSpike")
                {
                    AddEffect((int)effects.SPIKE);
                    Status[(int)effects.SPIKE] = "LeftSpike";
                }
                if (coll.gameObject.tag == "RightSpike")
                {
                    AddEffect((int)effects.SPIKE);
                    Status[(int)effects.SPIKE] = "RightSpike";
                }
            }
        }
    }

    private void CastCooldown()
    {
        if (cooldowns[(int)effects.CAST].hasElapsed())
        {
            Status[(int)effects.CAST] = "Nothing";
            cooldowns[(int)effects.CAST] = null;
            anim.SetBool("Locked", false);
        }
    }

    private void Shocked()
    {
        if (cooldowns[(int)effects.SHOCKED].hasElapsed())
        {
            Status[(int)effects.SHOCKED] = "Nothing";
            cooldowns[(int)effects.SHOCKED] = null;
        }
        else if (cooldowns[(int)effects.SHOCKED].hasElapsed(shockFlip))
        {
            anim.Play("Shocked");
            takeDamage((int)damageAmounts.LIGHTNING);
            int remaining = cooldowns[(int)effects.SHOCKED].timeRemaining();
            cooldowns[(int)effects.SHOCKED].reset(remaining);
        }
    }

    public void AddEffect(int effect)
    {
        switch (effect)
        {
            case (int)effects.FIRE:
                Status[(int)effects.FIRE] = "Fire";
                cooldowns[(int)effects.FIRE] = new Timer(BURNED);
                anim.SetBool("Locked", true);
                anim.SetBool("Fire", true);
                break;
            case (int)effects.STONE:
                Status[(int)effects.STONE] = "Stone";
                cooldowns[(int)effects.STONE] = new Timer(STONED);
                anim.SetBool("Locked", true);
                anim.SetBool("Stone", true);
                break;
            case (int)effects.ICE:
                Status[(int)effects.ICE] = "Ice";
                cooldowns[(int)effects.ICE] = new Timer(ICED);
                anim.SetBool("Ice", true);
                jumpForce = 400f;
                break;
            case (int)effects.SPIKE:
                cooldowns[(int)effects.SPIKE] = new Timer(SPIKED);
                anim.SetBool("Locked", true);
                break;
            case (int)effects.STONEPROTECTION:
                cooldowns[(int)effects.STONEPROTECTION] = new Timer(PROTECT);
                Status[(int)effects.STONEPROTECTION] = "Protected";
                break;
            case (int)effects.SHOCKED:
                cooldowns[(int)effects.SHOCKED] = new Timer(SHOCKED);
                Status[(int)effects.SHOCKED] = "Shock";
                break;
        }
    }

    private void Protected()
    {
        if (cooldowns[(int)effects.STONEPROTECTION].hasElapsed())
        {
            cooldowns[(int)effects.STONEPROTECTION] = null;
            Status[(int)effects.STONEPROTECTION] = "Nothing";
        }
    }

    private void fireEffect()
    {
        if (!cooldowns[(int)effects.FIRE].hasElapsed())
        {
            anim.SetBool("Locked", true);
            anim.Play("OnFire");
            if (cooldowns[(int)effects.FIRE].hasElapsed(burnedFlip) && sprite.flipX)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
                sprite.flipX = false;
                burnedFlip += 500;
            }
            else if (cooldowns[(int)effects.FIRE].hasElapsed(burnedFlip) && !sprite.flipX)
            {
                rb.velocity = new Vector2((-1) * maxSpeed, rb.velocity.y);
                sprite.flipX = true;
                burnedFlip += 500;
            }
        }
        else
        {
            Status[(int)effects.FIRE] = "Nothing";
            cooldowns[(int)effects.FIRE] = null;
            burnedFlip = 0;
            anim.SetBool("Locked", false);
            anim.SetBool("Fire", false);
        }
    }

    private void stoneEffect()
    {
        if (!cooldowns[(int)effects.STONE].hasElapsed())
        {
            anim.Play("Stoned");
            anim.SetBool("Locked", true);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            Status[(int)effects.STONE] = "Nothing";
            cooldowns[(int)effects.STONE] = null;
            anim.SetBool("Locked", false);
            anim.SetBool("Stone", false);
            AddEffect((int)effects.STONEPROTECTION);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void iceEffect()
    {
        if (!cooldowns[(int)effects.ICE].hasElapsed())
        {
            anim.Play("OnIce");
            if (sprite.flipX)
            {
                rb.velocity = new Vector2((-1) * maxSpeed / 2, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(maxSpeed / 2, rb.velocity.y);
            }
        }
        else
        {
            Status[(int)effects.ICE] = "Nothing";
            cooldowns[(int)effects.ICE] = null;
            anim.SetBool("Ice", false);
            jumpForce = 800f;
        }
    }

    public void Spiked()
    {
        if (!cooldowns[(int)effects.SPIKE].hasElapsed())
        {
            anim.SetBool("Locked", true);
            if (Status[(int)effects.SPIKE] == "LeftSpike")
                rb.AddForce(new Vector2(150f, 0f));
            else
                rb.AddForce(new Vector2(-150f, 0f));
        }
        else
        {
            Status[(int)effects.SPIKE] = "Nothing";
            anim.SetBool("Locked", false);
            cooldowns[(int)effects.SPIKE] = null;
        }
    }

    public void castSpell()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        bool success = false;
        List<int> itemCounts = new List<int>();
        for (int i = 0; i < 3; i++)
            itemCounts.Add(0);

        string item;
        for (int i = 0; i < items.Count; i++)
        {
            item = items[i];
            if (item == "Feather")
                itemCounts[0] += 1;
            else if (item == "Eye")
                itemCounts[1] += 1;
            else if (item == "Herb")
                itemCounts[2] += 1;
        }

        for (int i = 0; i < 3; i++)
        {
            if (itemCounts[i] > 2)
                success = true;
        }

        if (success)
        {
            anim.Play("CastSuccess");
            Status[(int)effects.CAST] = "Cast";
            cooldowns[(int)effects.CAST] = new Timer(CASTINGSUCCESS);
            anim.SetBool("Locked", true);
            if (itemCounts[0] > 2)
            {
                if (selectedSpells[1] == "Stone")
                {
                    AddEffect((int)effects.STONE);
                    AddEffect((int)effects.STONEPROTECTION);
                    Persistent.persistent.playEffect((int)Persistent.SoundEffects.STONE);
                    removeEffects();
                }
                else
                {
                    Vector3 playerPos = rb.position;
                    GameManager.gameManager.spawnProjectile(playerPos, sprite.flipX, (int)projSpells.WIND);
                    Persistent.persistent.playEffect((int)Persistent.SoundEffects.WIND);
                }
                itemCounts[0] -= 3;
            } else if (itemCounts[1] > 2)
            {
                Vector3 playerPos = rb.position;
                if (selectedSpells[2] == "Ice")
                {
                    GameManager.gameManager.spawnProjectile(playerPos, sprite.flipX, (int)projSpells.ICE);
                    Persistent.persistent.playEffect((int)Persistent.SoundEffects.ICE);
                }
                else
                {
                    GameManager.gameManager.spawnProjectile(playerPos, sprite.flipX, (int)projSpells.LIGHTNING);
                    Persistent.persistent.playEffect((int)Persistent.SoundEffects.LIGHTNING);
                }
                itemCounts[1] -= 3;
            } else if (itemCounts[2] > 2)
            {
                Vector3 playerPos = rb.position;
                if (selectedSpells[0] == "Fire")
                {
                    GameManager.gameManager.spawnProjectile(playerPos, sprite.flipX, (int)projSpells.FIRE);
                    Persistent.persistent.playEffect((int)Persistent.SoundEffects.FIRE);
                }
                else
                {
                    Instantiate(grass, playerPos, Quaternion.identity);
                    GrassHeal();
                    Persistent.persistent.playEffect((int)Persistent.SoundEffects.GRASS);
                }
                itemCounts[2] -= 3;
            }
            clearInventory();
            for (int i = 0; i < itemCounts[0]; i++)
                AddItem("Feather");
            for (int i = 0; i < itemCounts[1]; i++)
                AddItem("Eye");
            for (int i = 0; i < itemCounts[2]; i++)
                AddItem("Herb");
        }
        else
        {
            anim.Play("CastFail");
            Persistent.persistent.playEffect((int)Persistent.SoundEffects.FAIL);
            Status[(int)effects.CAST] = "Cast";
            cooldowns[(int)effects.CAST] = new Timer(CASTINGFAIL);
            anim.SetBool("Locked", true);
            clearInventory();
        }
    }

    private void clearInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i] = "Empty";
            if (displayedInv[i] != null)
            {
                Destroy(displayedInv[i]);
                displayedInv[i] = null;
            }
        }
        nextInvSlot = 0;
    }

    public void AddItem(string tag)
    {
        items[nextInvSlot] = tag;
        Rigidbody2D displayLoc = invLocs[nextInvSlot].GetComponent<Rigidbody2D>();
        if (displayedInv[nextInvSlot] != null)
        {
            Destroy(displayedInv[nextInvSlot]);
            displayedInv[nextInvSlot] = null;
        }
        if (tag == "Feather")
            displayedInv[nextInvSlot] = Instantiate(Feather, displayLoc.position, Quaternion.identity);
        else if (tag == "Eye")
            displayedInv[nextInvSlot] = Instantiate(Eye, displayLoc.position, Quaternion.identity);
        else if (tag == "Herb")
            displayedInv[nextInvSlot] = Instantiate(Herb, displayLoc.position, Quaternion.identity);
        nextInvSlot = (nextInvSlot + 1) % items.Count;
    }

    public void takeDamage(int damage)
    {
        Persistent.persistent.playEffect((int)Persistent.SoundEffects.TAKEDAMAGE);
        double damageTaken = 0;
        switch (damage)
        {
            case (int)damageAmounts.FIRE:
                damageTaken = fireDamage;
                break;
            case (int)damageAmounts.ICE:
                damageTaken = iceDamage;
                break;
            case (int)damageAmounts.SPIKES:
                damageTaken = spikeDamage;
                break;
            case (int)damageAmounts.LIGHTNING:
                damageTaken = lightningDamage;
                break;
        }
        if (Status[(int)effects.STONEPROTECTION].Equals("Protected"))
            damageTaken = Math.Floor(damageTaken / 2);
        health = (int)(health - damageTaken);
        if (health < 0)
            health = 0;
        DisplayHealth();
    }

    private void GrassHeal()
    {
        health += 10;
        if (health > 100)
            health = 100;
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        int hundreds = 0;
        if (health == 100)
            hundreds = 1;
        int tens = (int)Math.Floor((double)health / 10);
        if (tens == 10)
            tens = 0;
        int ones = health % 10;

        Vector2 displayLoc = healthDisplayLocs[0].GetComponent<Rigidbody2D>().position;
        Destroy(displayedHealth[0]);
        displayedHealth[0] = Instantiate(numbers[hundreds], displayLoc, Quaternion.identity);

        displayLoc = healthDisplayLocs[1].GetComponent<Rigidbody2D>().position;
        Destroy(displayedHealth[1]);
        displayedHealth[1] = Instantiate(numbers[tens], displayLoc, Quaternion.identity);

        displayLoc = healthDisplayLocs[2].GetComponent<Rigidbody2D>().position;
        Destroy(displayedHealth[2]);
        displayedHealth[2] = Instantiate(numbers[ones], displayLoc, Quaternion.identity);
    }

    public void onPause()
    {
        for (int i = 0; i < Enum.GetNames(typeof(effects)).Length; i++)
        {
            if (cooldowns[i] != null)
            {
                int remaining = cooldowns[i].timeRemaining();
                timeRemaining[i] = remaining;
            }
        }
    }

    public void unPause()
    {
        for (int i = 0; i < Enum.GetNames(typeof(effects)).Length; i++)
        {
            if (cooldowns[i] != null)
            {
                cooldowns[i].reset(timeRemaining[i]);
                timeRemaining[i] = 0;
            }
        }
    }

    private void removeEffects()
    {
        if (Status[(int)effects.ICE].Equals("Ice"))
            cooldowns[(int)effects.ICE].reset(0);

        if (Status[(int)effects.FIRE].Equals("Fire"))
            cooldowns[(int)effects.FIRE].reset(0);

        if (Status[(int)effects.SHOCKED].Equals("Shock"))
            cooldowns[(int)effects.SHOCKED].reset(0);
    }
}
