using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class ShootWhenClick : MonoBehaviourPun
{
    [SerializeField] float cooldown = 1f;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform Spawnpoint;
    [SerializeField] TMP_Text CooldownText;
    [SerializeField] Color Ready;
    [SerializeField] Color Cooling;
    float timer = 0f;

    void Start()
    {
        timer = 0f;
        CooldownText.text = "";
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (base.photonView.IsMine)
        {
            CooldownText.text = timer >= cooldown ? "Click!" : "Cooling...";
            CooldownText.color = timer >= cooldown ? Ready : Cooling;

            if (Input.GetMouseButtonDown(0) && timer >= cooldown)
            {
                ShootBullet();
                timer = 0f;
            }
        }

    }


    void ShootBullet()
    {
        // TODO: Implement ShootBullet() with PhotonNetwork.Instantiate
    }

}
