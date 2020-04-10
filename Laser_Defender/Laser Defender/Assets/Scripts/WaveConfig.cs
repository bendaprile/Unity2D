using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This puts the scriptable object at the top when we got to the create menu
[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        // Will hold our waypoints for the given path
        var waveWaypoints = new List<Transform>();

        // for each "child" (Waypoint) of type Transform under "parent" (pathPrefab)
        foreach (Transform child in pathPrefab.transform)
        {
            // Add that specific waypoint to the waveWaypoints list
            waveWaypoints.Add(child);
        }


        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }
}
