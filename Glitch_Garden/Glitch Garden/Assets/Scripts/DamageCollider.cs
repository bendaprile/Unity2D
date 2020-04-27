using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    LivesLeft livesLeft;

    private void Start()
    {
        livesLeft = FindObjectOfType<LivesLeft>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;

        if (otherObject.GetComponent<Attacker>())
        {
            livesLeft.SubtractLife();
        }
    }
}
