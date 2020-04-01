using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float screenWidthUnits = 16f;
    [SerializeField] float minXInUnits = 0f;
    [SerializeField] float maxXInUnits = 16f;
    [SerializeField] float paddleSize = 2f;

    GameSession gameSession;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate object references
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();

        // Calculates the min and max relative to the size of the Paddle
        minXInUnits = minXInUnits + (paddleSize / 2);
        maxXInUnits = maxXInUnits - (paddleSize / 2);
    }

    // Update is called once per frame
    void Update()
    {
        // Creates a Vector2 (x, y) with the current x position and y position of the Paddle
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);

        // Mathf.Clamp limits the location of the Paddle between the min and max.
        paddlePos.x = Mathf.Clamp(GetXPos(), minXInUnits, maxXInUnits);

        // Move the "Paddle" gameobject to the paddlePos position
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            // If autoplay enabled then the paddle position is set to the balls x pos
            return ball.transform.position.x;
        }
        else
        {
            // Calculates the position of the mouse in Units
            return Input.mousePosition.x / Screen.width * screenWidthUnits;
        }
    }
}
