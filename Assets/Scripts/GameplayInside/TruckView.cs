using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TruckView : MonoBehaviour
{
    private RawImage image;
    private new RectTransform transform;

    private int prevWidth, prevHeight;

    private bool subscribed = false;

    private void Start()
    {
        image = GetComponent<RawImage>();
        transform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 screenSpaceSize = Vector2.Scale(transform.rect.size, transform.lossyScale);
        int w = Mathf.FloorToInt(screenSpaceSize.x);
        int h = Mathf.FloorToInt(screenSpaceSize.y);
    }

}
