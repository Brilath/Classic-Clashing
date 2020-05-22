using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject enterGamePanel;
    [SerializeField] private GameObject connectionStatusPanel;
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private string gameScene = "TestScene";

    #region Unity Methods

    private void Awake()
    {
        // All clients in a room will load the same scene
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        enterGamePanel.SetActive(true);
        connectionStatusPanel.SetActive(false);
        lobbyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Public Methods
    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();

            connectionStatusPanel.SetActive(true);
            enterGamePanel.SetActive(false);
            lobbyPanel.SetActive(false);
        }
    }
    public void ConnectToRandomRoom()
    {
        // if this fails it will do a call back of On JoinRandomFailed
        PhotonNetwork.JoinRandomRoom();
    }
    #endregion

    #region Private Methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room " + Random.Range(0, 10000);

        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(randomRoomName, ro);
    }
    #endregion

    #region Photon Callbacks
    // Method called when connected to Master Server!!!!
    public override void OnConnectedToMaster()
    {
        Debug.Log($"{PhotonNetwork.NickName} connected to the master server!");

        lobbyPanel.SetActive(true);
        connectionStatusPanel.SetActive(false);
        enterGamePanel.SetActive(false);
    }
    // Method called when connected to the internet
    public override void OnConnected()
    {
        Debug.Log("Connected to the internet");
    }
    // Method will run if PhotonNetwork.JoinRandomRoom fails
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("No room found");
        CreateAndJoinRoom();
    }
    // Method called when connected sucessfully to a room
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined room " + PhotonNetwork.CurrentRoom.Name);

        PhotonNetwork.LoadLevel(gameScene);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined room " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " has " + PhotonNetwork.CurrentRoom.PlayerCount + " players");
    }
    #endregion
}
