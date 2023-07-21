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
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;
    void Update()
    {
        if (base.photonView.IsMine) {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius,
            _colliders, _interactableMask);
            if (_numFound > 0)
            {
                _interactable = _colliders[0].GetComponent<IInteractable>();
                if (_interactable != null)
                {
                    if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _interactable.Interact(this);

                    }
                }

            }
            else {
                if (_interactable != null) _interactable = null;
                if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
            }

        }
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }


}
