using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{

    private int numGuesses;
    private int guess;
    private int min;
    private int max;
    private bool gameOver;

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
        numGuesses = 1;
        guess = 500;
        min = 1;
        max = 1000;
        gameOver = false;

        Debug.Log("Welcome To Number Wizard, Ben");
        Debug.Log("Please pick a number between " + min + " and " + max + " .");
        Debug.Log("Is your number " + guess + "? (Up: Higher, Down: Lower, Enter: Correct)");

        // Max will never be guessed without this
        max += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {
            CheckGuess();
        }
        else if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Restarting... ");
                StartGame();
            }
        }
    }

    void CheckGuess()
    {
        // When UpArrow is pressed, print a message
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GuessUp();
            numGuesses += 1;
        }

        // When DownArrow is pressed, print a message
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GuessDown();
            numGuesses += 1;
        }

        // When Enter is pressed, game is over
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (numGuesses == 1)
            {
                Debug.Log("Yes I guessed your number on the first try! You suck!!");
                Debug.Log("Press Enter if you would like to play again...");
                gameOver = true;
            }
            else
            {
                Debug.Log("Yes I guessed your number!");
                Debug.Log("Press Enter if you would like to play again...");
                gameOver = true;
            }
        }
    }

    /*
     * Function that calculates a new guess higher than the last.
     */
    void GuessUp()
    {
        min = guess;
        guess = (min + max) / 2;

        Debug.Log("Is your number " + guess + "? (Up: Higher, Down: Lower, Enter: Correct)");
    }

    /*
     * Function that calculates a new guess lower than the last.
     */ 
    void GuessDown()
    {
        max = guess;
        guess = (min + max) / 2;

        Debug.Log("Is your number " + guess + "? (Up: Higher, Down: Lower, Enter: Correct)");
    }
}
