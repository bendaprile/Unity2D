using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] int score = 0;

    private void Awake()
    {
        CreateSingleton();
    }

    private void CreateSingleton()
    {

        int gameSessionCount = FindObjectsOfType<GameSession>().Length;

        if (gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<Level>().LoadStartScene();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreToAdd)
    {
        Debug.Log("Score Added: " + scoreToAdd);
        score += scoreToAdd;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}