using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeritcalScroll : MonoBehaviour
{

    [SerializeField] float waterRiseSpeed = 0.3f;

    Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        myRigidbody.velocity = new Vector2(0f, waterRiseSpeed);
    }
}
