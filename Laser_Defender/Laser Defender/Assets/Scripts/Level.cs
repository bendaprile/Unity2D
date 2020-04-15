using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] float loadDelay = 1f;

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartMenu");

        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadSceneShortDelay("GameOver"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneShortDelay(string scene)
    {
        yield return new WaitForSeconds(loadDelay);

        SceneManager.LoadScene(scene);
    }
}
