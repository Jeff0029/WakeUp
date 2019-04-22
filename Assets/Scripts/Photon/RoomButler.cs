using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomButler : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static RoomButler room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;
    public Transform[] spawnPoints;

    //Player info
    public int myNumberInRoom;

    public int playerInGame;

    //Delayed start
    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float aMaxPlayer;
    private float timeToStart;

    private void Awake()
    {
        if (RoomButler.room == null)
        {
            RoomButler.room = this;
        } else if (RoomButler.room != this)
        {
            Destroy(RoomButler.room.gameObject);
            RoomButler.room = null;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void OnSceneFinishedLoading(Scene scene, LoadSceneMode sceneMode)
    {

    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;

    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;

    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        RPC_CreatePlayer(true);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Butler: joined room");
        SceneManager.LoadSceneAsync("Bus");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new player has joined the room");
        
        if (PhotonNetwork.PlayerList.Length >= GlobalConst.MAX_PLAYER && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    public override void OnPlayerLeftRoom(Player leavingPlayer)
    {
        base.OnPlayerLeftRoom(leavingPlayer);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Ready"))
        {
            Debug.Log(myNumberInRoom + " is marked as ready");
        }
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playerInGame++;
        if (playerInGame == PhotonNetwork.PlayerList.Length)
        {
           // PV.RPC("RPC_CreatePlayer", RpcTarget.OthersBuffered);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer(bool isSelf = false)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
        GameObject spawnedPlayer = PhotonNetwork.Instantiate(Path.Combine("Prefabs","Player"), spawnPoint.position, spawnPoint.rotation);
        Debug.Log(isSelf);
        if (isSelf)
        {
            PhotonView PlayerPV = spawnedPlayer.GetComponent<PhotonView>();
            PlayerPV.TransferOwnership(myNumberInRoom);
        } else
        {
            PlayerControls playerControls = spawnedPlayer.GetComponent<PlayerControls>();
            playerControls.RemoveClientComponents();
        }
        
    }
}
