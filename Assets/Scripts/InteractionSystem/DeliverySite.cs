using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeliverySite : MonoBehaviourPun, IInteractable
{
    public string _interactionPrompt;
    public string InteractionPrompt => _interactionPrompt;
    
    public bool EnablePrompt => HasOrder();
    public GameObject OrderBubble;
    public ParticleSystem Confetti;

    [SerializeField] public List<ItemType> itemTypeStack = new List<ItemType>();

    void Start() {
        AttemptOrderGeneration();
        AttemptOrderGeneration();
        InvokeRepeating("AttemptOrderGeneration", 0, 40);
    }

    public bool Interact(Interactor interactor)
    {
        if (itemTypeStack.Count != 0)
        {
            PlayerItemManager pim = interactor.gameObject.GetComponent<PlayerItemManager>();
            if (pim.itemList.Count == itemTypeStack.Count)
            {
                //Check if item lists match
                for (int i = 0; i < pim.itemList.Count; i++)
                {
                    if (pim.itemList[i].objectType != itemTypeStack[i])
                    {
                        AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/Delivery/deliveryWrongOrder") as AudioClip);
                        ToastManager.Toast("Wrong order! Remember that order matters!");
                        return false;
                    }
                }
                //If they match, remove and award currency
                while (pim.itemList.Count != 0)
                {
                    pim.RemoveItem();
                }
                CompleteOrder();
                return true;
            }
        }
        AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/Delivery/deliveryWrongOrder") as AudioClip);
        ToastManager.Toast("Wrong order! Remember that order matters!");
        return false;
    }
    public bool HasOrder() {
        return itemTypeStack.Count != 0;
    }
    public void CompleteOrder()
    {
        GameDataManager.instance.gameObject.GetPhotonView().TransferOwnership(PhotonNetwork.LocalPlayer);
        GameDataManager.instance.currency += itemTypeStack.Count * 10;
        ToastManager.Toast("Succesful order! Earned $" + itemTypeStack.Count * 10 + "!");
        AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/Delivery/deliverySuccess") as AudioClip);
        Confetti.Play();
        itemTypeStack.Clear();
        OrderBubble.SetActive(false);
    }

    public List<ItemType> GenerateOrder()
    {
        if (HasOrder()) {
            Debug.Log("Error: order already exists!");
            return null;
        }
        //Cup or cone
        if (Random.Range(0, 2) == 0)
            itemTypeStack.Add(ItemType.Cup);
        else
            itemTypeStack.Add(ItemType.Cone);

        //Num scoops
        int numScoops;
        float numScoopChance = Random.Range(0, 1f);
        if (numScoopChance < 0.5f)
            numScoops = 1;
        else if (numScoopChance < 0.8f)
            numScoops = 2;
        else
            numScoops = 3;

        //Type of scoops
        for (int i = 0; i < numScoops; i++) {
            int type = Random.Range(1, 4);
            switch (type) {
                case 1:
                    itemTypeStack.Add(ItemType.Strawberry);
                    break;
                case 2:
                    itemTypeStack.Add(ItemType.Chocolate);
                    break;
                case 3:
                    itemTypeStack.Add(ItemType.Vanilla);
                    break;
            }

        }
        return itemTypeStack;
    }

    [PunRPC]
    public void SyncOrder(string[] itemTypeStack) {
        OrderBubble.SetActive(true);
        // Deserialize
        OrderBubble.GetComponent<OrderBubble>().SetOrderBubble(ConvertItemStackBack(itemTypeStack));
    }

    public void AttemptOrderGeneration() {
        if (base.photonView.IsMine && !HasOrder()) {
            if (Random.Range(0, 1f) < 0.10f) {
                List<ItemType> itemTypeStack = GenerateOrder();
                photonView.RPC("SyncOrder", RpcTarget.All, ConvertItemStack(itemTypeStack));
            }
        }
    }

    private string[] ConvertItemStack(List<ItemType> itemTypeStack) {
        List<string> itemStringStack = new List<string>();
        foreach(ItemType item in itemTypeStack) {
            itemStringStack.Add(item.ToString());
        }
        return itemStringStack.ToArray();
    }
    private List<ItemType> ConvertItemStackBack(string[] itemStringStack) {
        List<ItemType> itemTypeStack = new List<ItemType>();
        foreach(string item in itemStringStack) {
            itemTypeStack.Add((ItemType)System.Enum.Parse( typeof(ItemType), item));
        }
        return itemTypeStack;
    }
}
