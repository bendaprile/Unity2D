using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI difficultyPrompt;

    private int startingScene = 0;

    public void LoadNextScene()
    {
        if (FindObjectOfType<Difficulty>().GetDifficulty() == null
            && SceneManager.GetActiveScene().buildIndex == 0)
        {
            difficultyPrompt.text = "No difficulty selected. Please select a difficulty... ";
            difficultyPrompt.fontStyle = FontStyles.Bold;
        }
        else
        {
            // Grabs the index of the scene when this method is called
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Loads the scene that has the index one higher than our current one
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void LoadStartScene()
    {

        // Destroy both of our DontDestroyOnLoad objects when the game is reset
        FindObjectOfType<GameSession>().DestroyGameStatus();
        FindObjectOfType<Difficulty>().DestroyDifficulty();

        // Loads the first scene in the build
        SceneManager.LoadScene(startingScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        var sceneLostIndex = FindObjectOfType<GameSession>().GetLevelLost();
        SceneManager.LoadScene(sceneLostIndex);
    }
}
