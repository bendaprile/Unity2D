using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] int attackersLeft;
    bool levelTimerFinished = false;

    public void AttackerSpawned()
    {
        attackersLeft++;
    }

    public void AttackerKilled()
    {
        attackersLeft--;

        if (attackersLeft <= 0 && levelTimerFinished)
        {
            Debug.Log("End Level Now");
        }
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;

        // Set all attacker spawners spawn variable to false
        var attackerSpawners = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner attackerSpawner in attackerSpawners)
        {
            attackerSpawner.SetSpawn(false);
        }
    }
}
