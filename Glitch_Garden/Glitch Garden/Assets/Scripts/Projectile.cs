using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] int damage = 50;

    const string PROJECTILE_PARENT_NAME = "Projectiles";
    GameObject projectileParent;

    private void Awake()
    {
        CreateDefenderParent();

        gameObject.transform.parent = projectileParent.transform;
    }

    private void CreateDefenderParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);

        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Grab health and attacker component to ensure they exist
        var health = otherCollider.GetComponent<Health>();
        var attacker = otherCollider.GetComponent<Attacker>();

        if (attacker && health)
        {
            // Call the Health scripts DealDamage script
            health.DealDamage(damage);

            // Destroy this projectile so it can't hurt anythign else
            Destroy(gameObject);
        }
    }
}
