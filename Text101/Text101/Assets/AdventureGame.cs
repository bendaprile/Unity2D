using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    // [SerializeField] Means that we now have this field available in the inspector     // and we can set the variable from there instead of opening the script.
    [SerializeField] Text textComponent;
    [SerializeField] State startingState;

    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = startingState;

        // The () around the text represent it as a string
        textComponent.text = state.GetStoryText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
