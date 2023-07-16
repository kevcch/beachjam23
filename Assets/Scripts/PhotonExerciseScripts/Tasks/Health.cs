using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviourPun, IPunObservable
{
    [SerializeField] int MaxHealth;
    [SerializeField] Transform Container;
    [SerializeField] GameObject HeartsPrefab;

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            if (value <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
                return;
            }

            currentHealth = Mathf.Min(value, MaxHealth);
        }
    }

    private int currentHealth;
    private List<GameObject> heartsList = new List<GameObject>();

    void Start()
    {
        currentHealth = MaxHealth;

        for (int i = 0; i < MaxHealth; i++)
        {
            GameObject clone = Instantiate(HeartsPrefab, Container);
            heartsList.Add(clone);
        }
    }

    void Update()
    {
        for (int i = 0; i < heartsList.Count; i++)
        {
            heartsList[i].SetActive(i < currentHealth);
        }
    }

    // TODO: Use PhotonSerializeView to sync current health.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    public void NetworkChangeHealth(int amount)
    {
        this.photonView.RPC("ChangeHealth", RpcTarget.All, amount);
    }

    [PunRPC]
    public void ChangeHealth(int amount)
    {
        Debug.Log("Change Health Called! My view: " + this.photonView.IsMine);
        if (!this.photonView.IsMine) return;
        CurrentHealth += amount;
    }
}
