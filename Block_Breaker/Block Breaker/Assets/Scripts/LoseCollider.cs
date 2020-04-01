using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    string gameOverScene = "Game Over";
    int livesLeft;

    GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        livesLeft = gameSession.GetLivesLeft();
        gameSession.SetLevelLost();

        if (livesLeft <= 0)
        {
            SceneManager.LoadScene(gameOverScene);
        }
        else
        {
            FindObjectOfType<SceneLoader>().ContinueGame();
        }
    }
} 
