using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks; // Serialized for debugging purposes

    // cached reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Increments the breakableBlocks variable each time the Block script calls this method
    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    // Decrement breakableBlocks each time the Block script calls this method
    public void BlockDestroyed()
    {
        breakableBlocks--;

        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

    
}
