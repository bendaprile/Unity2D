using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{

    int startBuildIndex;

    private void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;

        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        // If our current scene index is different that means...
        // we've gone to the next scene or first scene so destroy this object
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentBuildIndex != startBuildIndex)
        {
            Destroy(gameObject);
        }
    }
}
