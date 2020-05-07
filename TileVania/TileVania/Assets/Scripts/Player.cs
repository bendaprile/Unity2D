using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config variables
    // Multiplier for our movement speed
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] float horizontalLadderSpeedMultiplier = 0.5f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 15f);

    // State variables
    float gravityScaleAtStart;
    bool isAlive = true;
    string isRunning = "isRunning";
    string isClimbing = "isClimbing";

    // Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    // Message then methods

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        // If our character is not alive we don't want to do any of this
        if (!isAlive) { return; }

        Run();
        Jump();
        ClimbLadder();
        PlayerDeath();
    }

    private void Run()
    {
        myRigidbody.gravityScale = 1f;

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
        // bool to hold if our collider is touching the collider on the ground layer
        bool isTouchingGround = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        // If the Jump button is pressed...
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            // Create a new vector 2 with some y velocity and add it to our rigidbody's velocity
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        // Bool to keep track of whether we are colliding with a ladder or not
        bool isTouchingLadder = myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));

        // if we're not touching the ladder then set animation back to idle
        // and gravityScale back to what it started at
        if (!isTouchingLadder)
        {
            myAnimator.SetBool(isClimbing, false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }

        // Get rid of gravity for our player and grab our control throw in the vertical direction
        myRigidbody.gravityScale = 0f;
        float controlThrow = Input.GetAxis("Vertical"); // value is from -1 to +1

        // Set our y velocity and then set myRigidBody's velocity
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x * horizontalLadderSpeedMultiplier, controlThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;

        // If the player has vertical speed then set its animation state to climbing
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool(isClimbing, playerHasVerticalSpeed);
        
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

    private void PlayerDeath()
    {

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            TriggerDeath();
        }

        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        isAlive = false;

        // Set our animation to death animation
        myAnimator.SetTrigger("Death");

        // Set velocity so our player flings upwards
        myRigidbody.velocity = deathKick;

        // Process the player death depending on how many lives are left
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
