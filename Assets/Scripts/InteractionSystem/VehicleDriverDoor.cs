using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDriverDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public GameObject vehicle;
    public bool Interact(Interactor interactor)
    {
        if (!vehicle.GetComponent<VehicleMovement>().vehicleActive) {
            PlayerMovement playerMovement = interactor.gameObject.GetComponent<PlayerMovement>();
            playerMovement.VehicleMode(true, vehicle.transform);
            EventBus.Publish<VehicleActivationEvent>(new VehicleActivationEvent(true));
            return true;
        }
        return false;
    }
}
