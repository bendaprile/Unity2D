using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{

    // 1f is realtime, 0.5 is 2x slower and so on
    // Range sets a minimum and maximum we can set in the inspector
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreText;

    // Serialized so we can see in inspector
    [SerializeField] int currentScore = 0;


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

        UpdateDifficulty();

        // Using GetComponent<TextMeshProUGUI here breaks the link between score text
        // when using DontDestroyOnLoad
        scoreText.text = currentScore.ToString();
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
        }
        else if (currentDifficulty == "normal")
        {
            gameSpeed = 0.75f;
        }
        else if (currentDifficulty == "hard")
        {
            gameSpeed = 1.0f;
        }
        else
        {
            Debug.Log("No difficulty found, defaulting to normal... ");
            gameSpeed = 0.75f;
        }
    }
}
