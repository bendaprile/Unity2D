using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private int startingScene = 0;

    public void LoadNextScene()
    {
        // Grabs the index of the scene when this method is called
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Loads the scene that has the index one higher than our current one
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        // Loads the first scene in the build
        SceneManager.LoadScene(startingScene);
    }
}
