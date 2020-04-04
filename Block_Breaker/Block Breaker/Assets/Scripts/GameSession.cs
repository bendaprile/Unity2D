using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

    // 1f is realtime, 0.5 is 2x slower and so on
    // Range sets a minimum and maximum we can set in the inspector
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesLeftText;
    [SerializeField] bool isAutoPlayEnabled = false;

    // Serialized so we can see in inspector
    [SerializeField] int currentScore = 0;
    int buildIndexOfLevelLost;
    float yPushVel;
    [SerializeField] int livesLeft;

    // Method executes before everything else
    private void Awake()
    {
        // Determines how many GameStatus objects we currently have in our scene
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;

        // If this new GameStatus brings the count to greater than one, destroy it
        if (gameSessionCount > 1)
        {
            // This is required because Destroy runs after everything else and that can sometimes cause a bug
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        UpdateDifficulty();
        scoreText.text = currentScore.ToString();
        livesLeftText.text = livesLeft.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        // Sets the speed of the game
        Time.timeScale = gameSpeed;
    }

    // Called from block script when the block is destroyed
    public void UpdateScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    private void UpdateDifficulty()
    {
        var currentDifficulty = FindObjectOfType<Difficulty>().GetDifficulty();

        if (currentDifficulty == "easy")
        {
            yPushVel = 9f;
            pointsPerBlockDestroyed = 50;
            livesLeft = 2;
        }
        else if (currentDifficulty == "normal")
        {
            yPushVel = 11f;
            pointsPerBlockDestroyed = 75;
            livesLeft = 1;
        }
        else if (currentDifficulty == "hard")
        {
            yPushVel = 13f;
            pointsPerBlockDestroyed = 100;
            livesLeft = 0;
        }
        else
        {
            Debug.Log("No difficulty found, defaulting to normal... ");
            yPushVel = 12.5f;
            pointsPerBlockDestroyed = 75;
            livesLeft = 1;
        }
    }

    public void UpdateLivesLeft(string currentDifficulty)
    {
        if (currentDifficulty == "easy")
        {
            livesLeft = 2;
        }
        else if (currentDifficulty == "normal")
        {
            livesLeft = 1;
        }
        else if (currentDifficulty == "hard")
        {
            livesLeft = 0;
        }
        else
        {
            Debug.Log("No difficulty found, defaulting to normal... ");
            livesLeft = 1;
        }

        livesLeftText.text = livesLeft.ToString();
    }

    // Called by the Ball Script to get the starting velocity in the y direction
    public float GetYPushVelocity()
    {
        return yPushVel;
    }

    public void SetLevelLost()
    {
        livesLeft--;

        if (buildIndexOfLevelLost == 1)
        {
            currentScore = 0;
        }
        else
        {
            if ((currentScore - 200) > 0)
            {
                currentScore -= 200;
            }
            else
            {
                currentScore = 0;
            }
        }
        buildIndexOfLevelLost = SceneManager.GetActiveScene().buildIndex;
    }

    public int GetLevelLost()
    {
        scoreText.text = currentScore.ToString();
        livesLeftText.text = livesLeft.ToString();
        return buildIndexOfLevelLost;
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public int GetLivesLeft()
    {
        return livesLeft;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void DestroyGameSession()
    {
        Destroy(gameObject);
    }
}
