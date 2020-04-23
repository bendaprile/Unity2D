using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [Range (0f, 5f)]
    float currentSpeed = 1f;

    // Reference to the current target we are attacking
    GameObject currentTarget;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
    }

    // Called by animation events to update the speed of our attacker
    public void SetMovementSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    public void Attack(GameObject target)
    {
        animator.SetBool("isAttacking", true);

        currentTarget = target;
    }
}
