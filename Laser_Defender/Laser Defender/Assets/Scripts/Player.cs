using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // configuration parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] int health = 200;
    [SerializeField] float explosionDuration = 0.7f;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Prefabs and Effects")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip shootingSFX;
    [SerializeField] [Range(0.0f, 1.0f)] float shootingVolume = 0.5f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0.0f, 1.0f)] float deathVolume = 0.5f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Called to set up the boundaries of the play space in order to limit the player
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        var sprite = GetComponent<SpriteRenderer>();

        // Grab the X and Y size of the sprite from center to right/top to use as padding
        var spriteXPadding = sprite.bounds.extents.x;
        var spriteYPadding = sprite.bounds.extents.y;


        // Create the minimum and maximum x values that we can travel to
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spriteXPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0, 0)).x - spriteXPadding;

        // Create the minimum and maximum y values that we can travel to
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + spriteYPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1f, 0)).y - spriteYPadding;
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
            // Start the FireContinuously coroutine and set it to firingCoroutine variable
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        // When the Fire1 button is released we will stop the coroutine
        if (Input.GetButtonUp("Fire1"))
        {
            // Stop the firingCoroutine that we set above
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {

        // Creates a loop so that when button is pressed down this will continuously shoot
        while (true)
        {
            // Create a GameObject called laser with the laserPrefab, at the Player's position, with no rotation
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            // Give the RigidBody2D of the laser some velocity in the y direction
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position, shootingVolume);

            // Waits for projectileFiringPeriod seconds before doing the while loop again
            yield return new WaitForSeconds(projectileFiringPeriod);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Grab the DamageDealer script from the other gameobject
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer)
        {
            throw new Exception("Null Damagedealer");
        }

        // Subtracts health and checks if the player should die
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        // Subtract the damage from the heatlh
        health -= damageDealer.GetDamage();

        // Destroys the laser so it cannot hurt the player again
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);

        // Instantiate a new explosion on the position of the ship and destroy it after a second
        GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 0.5f);

        FindObjectOfType<Level>().LoadGameOver();
    }
}
