using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerItemManager: MonoBehaviourPun
{
    [SerializeField] public List<ItemClass> itemList = new List<ItemClass>();
    public List<GameObject> stackedItems;
    public Transform stackedItemsTransform;

    public void AddItem(ItemClass iso)
    {
        Debug.Log(iso.objectViewPrefab + " " + iso.objectType);
        itemList.Add(new ItemClass(iso.objectViewPrefab, iso.objectType));
        GameObject newItem = Instantiate(iso.objectViewPrefab,
            stackedItemsTransform.position + new Vector3(0, GetNumItems() * 0.3f, 0),
            stackedItemsTransform.rotation);
        newItem.AddComponent<FollowTarget>();
        newItem.GetComponent<FollowTarget>().target = stackedItemsTransform;
        newItem.GetComponent<FollowTarget>().offset = new Vector3(0, GetNumItems() * 0.3f, 0);
        stackedItems.Add(newItem);
    }

    public void AddItem(ItemScriptableObject iso) {
        Debug.Log(iso.objectViewPrefab + " " + iso.objectType);
        itemList.Add(new ItemClass(iso.objectViewPrefab, iso.objectType));
        GameObject newItem = Instantiate(iso.objectViewPrefab,
            stackedItemsTransform.position + new Vector3(0, GetNumItems() * 0.3f, 0),
            stackedItemsTransform.rotation);
        newItem.AddComponent<FollowTarget>();
        newItem.GetComponent<FollowTarget>().target = stackedItemsTransform;
        newItem.GetComponent<FollowTarget>().offset = new Vector3(0, GetNumItems() * 0.3f, 0);
        stackedItems.Add(newItem);
    }

    public void RemoveItem() {
        if (GetNumItems() <= 0) {
            Debug.Log("Error: Tried to remove items when none left to remove");
            return;
        }
        itemList.RemoveAt(itemList.Count - 1);
        Destroy(stackedItems[itemList.Count - 1]);
        stackedItems.RemoveAt(itemList.Count - 1);
    }

    public void DropItem() {

    }

    public int GetNumItems() {
        return itemList.Count;
    }
}
