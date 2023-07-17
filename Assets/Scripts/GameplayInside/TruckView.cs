using UnityEngine;
using UnityEngine.UI;

public class TruckView : MonoBehaviour
{
    private RawImage image;
    private new RectTransform transform;

    private int prevWidth, prevHeight;

    private void Start()
    {
        image = GetComponent<RawImage>();
        transform = GetComponent<RectTransform>();

        // create the truck scene
    }

    private void OnDestroy()
    {
        // close the truck scene
    }

    private void Update()
    {
        Vector2 screenSpaceSize = Vector2.Scale(transform.rect.size, transform.lossyScale);
        int w = Mathf.FloorToInt(screenSpaceSize.x);
        int h = Mathf.FloorToInt(screenSpaceSize.y);
        if (w != prevWidth || h != prevHeight)
        {
            prevWidth = w;
            prevHeight = h;
            // SkillTreeRender.instance.requiredWidth = w;
            // SkillTreeRender.instance.requiredHeight = h;
            // SkillTreeRender.instance.ReinitRenderTexture();
        }
    }

    private void OnReinitRenderTexture()
    {
        // image.texture = SkillTreeRender.instance.renderTexture;
    }

    // ensure listener for changing texture is ready
    private void OnEnable()
    {
        // SkillTreeRender.instance.OnReinitRenderTexture.AddListener(OnReinitRenderTexture);
    }
    private void OnDisable()
    {
    }
}
