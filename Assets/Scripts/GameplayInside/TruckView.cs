using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TruckView : MonoBehaviour
{
    private RawImage image;
    private new RectTransform transform;

    private int prevWidth, prevHeight;

    public string sceneName = "Inside";

    private bool subscribed = false;

    private void Start()
    {
        image = GetComponent<RawImage>();
        transform = GetComponent<RectTransform>();
        OnReinitRenderTexture();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        Vector2 screenSpaceSize = Vector2.Scale(transform.rect.size, transform.lossyScale);
        int w = Mathf.FloorToInt(screenSpaceSize.x);
        int h = Mathf.FloorToInt(screenSpaceSize.y);
        if (w != prevWidth || h != prevHeight && subscribed)
        {
            prevWidth = w;
            prevHeight = h;
            TruckViewTexture.instance.requiredWidth = w;
            TruckViewTexture.instance.requiredHeight = h;
            TruckViewTexture.instance.ReinitRenderTexture();
        }
    }

    private void OnReinitRenderTexture()
    {
        if (TruckViewTexture.instance != null)
        {
            image.texture = TruckViewTexture.instance.renderTexture;
        }
    }

    // ensure listener for changing texture is ready
    private void OnEnable()
    {
        if (!subscribed && TruckViewTexture.instance != null)
        {
            TruckViewTexture.instance.OnReinitRenderTexture.AddListener(OnReinitRenderTexture);
            subscribed = true;
            TruckViewTexture.instance.requiredWidth = prevWidth;
            TruckViewTexture.instance.requiredHeight = prevHeight;
            TruckViewTexture.instance.ReinitRenderTexture();
        }
    }
    private void OnDisable()
    {
        if (subscribed && TruckViewTexture.instance != null)
        {
            TruckViewTexture.instance.OnReinitRenderTexture.RemoveListener(OnReinitRenderTexture);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnEnable();
    }
}
