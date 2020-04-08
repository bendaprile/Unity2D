using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // configuration parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] GameObject laserPrefab;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        StartCoroutine(PrintAndWait());
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        var sprite = GetComponent<SpriteRenderer>();

        // Grab the X and Y size of the sprite from center to right/top to use as padding
        var spriteXPadding = sprite.bounds.extents.x;
        var spriteYPadding = sprite.bounds.extents.y;


        // Create the minimum and maximum x values that we can travel to
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spriteXPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spriteXPadding;

        // Create the minimum and maximum y values that we can travel to
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + spriteYPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y - spriteYPadding;
    }

    IEnumerator PrintAndWait()
    {
        Debug.Log("Damn gotta wait boys");
        yield return new WaitForSeconds(3);
        Debug.Log("Well that wasn't too bad");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {

        // If the button that is mapped to Fire1 (from input manager) is pressed...
        if (Input.GetButtonDown("Fire1"))
        {
            // Create a GameObject called laser with the laserPrefab, at the Player's position, with no rotation
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            // Give the RigidBody2D of the laser some velocity in the y direction
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }

    private void Move()
    {

        // Grab the change in X for this frame along the Horizontal axis
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // Limits the new y position of our current plus the change in y to be between yMin and yMax
        var newXPos = Mathf.Clamp((transform.position.x + deltaX), xMin, xMax);
        var newYPos = Mathf.Clamp((transform.position.y + deltaY), yMin, yMax);


        // Update x position to our newly found x position
        transform.position = new Vector2(newXPos, newYPos);
    }
}
