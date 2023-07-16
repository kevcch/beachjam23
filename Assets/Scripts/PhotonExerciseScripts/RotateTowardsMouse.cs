using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateTowardsMouse : MonoBehaviourPun
{
    void Update () {
        if (!base.photonView.IsMine) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mousePos.y - transform.position.y,
            mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }
}
