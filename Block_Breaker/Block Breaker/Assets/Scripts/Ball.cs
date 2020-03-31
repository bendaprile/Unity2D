using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    // Links to the Paddle gameobject
    [SerializeField] Paddle paddle;

    [SerializeField] float xPushVel = 2f;
    [SerializeField] float yPushVel = 15f;

    // Array of AudioClips to use randomly during a collision
    [SerializeField] AudioClip[] ballSounds;

    // State Difference Vector (Between paddle and ball)
    Vector2 paddleToBallVector;

    // bool to keep track of whether the ball has been launched
    private bool hasLaunched = false;

    //Cached component references (audio files)
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {

        // Sets the vector to the difference between the ball and the paddle positions
        paddleToBallVector = transform.position - paddle.transform.position;

        // We grab the AudioSource object on startup so we don't have to grab it every time
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLaunched)
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
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPushVel, yPushVel);
            hasLaunched = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasLaunched)
        {
            // Grabs a random AudioClip from the ballSounds Array
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];

            // Grabs the AudioSource component and plays an audio
            // PlayOneShot means it will not get cut off by other audio and will play all the way through
            myAudioSource.PlayOneShot(clip);
        }
    }
}
