using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    string gameOverScene = "Game Over";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().SetLevelLost();
        SceneManager.LoadScene(gameOverScene);
    }
}
