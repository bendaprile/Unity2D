using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    [SerializeField] int stars = 100;
    Text starsText;

    private void Start()
    {
        starsText = GetComponent<Text>();

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        starsText.text = stars.ToString();
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
