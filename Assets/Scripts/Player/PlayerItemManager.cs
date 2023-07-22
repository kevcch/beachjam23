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
        GameObject newItem = PhotonNetwork.Instantiate(iso.objectViewPrefab.name,
            stackedItemsTransform.position + new Vector3(0, GetNumItems() * 0.3f, 0),
            stackedItemsTransform.rotation);
        if(!newItem.GetPhotonView().AmOwner) {
            newItem.GetPhotonView().TransferOwnership(PhotonNetwork.LocalPlayer);
        }
        newItem.AddComponent<FollowTarget>();
        newItem.GetComponent<FollowTarget>().target = stackedItemsTransform;
        newItem.GetComponent<FollowTarget>().offset = new Vector3(0, GetNumItems() * 0.3f, 0);
        stackedItems.Add(newItem);
    }

    public void AddItem(ItemScriptableObject iso) {
        Debug.Log(iso.objectViewPrefab + " " + iso.objectType);
        itemList.Add(new ItemClass(iso.objectViewPrefab, iso.objectType));
        GameObject newItem = PhotonNetwork.Instantiate(iso.objectViewPrefab.name,
            stackedItemsTransform.position + new Vector3(0, GetNumItems() * 0.3f, 0),
            stackedItemsTransform.rotation);
        if(!newItem.GetPhotonView().AmOwner) {
            newItem.GetPhotonView().TransferOwnership(PhotonNetwork.LocalPlayer);
        }
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
        if(!stackedItems[itemList.Count - 1].GetPhotonView().AmOwner) {
            stackedItems[itemList.Count - 1].GetPhotonView().TransferOwnership(PhotonNetwork.LocalPlayer);
        }
        PhotonNetwork.Destroy(stackedItems[itemList.Count - 1]);
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
        GameObject droppedItem = PhotonNetwork.Instantiate(droppedItemPrefab.name, stackedItemsTransform.position, stackedItemsTransform.rotation);
        //droppedItem.GetComponent<DroppedItem>().item = new ItemClass(item.objectViewPrefab, item.objectType);
        photonView.RPC("AssignValues", RpcTarget.All, droppedItem.GetPhotonView().ViewID, item.objectType.ToString());
        GameObject droppedItemView = PhotonNetwork.Instantiate(droppedItem.GetComponent<DroppedItem>().item.objectViewPrefab.name, stackedItemsTransform.position, stackedItemsTransform.rotation);
        photonView.RPC("ParentItem", RpcTarget.All, droppedItem.GetPhotonView().ViewID, droppedItemView.GetPhotonView().ViewID, item.objectType.ToString());
        RemoveItem();
        AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/Using/dropSound") as AudioClip);
    }

    [PunRPC]
    public void AssignValues(int droppedItemID, string objectType) {
        Debug.Log("Assigning values");
        PhotonView.Find(droppedItemID).gameObject.GetComponent<DroppedItem>().item = new ItemClass((ItemType)System.Enum.Parse( typeof(ItemType), objectType));
    }
    [PunRPC]
    public void ParentItem(int droppedItemID, int droppedItemViewID, string objectType) {
        //GameObject droppedItemView = PhotonNetwork.Instantiate(PhotonView.Find(droppedItemID).gameObject.GetComponent<DroppedItem>().item.objectViewPrefab.name, stackedItemsTransform.position, stackedItemsTransform.rotation);
        PhotonView.Find(droppedItemViewID).gameObject.transform.parent = PhotonView.Find(droppedItemID).gameObject.transform;
    }

    public int GetNumItems() {
        return itemList.Count;
    }
}
