using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisplayUsername : MonoBehaviourPun
{
    [SerializeField] TMPro.TMP_Text nameTag;

    private void Start()
    {
        string username = FindUsername();
        nameTag.text = username;
    }

    // TODO: Implement this function so it returns this PhotonView's username
    string FindUsername()
    {
        return "";
    }
}
