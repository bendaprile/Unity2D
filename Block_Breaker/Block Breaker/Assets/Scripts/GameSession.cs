using System.Collections;
using System.Collections.Generic;
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

    // Serialized so we can see in inspector
    [SerializeField] int currentScore = 0;
    int levelLostIndex;


    // Method executes before everything else
    private void Awake()
    {
        
        // Determines how many GameStatus objects we currently have in our scene
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;

        // If this new GameStatus brings the count to greater than one, destroy it
        if (gameStatusCount > 1)
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
        scoreText.text = currentScore.ToString();
        UpdateDifficulty();
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

    public void DestroyGameStatus()
    {
        Destroy(gameObject);
    }

    private void UpdateDifficulty()
    {
        var currentDifficulty = FindObjectOfType<Difficulty>().GetDifficulty();

        if (currentDifficulty == "easy")
        {
            gameSpeed = 0.5f;
            pointsPerBlockDestroyed = 50;
        }
        else if (currentDifficulty == "normal")
        {
            gameSpeed = 0.75f;
            pointsPerBlockDestroyed = 75;
        }
        else if (currentDifficulty == "hard")
        {
            gameSpeed = 1.0f;
            pointsPerBlockDestroyed = 100;
        }
        else
        {
            Debug.Log("No difficulty found, defaulting to normal... ");
            gameSpeed = 0.75f;
            pointsPerBlockDestroyed = 75;
        }
    }

    public void SetLevelLost()
    {
        if (levelLostIndex == 1)
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
        levelLostIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public int GetLevelLost()
    {
        scoreText.text = currentScore.ToString();
        return levelLostIndex;
    }
}
