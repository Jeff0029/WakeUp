using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(PlayerControls))]
public class MenuInputs : MonoBehaviour
{
    Transform warpToExit;
    PlayerControls pControls;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag != "Player")
        {
            Debug.LogError("Must be attached to the player");
        }

        pControls = GetComponent<PlayerControls>();
        if (SceneManager.GetActiveScene().name == "House")
        {
            warpToExit = GameObject.Find("WarpToExit").transform;
        } else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButtonDown("Cancel")) {
            transform.position = warpToExit.position;
            pControls.Teleport(warpToExit);
        }
    }
}
