using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] AudioClip breakSound;

    // cached reference
    Level level;

    private void Start()
    {
        // Instantiates level reference to the Level object so we can call public methods
        level = FindObjectOfType<Level>();

        // Increments the breakableBlocks variable in the Level.cs
        level.CountBreakableBlocks();
    }

    // This method is called when anything collides with the block
    // The collision parameter will tell us what collided with the block
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBlock();
    }

    private void DestroyBlock()
    {
        // Creates a new AudioSource with break sound at the position of the camera
        // This will play even if the block has been destroyed
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

        // Decrements the breakableBlocks variable in Level.cs
        level.BlockDestroyed();

        // This destroys the Block and takes it out of the hierarchy
        Destroy(gameObject);
    }
}
