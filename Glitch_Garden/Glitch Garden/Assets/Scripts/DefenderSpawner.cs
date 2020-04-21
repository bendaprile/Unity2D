using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{

    Defender defender;

    private void OnMouseDown()
    {
        Vector2 mousePos = GetSquareClicked();

        SpawnDefender(mousePos);
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
    }

    public void SetSelectedDefender(Defender defenderToSelect)
    {
        defender = defenderToSelect;
    }
}
