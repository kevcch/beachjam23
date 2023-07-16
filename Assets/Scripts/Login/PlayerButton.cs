using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
    }
}
