using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float explosionDuration = 0.7f;
    [SerializeField] int scoreWhenKilled = 150;

    // Used for enemy shooting mechanism
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3;

    [Header("Effects & Prefabs")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0.0f, 1.0f)] float deathVolume = 0.8f;
    [SerializeField] AudioClip shootingSFX;
    [SerializeField] [Range(0.0f, 1.0f)] float shootingVolume = 0.5f;

    //cached references
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        gameSession = FindObjectOfType<GameSession>();
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
            ShootBasic();
        }
        else if (tag == "Terry")
        {
            ShootBoss();
        }
        else
        {
            ShootBasic();
        }
    }

    private void ShootBasic()
    {
        // Create a GameObject called laser with the laserPrefab, at the Enemy's position, with no rotation
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

        // Give the RigidBody2D of the laser some velocity in the y direction
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        // Play the basic shooting sound effect
        AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position, shootingVolume);
    }

    private void ShootBoss()
    {
        Vector3 leftPosition = new Vector3(transform.position.x - 0.8f, transform.position.y);
        Vector3 rightPosition = new Vector3(transform.position.x + 0.8f, transform.position.y);

        // Create two lasers on either side of the enemy
        GameObject leftLaser = Instantiate(laserPrefab, leftPosition, Quaternion.identity) as GameObject;
        GameObject rightLaser = Instantiate(laserPrefab, rightPosition, Quaternion.identity) as GameObject;

        // Give the RigidBody2D of the lasers some velocity in the y direction
        leftLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        rightLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);

        // Play the big guns shooting sound
        AudioSource.PlayClipAtPoint(shootingSFX, Camera.main.transform.position, shootingVolume);
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
            Debug.Log("Ship Dead");
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        // Play the death sound effect
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);

        // Instantiate a new explosion on the position of the ship and destroy it after a second
        GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, explosionDuration);

        // Add to the score the value that this ship gives when killed
        gameSession.AddToScore(scoreWhenKilled);
    }
}
