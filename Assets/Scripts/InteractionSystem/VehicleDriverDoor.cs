using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class VehicleDriverDoor : MonoBehaviourPun, IInteractable
{
    public string InteractionPrompt => "Drive Truck";
    public GameObject vehicle;
    public bool EnablePrompt => !vehicle.GetComponent<VehicleMovement>().vehicleActive;
    public bool Interact(Interactor interactor)
    {
        if (/*!vehicle.GetComponent<VehicleMovement>().vehicleActive*/vehicle.GetPhotonView().Owner == null) {
            PlayerMovement playerMovement = interactor.gameObject.GetComponent<PlayerMovement>();
            playerMovement.VehicleMode(true, vehicle.transform);
            vehicle.GetPhotonView().TransferOwnership(PhotonNetwork.LocalPlayer);
            EventBus.Publish<VehicleActivationEvent>(new VehicleActivationEvent(true));
            return true;
        }
        return false;
    }
}
