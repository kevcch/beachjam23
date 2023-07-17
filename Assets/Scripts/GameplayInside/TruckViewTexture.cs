using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TruckViewTexture : MonoBehaviour
{
    public static TruckViewTexture instance;

    private void Awake()
    {
        // this is a shitty singleton, just override it
        // because we actually do want a new version for every scene
        instance = this;
    }

    public Camera skillTreeCamera;
    public UnityEvent OnReinitRenderTexture;

    // filled out by skill tree UI
    public int requiredWidth = 400, requiredHeight = 400;

    // filled out programmatically, nice to have visible
    public RenderTexture renderTexture;

    public void Start()
    {
        ReinitRenderTexture();
    }

    // call this when you want to reset the size of the UI for the skill tree view
    public void ReinitRenderTexture()
    {
        Debug.Log("Init with " + requiredWidth + ", " + requiredHeight);
        // get the size from somewhere
        renderTexture = new RenderTexture(requiredWidth, requiredHeight, 0);

        skillTreeCamera.targetTexture = renderTexture;
        OnReinitRenderTexture.Invoke();
    }
}
