using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] float timeToWaitForNextScene = 3f;
    [SerializeField] int attackersLeft;
    bool levelTimerFinished = false;

    [SerializeField] GameObject winOverlay;
    [SerializeField] GameObject loseOverlay;

    private void Start()
    {
        winOverlay.SetActive(false);
        loseOverlay.SetActive(false);
    }

    public void AttackerSpawned()
    {
        attackersLeft++;
    }

    public void AttackerKilled()
    {
        attackersLeft--;

        if (attackersLeft <= 0 && levelTimerFinished)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;

        StopSpawners();
    }

    public void LevelLost()
    {
        loseOverlay.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator HandleWinCondition()
    {
        // Show win overlay... I'm not adding audio its a waste of time
        winOverlay.SetActive(true);

        yield return new WaitForSeconds(timeToWaitForNextScene);

        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    private void StopSpawners()
    {
        // Set all attacker spawners spawn variable to false
        AttackerSpawner[] attackerSpawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner attackerSpawner in attackerSpawners)
        {
            attackerSpawner.StopSpawn();
        }
    }
}
