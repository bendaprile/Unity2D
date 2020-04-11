using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Grab the DamageDealer script from the other gameobject
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        // Subtracts health and checks if the enemy should be destroyed
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        // Subtract the damage from the heatlh
        health -= damageDealer.GetDamage();

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        // Destroys the laser so it cannot hurt anything else
        damageDealer.Hit();
    }
}
