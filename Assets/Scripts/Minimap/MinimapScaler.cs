using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinimapScaler : MonoBehaviour
{
    public RectTransform minimapTransform;
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
    }

    public void OnDragDelegate(PointerEventData data)
    {
        float size = Mathf.Max(Mathf.Abs(Screen.width - data.position.x), Mathf.Abs(Screen.height - data.position.y));
        minimapTransform.sizeDelta = new Vector2(size, size);
    }
}
