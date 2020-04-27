using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] Attacker[] attackerPrefabs;
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
        int attackerIndex = Random.Range(0, attackerPrefabs.Length);

        Spawn(attackerIndex);
    }

    private void Spawn(int attackerIndex)
    {
        // Spawn an attacker at the randomized position in the array
        var newAttacker = Instantiate(attackerPrefabs[attackerIndex],
            transform.position,
            Quaternion.identity)
            as Attacker;

        // Places the attacker within its parent spawner in the hierarchy for sorting
        newAttacker.transform.parent = transform;
    }
}
