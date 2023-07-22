using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderBubble : MonoBehaviour
{
    Camera playerCamera;
    [SerializeField] public GameObject[] UISlots;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("PlayerCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion rotation = playerCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
    public void ClearOrderBubble() {
        foreach (GameObject slot in UISlots)
            slot.GetComponent<SpriteRenderer>().sprite = null;
    }
    public void SetOrderBubble(List<ItemType> itemTypes) {
        ClearOrderBubble();
        for (int i = 0; i < itemTypes.Count; i++) {
            switch (itemTypes[i]) {
                case ItemType.Cup:
                    UISlots[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/cup");
                    break;
                case ItemType.Cone:
                    UISlots[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/cone");
                    break;
                case ItemType.Strawberry:
                    UISlots[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/strawberry");
                    break;
                case ItemType.Chocolate:
                    UISlots[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/chocolate");
                    break;
                case ItemType.Vanilla:
                    UISlots[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/vanilla");
                    break;
            }
        }
    }
}
