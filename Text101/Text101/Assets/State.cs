using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This creates a menu item when you right click on the assets folder named “State”
[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{

    // TextArea makes the text box in Unity much larger [TextArea(minSize, amountOfLinesBeforeScrolling)]
    [TextArea(18, 10)] [SerializeField] string storyText;

    public string GetStoryText()
    {
        return storyText;
    }

}
