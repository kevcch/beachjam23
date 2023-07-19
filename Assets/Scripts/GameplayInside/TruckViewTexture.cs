using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TruckViewTexture : MonoBehaviour
{
    public static TruckViewTexture instance;

    private void Awake()
    {
        // there should only be one truck scene anyways
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }

    private new Camera camera;
    public UnityEvent OnReinitRenderTexture;

    // filled out by skill tree UI
    public int requiredWidth = 400, requiredHeight = 400;

    // filled out programmatically, nice to have visible
    public RenderTexture renderTexture;

    public void Start()
    {
        camera = GetComponent<Camera>();

        ReinitRenderTexture();
    }

    // call this when you want to reset the size of the UI for the skill tree view
    public void ReinitRenderTexture()
    {
        //Set width and height if they're at 0
        if (requiredWidth == 0)
            requiredWidth = 400;
        if (requiredHeight == 0)
            requiredHeight = 400;
        // get the size from somewhere
        renderTexture = new RenderTexture(requiredWidth, requiredHeight, 0);

        camera.targetTexture = renderTexture;

        Debug.Log("Init with " + requiredWidth + ", " + requiredHeight);

        OnReinitRenderTexture.Invoke();
    }
}
