using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerItemManager: MonoBehaviourPun
{
    [SerializeField] public List<ItemClass> itemList = new List<ItemClass>();
    public List<GameObject> stackedItems;
    public Transform stackedItemsTransform;
    public GameObject droppedItemPrefab;

    private void Update()
    {
        if (base.photonView.IsMine) {
            if (itemList.Count > 0 && Input.GetKeyDown(KeyCode.Q)) {
                DropItem();
            }
        }
        
    }
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
        Destroy(stackedItems[itemList.Count - 1]);
        stackedItems.RemoveAt(itemList.Count - 1);
        itemList.RemoveAt(itemList.Count - 1);
    }

    public void DropItem() {
        if (GetNumItems() <= 0)
        {
            Debug.Log("Error: Tried to drop items when none left to drop");
            return;
        }
        ItemClass item = itemList[itemList.Count - 1];
        GameObject droppedItem = Instantiate(droppedItemPrefab, stackedItemsTransform.position, stackedItemsTransform.rotation);
        droppedItem.GetComponent<DroppedItem>().item = new ItemClass(item.objectViewPrefab, item.objectType);
        GameObject droppedItemView = Instantiate(droppedItem.GetComponent<DroppedItem>().item.objectViewPrefab, stackedItemsTransform.position, stackedItemsTransform.rotation);
        droppedItemView.transform.parent = droppedItem.transform;

        RemoveItem();
        AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/Using/dropSound") as AudioClip);
    }

    public int GetNumItems() {
        return itemList.Count;
    }
}
