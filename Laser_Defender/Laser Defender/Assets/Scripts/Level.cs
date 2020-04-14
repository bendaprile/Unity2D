using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartMenu");
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
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(scene);
    }
}
