using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] string splashScreenName = "Splash Screen";
    [SerializeField] string startScreenName = "Start Screen";

    int currentSceneIndex;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 0)
        {
            StartCoroutine(LoadStartSceneAfterDelay());
        }
    }

    IEnumerator LoadStartSceneAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        LoadStartScene();
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadStartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(startScreenName);
    }

    public void LoadSplashScene()
    {
        SceneManager.LoadScene(splashScreenName);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene("Lose Scene");
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
