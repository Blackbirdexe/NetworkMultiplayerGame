using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class WaitingRoom : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;

    [SerializeField]
    private int multiplayerScene;

    private int menuScene;

    private int currentPlayerCount;
    private int roomSize = 4;

    [SerializeField]
    private TextMeshProUGUI roomCountDisplay;
    [SerializeField]
    private TextMeshProUGUI timerToStartDisplay;
    [SerializeField]
    private GameObject countDownDisplay;

    private bool readyToCountdown;
    private bool readyToStart;
    private bool startingGame;

    private int currentTimer;

    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        PlayerCountUpdate();
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " has joined the room");
        PlayerCountUpdate();

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " has left the room");
        PlayerCountUpdate();
    }

    void PlayerCountUpdate()
    {
        currentPlayerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        roomCountDisplay.text = currentPlayerCount + "/" + roomSize;

        if (currentPlayerCount == roomSize)
        {
            CountDownToStart(true);
        }
        else
        {
            CountDownToStart(false);
        }
    }

    IEnumerator Timer(int seconds)
    {
        for (int i = seconds; i >= 0; i--)
        {
            currentTimer = i;
            timerToStartDisplay.text = currentTimer.ToString();
            yield return new WaitForSeconds(1f);
        }
        readyToStart = true;
        StartGame();
    }
    void CountDownToStart(bool countdown)
    {
        if (countdown == true)
        {
            Debug.Log("Starting countdown to game");
            StartCoroutine(Timer(10));
            countDownDisplay.SetActive(true);
        }
        else if(countdown == false)
        {
            StopCoroutine(Timer(10));
            countDownDisplay.SetActive(false);
        }
    }

    void StartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerScene);
    }
}
