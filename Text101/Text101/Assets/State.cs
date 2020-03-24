using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This creates a menu item when you right click on the assets folder named “State”
[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{

    // TextArea makes the text box in Unity much larger [TextArea(minSize, amountOfLinesBeforeScrolling)]
    [TextArea(18, 10)] [SerializeField] string storyText;

    // nextStates is an Array that contains the possible next states given our current state
    [SerializeField] State[] nextStates;

    public string GetStoryText()
    {
        return storyText;
    }

    // Returns the variable nextStates which have been given for a specific state in Unity inspector
    public State[] GetNextStates()
    {
        return nextStates;
    } 
}
