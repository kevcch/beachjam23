using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwitchWeapons : MonoBehaviourPun
{
    [SerializeField] GameObject[] weapons;

    private int currentIndex;

    private void Start()
    {
        currentIndex = 0;
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[currentIndex].SetActive(true);
    }

    private void Update()
    {
        if (base.photonView.IsMine)
        {

            if (Input.GetMouseButtonDown(1))
            {
                // TODO: Call ChangeWeapon() with RPC
            }
        }
    }

    // TODO: Implement ChangeWeapon() as a RPC function
    void ChangeWeapon()
    {

    }
}
