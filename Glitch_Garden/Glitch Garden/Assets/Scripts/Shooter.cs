using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    // This is a child gameobject of shooter
    [SerializeField] GameObject gun;

    public void ShootProjectile()
    {
        Instantiate(projectile, gun.transform.position, transform.rotation);
    }
}
