using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberWizard : MonoBehaviour
{

    [SerializeField] private int min;
    [SerializeField] private int max;
    [SerializeField] TextMeshProUGUI guessText;

    private int guess;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    /*
     * Function that updates variables and starts the game.
     */
    void StartGame()
    {
        Guess();
    }

    // This is called when the higher button is pressed
    public void OnPressHigher()
    {
        // Adding 1 to min means it won't guess that number when higher is pressed

        if (min < max)
        {
            min = guess + 1;
        }
        Guess();
    }

    // This is called when the lower button is pressed
    public void OnPressLower()
    {
        // Subtracting 1 from max means it won't guess that number when lower is pressed
        max = guess;
        Guess();
    }

    /*
     * Function that calculates a new guess
     */
    void Guess()
    {

        guess = Random.Range(min, max + 1);

        // Displays the guess in the field that we've specified in Unity
        guessText.text = guess.ToString();
    }

}
