using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class ClientLobby : MonoBehaviourPunCallbacks
{
    public static ClientLobby client_Lobby;

    private void Awake()
    {
        client_Lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Client connected to server in " + PhotonNetwork.CloudRegion + " at " + DateTime.Now.ToLocalTime() + " ");
    }

    public void Quickplay()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        switch (returnCode)
        {
            case 32760:
                Debug.Log("Code " + returnCode + ": " + message);
                Debug.Log("Creating room...");
                CreateRoom();
                break;
        }

    }

    public void CreateRoom()
    {
        int roomNum = UnityEngine.Random.Range(0, 255);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Room "+ roomNum, roomOps);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
