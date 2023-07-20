using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDriverDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Attempt to enter vehicle");
        return true;
    }
}
