using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkLayer : MonoBehaviourPun
{
    [SerializeField] string AllyLayer;
    [SerializeField] string OpponentLayer;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer( base.photonView.IsMine ? AllyLayer : OpponentLayer);
    }
}
