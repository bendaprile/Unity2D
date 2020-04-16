using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] string splashScreenName = "Splash Screen";
    [SerializeField] string startScreenName = "Start Screen";

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(LoadStartSceneAfterDelay());
        }
    }

    IEnumerator LoadStartSceneAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        LoadStartScene();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(startScreenName);
    }

    public void LoadSplashScene()
    {
        SceneManager.LoadScene(splashScreenName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
