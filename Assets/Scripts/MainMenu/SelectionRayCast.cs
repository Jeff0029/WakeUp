using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectionAction
{
    joinLobby = 0,
    customize,
    option,
    exit
}

public class SelectionRayCast : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("worked");
    }
}
