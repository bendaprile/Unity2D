using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{

    string[] difficulties = { "easy", "normal", "hard" };
    string currentDifficulty;

    private void Awake()
    {
        int difficultyCount = FindObjectsOfType<Difficulty>().Length;

        if (difficultyCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetDifficultyEasy()
    {
        currentDifficulty = difficulties[0];
    }

    public void SetDifficultyNormal()
    {
        currentDifficulty = difficulties[1];
    }

    public void SetDifficultyHard()
    {
        currentDifficulty = difficulties[2];
    }

    public string GetDifficulty()
    {
        Debug.Log("Difficulty set to: " + currentDifficulty);
        return currentDifficulty;
    }

    public void DestroyDifficulty()
    {
        Destroy(gameObject);
    }
}
