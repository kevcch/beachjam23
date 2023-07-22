using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable
{
    public string _interactionPrompt = "Pick up Item";
    public string InteractionPrompt => _interactionPrompt;

    public bool EnablePrompt => true;

    public ItemClass item;

    public bool Interact(Interactor interactor)
    {
        if (interactor.gameObject.GetComponent<PlayerItemManager>().GetNumItems() < 4)
        {
            interactor.gameObject.GetComponent<PlayerItemManager>().AddItem(item);
            AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/itemPickUp") as AudioClip);
            Destroy(gameObject);
            return true;
        }
        return false;
    }

}
