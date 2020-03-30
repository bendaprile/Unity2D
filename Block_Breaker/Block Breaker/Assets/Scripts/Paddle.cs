using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField] float screenWidthUnits = 16f;
    [SerializeField] float minXInUnits = 0f;
    [SerializeField] float maxXInUnits = 16f;
    [SerializeField] float paddleSize = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Calculates the min and max relative to the size of the Paddle
        minXInUnits = minXInUnits + (paddleSize / 2);
        maxXInUnits = maxXInUnits - (paddleSize / 2);
    }

    // Update is called once per frame
    void Update()
    {

        // Calculates the position of the mouse in Units
        float xMousePosUnits = Input.mousePosition.x / Screen.width * screenWidthUnits;
        //Debug.Log(xMousePosUnits.ToString);

        // Creates a Vector2 (x, y) with the current x position and y position of the Paddle
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);

        // Mathf.Clamp limits the location of the Paddle between the min and max.
        paddlePos.x = Mathf.Clamp(xMousePosUnits, minXInUnits, maxXInUnits);

        // Move the "Paddle" gameobject to the paddlePos position
        transform.position = paddlePos;
    }
}
