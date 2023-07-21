using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerUIHelper : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if (base.photonView.IsMine)
        {
            GameObject.Find("MiniMapCamera").GetComponent<Minimap>().player = transform;

        }
    }

}
