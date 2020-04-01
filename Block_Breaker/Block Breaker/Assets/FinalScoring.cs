using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScoring : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    int finalScore;

    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        finalScore = CalculateFinalScore();

        finalScoreText.text = finalScore.ToString();
    }

    int CalculateFinalScore()
    {
        int livesLeft = gameSession.GetLivesLeft();
        int currentScore = gameSession.GetCurrentScore();

        return currentScore + ((livesLeft + 1) * 5000);
    }
}
