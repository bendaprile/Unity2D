using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    // effects
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;

    // tags
    [SerializeField] string breakableTag = "Breakable";
    [SerializeField] string unbreakableTag = "Unbreakable";

    // vars
    [SerializeField] int maxHits = 1;

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
            timesHit++;

            if (timesHit >= maxHits)
            {
                HandleHit();
            }
        }
    }

    private void HandleHit()
    {
        // Increments the score when the block is destroyed
        FindObjectOfType<GameSession>().UpdateScore();

        // Creates a new AudioSource with break sound at the position of the camera
        // This will play even if the block has been destroyed
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

        // Creates a particle effect at the position of the block
        TriggerSparklesVFX();

        // This destroys the Block and takes it out of the hierarchy
        Destroy(gameObject);

        // Decrements the breakableBlocks variable in Level.cs
        level.BlockDestroyed();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);

        Destroy(sparkles, 2f);
    }
}
