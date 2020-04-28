﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject deathVFX;

    public void DealDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // Destroy attacker and trigger death effect
            Destroy(gameObject);
            TriggerDeathFX();
        }
    }

    private void TriggerDeathFX()
    {
        if (!deathVFX)
        {
            return;
        }

        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(deathVFXObject, 2f);
    }
}
