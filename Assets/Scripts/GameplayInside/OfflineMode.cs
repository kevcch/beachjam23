using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class OfflineMode : MonoBehaviour
{
    public static bool UseOfflineMode = true;

    void Awake()
    {
        if (!UseOfflineMode)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PhotonNetwork.OfflineMode = true;
        if (!PhotonNetwork.InRoom)
        {
            PhotonNetwork.CreateRoom("Offline Room");
        }

        // Not necessary since once OfflineMode is true, PhotonNetwork.Instantiate will 
        // work which means the PlayerSpawner calls spawn itself
        // SpawnPlayer spawner = FindObjectOfType<SpawnPlayer>();
        // spawner.Spawn();
    }
}
