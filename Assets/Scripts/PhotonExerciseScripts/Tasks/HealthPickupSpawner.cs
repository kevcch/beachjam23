using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthPickupSpawner : MonoBehaviourPun
{
    [SerializeField] float Cooldown;
    [SerializeField] GameObject HealthPickupPrefab;

    float lastSpawnTime;
    List<GameObject> cloneList = new List<GameObject>();

    private void Start()
    {
        lastSpawnTime = 0;
        this.photonView.TransferOwnership(PhotonNetwork.MasterClient);
    }

    private void Update()
    {
        if (cloneList.Count == 0 && Time.time - lastSpawnTime >= Cooldown)
        {
            SpawnHealthPickup();
            lastSpawnTime = Time.time;
        }

        cloneList.RemoveAll(item => item == null);
    }

    // TODO:
    // Implement this function so only master client spawns health pickups as scene object. 
    // Also make sure that only one health pickup is spawned at a time (use cloneList). 
    void SpawnHealthPickup()
    {

    }
}
