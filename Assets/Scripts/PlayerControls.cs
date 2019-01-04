using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerControls : MonoBehaviour
{
    CharacterController controls;
    [Header("Jump Settings")]
    [SerializeField]
    internal float jumpForce;
    private Vector3 jump;
    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<CharacterController>();
        jump = new Vector3(0, jumpForce, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (Input.GetButton("Jump")) {
            controls.Move(jump);
        }
    }
}
