using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomButton : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;
    LobbyManager manager;

    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    public void SetRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void OnClickButton()
    {
        manager.JoinRoom(roomName.text);
    }
}
