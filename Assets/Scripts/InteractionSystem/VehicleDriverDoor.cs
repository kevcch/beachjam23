using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDriverDoor : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => "Drive Truck";
    public GameObject vehicle;
    public bool EnablePrompt => !vehicle.GetComponent<VehicleMovement>().vehicleActive;
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
