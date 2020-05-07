using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float levelExitSlowMoveFactor = 0.2f;
    float currentTimeScale;

    private void Start()
    {
        currentTimeScale = Time.timeScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = levelExitSlowMoveFactor; 

        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = currentTimeScale;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
