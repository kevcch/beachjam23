using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Interactor : MonoBehaviourPun
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    void Update()
    {
        if (base.photonView.IsMine) {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius,
            _colliders, _interactableMask);
            if (_numFound > 0)
            {
                var interactable = _colliders[0].GetComponent<IInteractable>();
                if (interactable != null && Input.GetKeyDown(KeyCode.E)) {
                    interactable.Interact(this);
                }
            }
        }
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }


}
