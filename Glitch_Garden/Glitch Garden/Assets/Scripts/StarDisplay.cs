using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    [SerializeField] int stars = 100;
    Text starsText;
    Color32 starsTextColor;

    private void Start()
    {
        starsText = GetComponent<Text>();
        starsTextColor = starsText.color;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        starsText.text = stars.ToString();
    }

    public bool EnoughStarsToPurchase(int purchaseAmount)
    {
        return stars >= purchaseAmount;
    }

    public void AddStars(int starsToAdd)
    {
        stars += starsToAdd;
        UpdateDisplay();
    }

    public void SpendStars(int starsCost)
    {
        if (starsCost <= stars)
        {
            stars -= starsCost;
            UpdateDisplay();
        }
    }
}