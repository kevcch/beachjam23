using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// holds the truck scene in as long as this object exists
public class TruckManager : MonoBehaviour
{
    public static TruckManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public string truckSceneName = "Inside";

    // Start is called before the first frame update
    void Start()
    {
        // create the truck scene
        if (!SceneManager.GetSceneByName(truckSceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(truckSceneName, LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogError("Error: truck inside scene was already loaded when TruckManager was created");
            Destroy(gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        // destroy this scene
        SceneManager.UnloadSceneAsync(truckSceneName);
        instance = null;
    }
}
