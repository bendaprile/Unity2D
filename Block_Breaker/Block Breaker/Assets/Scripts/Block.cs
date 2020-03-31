using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] AudioClip breakSound;

    // This method is called when anything collides with the block
    // The collision parameter will tell us what collided with the block
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Creates a new AudioSource with break sound at the position of the camera
        // This will play even if the block has been destroyed
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

        // This destroys the Block and takes it out of the hierarchy
        Destroy(gameObject);
    }
}
