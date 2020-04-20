using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] Attacker attackerPrefab;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 5f;

    bool spawn = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {

        // While spawn is true this will do the SpawnAttacker coroutine
        // As soon as spawn is false this coroutine will stop
        while (spawn)
        {
            // Wait for a random amount of time between minSpawnDelay and maxSpawnDelay
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {

        // Spawn an attacker
        var newAttacker = Instantiate(attackerPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
