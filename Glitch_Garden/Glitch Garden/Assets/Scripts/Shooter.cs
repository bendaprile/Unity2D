using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    // This is a child gameobject of shooter
    [SerializeField] GameObject gun;

    // Reference to the particular spawner this object is
    AttackerSpawner myLaneSpawner;

    // Reference to our animator
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        SetLaneSpawner();
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            // Set the isAttacking boolean in our animator to true to transition into attack
            animator.SetBool("isAttacking", true);
        }
        else
        {
            // Set the isAttacking boolean in our animator to false to transition into idle
            animator.SetBool("isAttacking", false);
        }
    }

    private bool IsAttackerInLane()
    {
        // Get the amount of chidren in our lane
        var childrenCount = myLaneSpawner.transform.childCount;

        // If there are no attackers in our lane then return false
        if (childrenCount <= 0)
        {
            return false;
        }

        return true;
    }

    private void SetLaneSpawner()
    {
        // Populate array of attacker spawners in the scenes
        // Array is used because number of spawners should not change
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach (AttackerSpawner spawner in spawners)
        {
            // Checks that our spawner's position is equal to the shooter's position
            // meaning they are in the same lane
            // Will be true for 1-1 but not 1-2 or 2-1
            bool isCloseEnough = (Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon);

            // If the spawner is in the same lane as the shooter then the shooter stores a reference to the spawner
            if (isCloseEnough)
            {
                myLaneSpawner = spawner;
            }
        }
    }

    public void ShootProjectile()
    {
        Instantiate(projectile, gun.transform.position, transform.rotation);
    }
}
