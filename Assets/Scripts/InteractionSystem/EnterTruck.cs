using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

public class EnterTruck : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => "Enter Truck";

    public bool EnablePrompt => true;

    public GameObject truck;
    public GameObject inside;
    public GameObject insideOverlay;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Enter truck inside");

        // show overlay and teleport player into "Inside"
        insideOverlay.SetActive(true);
        interactor.gameObject.transform.position = inside.transform.Find("Entrance").transform.position;
        interactor.GetComponent<PlayerMovement>().insideVehicle = true;

        // make camera follow truck
        CameraWork cameraWork = interactor.GetComponent<CameraWork>();
        cameraWork.UpdateTargetTransform(truck.transform);
        cameraWork.lookAt = false;
        return true;
    }
}
