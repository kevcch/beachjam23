using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public bool _enablePrompt = true;
    public bool EnablePrompt => _enablePrompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Dispense Item");
        return true;
    }
}
