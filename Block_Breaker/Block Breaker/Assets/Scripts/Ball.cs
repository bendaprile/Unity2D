using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    // Links to the Paddle gameobject
    [SerializeField] Paddle paddle;

    [SerializeField] float xPushVel = 2f;
    [SerializeField] float yPushVel = 15f;

    // State Difference Vector (Between paddle and ball)
    Vector2 paddleToBallVedctor;

    private bool hasLaunched = false;

    // Start is called before the first frame update
    void Start()
    {

        // Sets the vector to the difference between the ball and the paddle positions
        paddleToBallVedctor = transform.position - paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLaunched == false)
        {
            LockBallToPaddle();
            LaunchBallOnMouseClick();
        }
    }

    private void LockBallToPaddle()
    {
        // Vector to track the paddle's current position
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);

        // Set the balls position to the paddle's + the difference vector
        transform.position = paddlePos + paddleToBallVedctor;
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPushVel, yPushVel);
            hasLaunched = true;
        }

    }
}
