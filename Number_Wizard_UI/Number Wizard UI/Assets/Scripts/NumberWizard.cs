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

        guess = Random.Range(min, max + 1);
        guessText.text = guess.ToString();

        // Max will never be guessed without this
        max += 1;
    }

    // This is called when the higher button is pressed
    public void OnPressHigher()
    {
        GuessUp();
    }

    // This is called when the lower button is pressed
    public void OnPressLower()
    {
        GuessDown();
    }

    /*
     * Function that calculates a new guess higher than the last.
     */
    void GuessUp()
    {
        min = guess;
        guess = Random.Range(min, max + 1);

        // Displays the guess in the field that we've specified in Unity
        guessText.text = guess.ToString();
    }

    /*
     * Function that calculates a new guess lower than the last.
     */ 
    void GuessDown()
    {
        max = guess;
        guess = Random.Range(min, max + 1);

        // Displays the guess in the field that we've specified in Unity
        guessText.text = guess.ToString();
    }
}
