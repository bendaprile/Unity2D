using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start was changed to a coroutine to facilitate looping enemy waves
    IEnumerator Start()
    {

        // This do while loop will continue spawning waves of enemies until looping = false
        do
        {
            yield return StartCoroutine(SpawnAllWaves());         
        }
        while (looping);
    }

    // Spawns all the waves in order
    private IEnumerator SpawnAllWaves()
    {
        // Run through for every wave in the waveConfigs and spawn all enemies in that wave
        for (int waveIndex=startingWave; waveIndex<waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        var maxEnemies = waveConfig.GetNumberOfEnemies();

        // Loop for as many times as number of enemies we want to spawn
        for (int i=0; i<maxEnemies; i++)
        {
            // Instantiate our waveConfig using getters for...
            // EnemyPrefab, Waypoints.position, and no rotation
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].position,
                Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            // Wait a certain amount of time before spawning another enemy
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            looping = false;
        }
    }
}
