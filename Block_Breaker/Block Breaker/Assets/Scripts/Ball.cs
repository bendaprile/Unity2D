using UnityEngine;

public class Ball : MonoBehaviour
{

    // Links to the Paddle gameobject
    [SerializeField] Paddle paddle;

    [SerializeField] float xPushVel = 2f;
    [SerializeField] float yPushVel = 15f;
    [SerializeField] float randomFactor = 0.2f;

    // Array of AudioClips to use randomly during a collision
    [SerializeField] AudioClip[] ballSounds;

    // State Difference Vector (Between paddle and ball)
    Vector2 paddleToBallVector;

    // bool to keep track of whether the ball has been launched
    private bool hasLaunched = false;

    //Cached component references (audio files)
    AudioSource audioSource;
    Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the vector to the difference between the ball and the paddle positions
        paddleToBallVector = transform.position - paddle.transform.position;

        yPushVel = FindObjectOfType<GameSession>().GetYPushVelocity();

        // We grab the AudioSource object on startup so we don't have to grab it every time
        audioSource = GetComponent<AudioSource>();
        rigidBody2D = GetComponent<Rigidbody2D>();
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
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor),
            Random.Range(0f, randomFactor));

        if (hasLaunched)
        {
            // Grabs a random AudioClip from the ballSounds Array
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];

            // Grabs the AudioSource component and plays an audio
            // PlayOneShot means it will not get cut off by other audio and will play all the way through
            audioSource.PlayOneShot(clip);

            rigidBody2D.velocity += velocityTweak;
        }
    }
}
