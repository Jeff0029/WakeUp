using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StartGameSpawn : MonoBehaviour
{
    public static StartGameSpawn startGSpawn;
    public Transform[] spawnPoints;

    private void OnEnable()
    {
        if (StartGameSpawn.startGSpawn == null)
        {
            StartGameSpawn.startGSpawn = this;
        }
    }

}
