using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // tags
    [SerializeField] string breakableTag = "Breakable";
    [SerializeField] string unbreakableTag = "Unbreakable";

    // cached references
    Level level;

    // state variables
    [SerializeField] int timesHit; // TODO only serialized for debug purposes


    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        // Instantiates level reference to the Level object so we can call public methods
        level = FindObjectOfType<Level>();

        if (tag == breakableTag)
        {
            // Increments the breakableBlocks variable in the Level.cs
            level.CountBlocks();
        }
    }



    // This method is called when anything collides with the block
    // The collision parameter will tell us what collided with the block
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == breakableTag)
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        // If times hit is 1 then sprite index will be 0 and we'll pull the 0 index from the hitSprites array
        int spriteIndex = timesHit - 1;

        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array index: " + spriteIndex
                + ", gameObject: " + gameObject.name);
        }
        
    }

    private void DestroyBlock()
    {
        // Increments the score when the block is destroyed
        FindObjectOfType<GameSession>().UpdateScore();

        PlayBlockDestroyVFX();

        // Creates a particle effect at the position of the block
        TriggerSparklesVFX();

        // This destroys the Block and takes it out of the hierarchy
        Destroy(gameObject);

        // Decrements the breakableBlocks variable in Level.cs
        level.BlockDestroyed();
    }

    private void TriggerSparklesVFX()
    {
        // Instantiate a sparkles effect at the position of the block
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }

    private void PlayBlockDestroyVFX()
    {
        // Creates a new AudioSource with break sound at the position of the camera
        // This will play even if the block has been destroyed
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

    }
}
