using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkColorScheme : MonoBehaviourPun
{
    [SerializeField] Color AllyColor;
    [SerializeField] Color OpponentColor;

    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = base.photonView.IsMine ? AllyColor : OpponentColor;
    }
}
