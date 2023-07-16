using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Projectiles : MonoBehaviourPun
{
    [SerializeField] int Damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != gameObject.layer && other.gameObject.GetComponent<Health>() != null)
        {
            if (base.photonView.IsMine)
            {
                other.gameObject.GetComponent<Health>().NetworkChangeHealth(-Damage);
            }
        }

        if (base.photonView.IsMine) PhotonNetwork.Destroy(gameObject);
    }
}
