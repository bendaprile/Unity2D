using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{

    // 1f is realtime, 0.5 is 2x slower and so on
    // Range sets a minimum and maximum we can set in the inspector
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the speed of the game
        Time.timeScale = gameSpeed;
    }
}
