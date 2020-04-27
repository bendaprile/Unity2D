using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesLeft : MonoBehaviour
{
    [SerializeField] int livesLeft = 10;
    Text livesText;

    // stored reference
    LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        livesText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        livesText.text = livesLeft.ToString();
    }

    public void AddLives(int livesToAdd)
    {
        livesLeft += livesToAdd;
        UpdateDisplay();
    }

    public void SubtractLife()
    {
        livesLeft -= 1;
        UpdateDisplay();

        if (livesLeft <= 0)
        {
            levelLoader.LoadLoseScene();
        }
    }
}
