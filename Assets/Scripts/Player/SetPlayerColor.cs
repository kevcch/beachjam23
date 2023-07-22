using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SetPlayerColor : MonoBehaviourPun
{
    public List<GameObject> models;
    void Awake()
    {
        int i = Mathf.FloorToInt(base.photonView.ViewID / 1000) - 1;
        //Random.Range(0, models.Count);
        models[i].SetActive(true);
    }
}
