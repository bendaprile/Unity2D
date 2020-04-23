using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Create a reference to the otherCollider's parent GameObject
        GameObject otherObject = otherCollider.gameObject;

        // If the thing we collided with is a defender...
        if (otherObject.GetComponent<Defender>())
        {
            // Grab our Attacker component and call the attack method with the
            // gameObject we collided with
            GetComponent<Attacker>().Attack(otherObject);
        }
    }
}
