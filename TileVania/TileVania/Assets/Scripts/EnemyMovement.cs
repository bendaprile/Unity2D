using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }


    // Our boxcollider will trigger this when it leaves a platform
    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipEnemy();
    }

    // Will trigger when the capsule collider collides with a wall or player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the collision was with a wall then we will flip the enemy
        if (!collision.gameObject.GetComponent<Player>())
        {
            FlipEnemy();
        }
    }

    void FlipEnemy()
    {
        // Reverse the velocity of our enemy
        myRigidbody.velocity = new Vector2(-myRigidbody.velocity.x, 0f);

        // reverse the current scaling of the x axis
        // Mathf.Sign will return -1 when number is negative and +1 when number is positive
        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }
}
