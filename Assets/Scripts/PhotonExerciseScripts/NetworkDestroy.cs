using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkDestroy : MonoBehaviourPun, IPunObservable
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != gameObject.layer && other.gameObject.CompareTag("Bullet"))
        {
            if (base.photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
