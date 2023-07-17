using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// holds the truck scene in as long as this object exists
public class TruckManager : MonoBehaviour
{
    public static TruckManager instance;
    public GameObject truckView;
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
    public string outsideSceneName = "Game";

    void Start() {
        LoadTruck();
    }

    void LoadTruck()
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

    public void ToggleTruck(bool inside) {
        // PlayerInput[] oldInputs = GameObject.FindObjectsOfType<PlayerInput>();
        // foreach(PlayerInput oldInput in oldInputs) {
        //     oldInput.enabled = false;
        // }
        if(inside) {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(truckSceneName));
            truckView.SetActive(true);

        } else {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(outsideSceneName));
            truckView.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        // destroy this scene
        SceneManager.UnloadSceneAsync(truckSceneName);
        instance = null;
    }
}
