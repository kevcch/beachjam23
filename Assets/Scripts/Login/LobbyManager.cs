using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI Components")]
    [SerializeField] TMP_InputField RoomInput;
    [SerializeField] GameObject LobbyPanel;
    [SerializeField] GameObject RoomPanel;
    [SerializeField] TMP_Text RoomNameText;
    [SerializeField] GameObject PlayButton; 

    [Header("Player List Configs")]
    [SerializeField] List<PlayerButton> playerButtonsList = new List<PlayerButton>();
    [SerializeField] GameObject PlayerButtonPrefab;
    [SerializeField] Transform PlayerButtonParent;


    [Header("Other Configs")]
    [SerializeField] float TimeBetweenUpdate = 1.5f;
    float nextUpdateTime; 

    private void Start()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        PhotonNetwork.JoinLobby();
    }

    public void OnClickCreate()
    {
        string[] digits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string roomcode = "";

        for (int i = 0; i < 6; ++i)
        {
            roomcode += Random.Range(0, digits.Length - 1);
        }

        PhotonNetwork.CreateRoom(roomcode, new RoomOptions() { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(true);
        RoomNameText.text = PhotonNetwork.MasterClient.NickName + "'s Room (" + PhotonNetwork.CurrentRoom.Name + ")";
        UpdatePlayerList();
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void JoinRoom()
    {
        if (RoomInput.text.Length < 6) return; 
        PhotonNetwork.JoinRoom(RoomInput.text);
    }

    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        RoomPanel.SetActive(false);
        LobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList()
    {
        foreach(PlayerButton item in playerButtonsList)
        {
            Destroy(item.gameObject);
        }

        playerButtonsList.Clear();

        if (PhotonNetwork.CurrentRoom == null) return;

        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerButton newButton = Instantiate(PlayerButtonPrefab, PlayerButtonParent).GetComponent<PlayerButton>();
            newButton.SetPlayerInfo(player.Value);
            playerButtonsList.Add(newButton);

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            PlayButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
        }
    }

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
