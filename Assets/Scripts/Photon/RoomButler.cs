using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomButler : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static RoomButler room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    //Player info
    Player[] photonPlayers;
    public int playerInRoom;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
