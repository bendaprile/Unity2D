using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{

    [Tooltip("Our level timer in SECONDS")]
    [SerializeField] float levelTime = 10;

    // Update is called once per frame
    void Update()
    {
        // Time since level load divided by levelTime will give us a proportion out of 1
        // of how much time has passed.
        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTime;

        bool timerFinished = (Time.timeSinceLevelLoad >= levelTime);

        // When the timer is finished call LevelTimerFinished in LevelController
        if (timerFinished)
        {
            FindObjectOfType<LevelController>().LevelTimerFinished();
        }
    }
}
