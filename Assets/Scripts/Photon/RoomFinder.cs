using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomFinder : MonoBehaviourPunCallbacks
{
    public static RoomFinder lobbyFinder;
    private bool canSearchRoom = false; 
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        lobbyFinder = this;
    }

    public bool FindRoom()
    {
        if (canSearchRoom)
        {
            canSearchRoom = false;
            PhotonNetwork.JoinRandomRoom();
        }
            
        return canSearchRoom;
    }

    public void FindRoomAbort()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnConnectedToMaster()
    {
        canSearchRoom = true;
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("Player connected to the server");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        switch (returnCode)
        {
            case 32760:
                CreateRoom();
                break;
            default:
                Debug.LogError("Code:" + returnCode + " Message:" + message);
                break;
        }
        
    }

    void CreateRoom()
    {
        Debug.Log("Creating Room");
        string roomName = "Room: " + Random.Range(0, 1000).ToString();
        RoomOptions roomsOps = new RoomOptions() { IsVisible=true, IsOpen=true, MaxPlayers=GlobalConst.MAX_PLAYER };
        PhotonNetwork.CreateRoom(roomName, roomsOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined room");
        SceneManager.LoadSceneAsync("Bus");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        canSearchRoom = true;
    }

}
