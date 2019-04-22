using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView PV;
    public int chartacterValue;
    public GameObject myAvatar;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        Transform[] spawnPoints = StartGameSpawn.startGSpawn.spawnPoints;
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Player"), spawnPoint.position, spawnPoint.rotation, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
