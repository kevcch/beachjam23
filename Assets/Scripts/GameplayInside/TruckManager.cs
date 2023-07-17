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
        StartCoroutine(LoadTruck());
    }

    public string truckSceneName = "Inside";
    public string outsideSceneName = "Game";

    private PlayerInput[] playerInputs;

    void Start() {
        
    }

    IEnumerator LoadTruck()
    {
        // create the truck scene
        if (!SceneManager.GetSceneByName(truckSceneName).isLoaded)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(truckSceneName, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            playerInputs = GameObject.FindObjectsOfType<PlayerInput>();
            foreach(PlayerInput input in playerInputs) {
                Debug.Log(input.gameObject.name + ": " + input.gameObject.scene);
            }
        }
        else
        {
            Debug.LogError("Error: truck inside scene was already loaded when TruckManager was created");
            Destroy(gameObject);
            yield return null;
        }
    }

    public void ToggleTruck(bool inside) {
        if(inside) {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(truckSceneName));
            truckView.SetActive(true);
            // Disable player input for players in old scene
            foreach(PlayerInput input in playerInputs) {
                if(input.gameObject.scene == SceneManager.GetSceneByName(outsideSceneName)) {
                    input.enabled = false;
                }
            }
            // Enable player input for players in new scene
            foreach(PlayerInput input in playerInputs) {
                if(input.gameObject.scene == SceneManager.GetSceneByName(truckSceneName)) {
                    input.enabled = true;
                }
            }

        } else {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(outsideSceneName));
            truckView.SetActive(false);
            // Disable player input for players in old scene
            foreach(PlayerInput input in playerInputs) {
                if(input.gameObject.scene == SceneManager.GetSceneByName(truckSceneName)) {
                    input.enabled = false;
                }
            }
            // Enable player input for players in new scene
            foreach(PlayerInput input in playerInputs) {
                if(input.gameObject.scene == SceneManager.GetSceneByName(outsideSceneName)) {
                    input.enabled = true;
                }
            }
        }
    }

    private void OnDestroy()
    {
        // destroy this scene
        SceneManager.UnloadSceneAsync(truckSceneName);
        instance = null;
    }
}
