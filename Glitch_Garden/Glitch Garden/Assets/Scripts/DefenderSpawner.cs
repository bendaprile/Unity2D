using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    const string DEFENDER_PARENT_NAME = "Defenders";
    Defender defender;
    GameObject defenderParent;

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderParent = GameObject.Find(DEFENDER_PARENT_NAME);

        if (!defenderParent)
        {
            defenderParent = new GameObject(DEFENDER_PARENT_NAME);
        }
    }

    private void OnMouseDown()
    {
        Vector2 mousePos = GetSquareClicked();

        //Check if there are any other defenders in the square already
        if (IsSquareFree(mousePos))
        {
            AttemptToPlaceDefenderAt(mousePos);
        }
    }

    private void AttemptToPlaceDefenderAt(Vector2 gridPos)
    {
        // Create a reference to the StarDisplay object in our scene
        var StarDisplay = FindObjectOfType<StarDisplay>();

        // Get the star cost from our defender getter
        int defenderCost = defender.GetStarCost();

        // Call method in StarDisplay to check if we have enough stars to buy this defender
        if (StarDisplay.EnoughStarsToPurchase(defenderCost))
        {
            // Spawn defender and subtract purchase cost from our stars
            SpawnDefender(gridPos);
            StarDisplay.SpendStars(defenderCost);
        }
    }

    private bool IsSquareFree(Vector2 worldPos)
    {
        var defenders = FindObjectsOfType<Defender>();

        foreach (Defender defender in defenders)
        {
            bool isSameSquareCloseEnough = (Mathf.Abs(defender.transform.position.x - worldPos.x) <= Mathf.Epsilon)
                && (Mathf.Abs(defender.transform.position.y - worldPos.y) <= Mathf.Epsilon);

            if (isSameSquareCloseEnough)
            {
                return false;
            }
        }

        return true;
    }

    private Vector2 GetSquareClicked()
    {
        // Grab the point on the screen that was clicked
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Convert that to a world position and return
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);

        Vector2 gridWorldPos = SnapToGrid(worldPos);

        return gridWorldPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        // Round x and y so they stick to the exact grid cell that was clicked in
        rawWorldPos.x = Mathf.RoundToInt(rawWorldPos.x);
        rawWorldPos.y = Mathf.RoundToInt(rawWorldPos.y);

        return rawWorldPos;
    }

    private void SpawnDefender(Vector2 mousePos)
    {
        var newDefender = Instantiate(defender, mousePos, Quaternion.identity) as Defender;
        newDefender.transform.parent = defenderParent.transform;
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }
}
