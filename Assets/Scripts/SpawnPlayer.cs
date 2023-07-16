using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviourPun
{
    [SerializeField] GameObject player;
    [SerializeField] Transform[] Spawnpoints;

    private void Start()
    {
        int index = Random.Range(0, Spawnpoints.Length);
        GameObject playerObj = PhotonNetwork.Instantiate(player.name, Spawnpoints[index].position, Quaternion.identity);
    }
}
