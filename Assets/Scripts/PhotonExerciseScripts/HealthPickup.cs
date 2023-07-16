using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthPickup : MonoBehaviourPun
{
    public void NetworkDestroy()
    {
        this.photonView.RPC("DestroyObject", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void DestroyObject()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.Destroy(gameObject);
    }
}
