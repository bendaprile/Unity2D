using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        var buttons = FindObjectsOfType<DefenderButton>();

        // foreach loop through all defenderbuttons in scene and set them to grey
        foreach(DefenderButton button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = new Color32(84, 84, 84, 255);
        }

        // set this button to white since its selected
        gameObject.GetComponent<SpriteRenderer>().color = Color.white; 
    }
}
