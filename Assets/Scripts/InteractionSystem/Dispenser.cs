using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public bool _enablePrompt = true;
    public bool EnablePrompt => _enablePrompt;

    public int price;
    public ItemScriptableObject item;

    public bool Interact(Interactor interactor)
    {
        if (GameDataManager.instance.currency > price && interactor.gameObject.GetComponent<PlayerItemManager>().GetNumItems() < 4)
        {
            GameDataManager.instance.currency -= price;
            interactor.gameObject.GetComponent<PlayerItemManager>().AddItem(item);
            AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/Using/moneyBuyingComplete") as AudioClip);
            return true;
        }
        else if (GameDataManager.instance.currency <= price)
        {
            ToastManager.Toast("Can't buy that. Are you broke?");
            AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/Using/moneyNotEnough") as AudioClip);
        }
        else if (interactor.gameObject.GetComponent<PlayerItemManager>().GetNumItems() >= 4) {
            ToastManager.Toast("You are already carrying four items!");
            AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/Using/moneyNotEnough") as AudioClip);
        }
            
        return false;
    }
}
