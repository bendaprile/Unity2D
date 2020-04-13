using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject laserPrefab;

    // Used for enemy shooting mechanism
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        // When shotCounter reaches 0 shoot a projectile and reset shotCounter
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {

        if (tag == "Basic")
        {
            // Create a GameObject called laser with the laserPrefab, at the Enemy's position, with no rotation
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            // Give the RigidBody2D of the laser some velocity in the y direction
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        }
        else if (tag == "Terry")
        {

            Vector3 leftPosition = new Vector3(transform.position.x - 0.8f, transform.position.y);
            Vector3 rightPosition = new Vector3(transform.position.x + 0.8f, transform.position.y);

            // Create two lasers on either side of the enemy
            GameObject leftLaser = Instantiate(laserPrefab, leftPosition, Quaternion.identity) as GameObject;
            GameObject rightLaser = Instantiate(laserPrefab, rightPosition, Quaternion.identity) as GameObject;

            // Give the RigidBody2D of the lasers some velocity in the y direction
            leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
            rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        }
        else
        {
            // Create a GameObject called laser with the laserPrefab, at the Enemy's position, with no rotation
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

            // Give the RigidBody2D of the laser some velocity in the y direction
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Grab the DamageDealer script from the other gameobject
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer)
        {
            throw new System.Exception("Null DamageDealer");
        }

        // Subtracts health and checks if the enemy should be destroyed
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        // Subtract the damage from the heatlh
        health -= damageDealer.GetDamage();

        // Destroys the laser so it cannot hurt anything else
        damageDealer.Hit();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
