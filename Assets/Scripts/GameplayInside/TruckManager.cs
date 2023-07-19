using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

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

    public UnityEvent insideOutsideChangeEvent = new UnityEvent();

    public bool inside = false;

    void Start() {
        
    }

    //public void ToggleTruck(bool inside) {
    //    if(inside) {
    //        SceneManager.SetActiveScene(SceneManager.GetSceneByName(truckSceneName));
    //        truckView.SetActive(true);
    //        // Disable player input for players in old scene
    //        foreach(PlayerInput input in playerInputs) {
    //            if(input.gameObject.scene == SceneManager.GetSceneByName(outsideSceneName)) {
    //                input.enabled = false;
    //            }
    //        }
    //        // Enable player input for players in new scene
    //        foreach(PlayerInput input in playerInputs) {
    //            if(input.gameObject.scene == SceneManager.GetSceneByName(truckSceneName)) {
    //                input.enabled = true;
    //            }
    //        }

    //    } else {
    //        SceneManager.SetActiveScene(SceneManager.GetSceneByName(outsideSceneName));
    //        truckView.SetActive(false);
    //        // Disable player input for players in old scene
    //        foreach(PlayerInput input in playerInputs) {
    //            if(input.gameObject.scene == SceneManager.GetSceneByName(truckSceneName)) {
    //                input.enabled = false;
    //            }
    //        }
    //        // Enable player input for players in new scene
    //        foreach(PlayerInput input in playerInputs) {
    //            if(input.gameObject.scene == SceneManager.GetSceneByName(outsideSceneName)) {
    //                input.enabled = true;
    //            }
    //        }
    //    }
    //    playerChangeEvent.Invoke();
    //}

    private void OnDestroy()
    {
        // destroy this scene
        instance = null;
    }
}
