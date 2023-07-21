using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTruck : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => "Enter Truck";

    public bool EnablePrompt => true;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Enter truck inside");
        return true;
    }
}
