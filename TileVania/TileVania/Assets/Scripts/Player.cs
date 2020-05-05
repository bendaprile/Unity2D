using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Multiplier for our movement speed
    [SerializeField] float runSpeed = 5f;

    // stored reference to our rigidbody
    Rigidbody2D myRigidbody;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        // Grab the value of the horizontal axis' input
        float controlThrow = Input.GetAxis("Horizontal"); // value is from -1 to +1

        // Create new vector 2 with...
        // x value as our control value velocity times a speed multiplier
        // y value as our current rigidbody's y velocity so we don't change anything
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);

        // Set the rigidbody's velocity to our player velocity
        myRigidbody.velocity = playerVelocity;
    }
}
