using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] int damage = 50;

    [Range(0f, 5f)]
    [SerializeField] float currentSpeed = 1f;
    

    // Reference to the current target we are attacking
    GameObject currentTarget;

    // Called before all initilization and attacker is spawned
    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawned();
    }

    // Called after everything and the attacker is destroyed 
    private void OnDestroy()
    {
        FindObjectOfType<LevelController>().AttackerKilled();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            // Don't create a reference to this... it doesn't work for some reason
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
    }

    // Called by animation events to update the speed of our attacker
    public void SetMovementSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    public void Attack(GameObject target)
    {
        // Set our animator to isAttacking = true to change animation
        GetComponent<Animator>().SetBool("isAttacking", true);

        // Set our current target to the target it collided with
        currentTarget = target;
    }

    public void StrikeCurrentTarget()
    {
        if (!currentTarget) { return; }

        // Grab health component from our currentTarget
        Health health = currentTarget.GetComponent<Health>();

        if (health)
        {
            // Call DealDamage method in health script
            health.DealDamage(damage);
        }
    }
}
