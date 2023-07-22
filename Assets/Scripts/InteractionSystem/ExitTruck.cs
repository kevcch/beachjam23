using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

public class ExitTruck : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => "Exit Truck";

    public bool EnablePrompt => true;

    public GameObject exitPoint;
    public GameObject insideOverlay;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Exit truck inside");

        // hide overlay and teleport player outside
        insideOverlay.SetActive(false);
        interactor.gameObject.transform.position = exitPoint.transform.position;

        // make camera follow player
        CameraWork cameraWork = interactor.GetComponent<CameraWork>();
        cameraWork.UpdateTargetTransform(interactor.transform);
        return true;
    }
}
