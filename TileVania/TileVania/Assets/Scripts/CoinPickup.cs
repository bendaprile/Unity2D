using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] int coinPickupScore = 100;
    [SerializeField] AudioClip coinPickupSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coinPickupSFX)
        {
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        }
        else
        {
            Debug.Log("No Coin Pickup SFX Assigned!");
        }

        FindObjectOfType<GameSession>().IncreaseScore(coinPickupScore);

        Destroy(gameObject);
    }
}
