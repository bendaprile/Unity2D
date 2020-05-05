using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config variables
    // Multiplier for our movement speed
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    // State variables
    bool isAlive = true;
    string isRunning = "isRunning";

    // Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;

    // Message then methods

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        Jump();
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

        // Flip sprite to appropriate direction of velocity
        FlipSprite();

        UpdateRunAnimation();
    }

    private void UpdateRunAnimation()
    {
        // Check if the player has horizontal speed (is he running)
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        // Will set running animation to true if there is horizontal speed
        myAnimator.SetBool(isRunning, playerHasHorizontalSpeed);
    }

    private void Jump()
    {

        bool isTouchingGround = GetComponent<CapsuleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"));

        // If the Jump button is pressed...
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            // Create a new vector 2 with some y velocity and add it to our rigidbody's velocity
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        // if the player is moving horizontally
        if (playerHasHorizontalSpeed)
        {
            // reverse the current scaling of the x axis
            // Mathf.Sign will return -1 when number is negative and +1 when number is positive
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
