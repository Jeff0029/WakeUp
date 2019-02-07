using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerControls : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField]
    internal Transform xPivot;
    [SerializeField]
    internal bool inverted = false;
    [SerializeField]
    internal float yAxisSpeed = 5f;
    [SerializeField]
    internal float xAxisSpeed = 5f;
    [SerializeField]
    internal float xMaxUpDeg = 60f;
    [SerializeField]
    internal float xMaxDownDeg = 40f;
    internal float xMaxAxis = 0.8f;

    [Header("Movement Settings")]
    [SerializeField]
    internal float initialMomentum = 4f;
    [SerializeField]
    internal float finalMomentum = 8f;
    [SerializeField]
    internal float aceleration = 1.5f;
    
    [Header("Jump Settings")]
    [SerializeField]
    internal float jumpVelocity = 5;
    [SerializeField]
    internal float terminalVelocity = 53;
    [SerializeField]
    internal float addedFallVelocity = 5;

    private float fallSpeed = 0f;

    private Vector3 jump;
    CharacterController controls;
    
    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Rotation
        transform.Rotate(0, yAxisSpeed * Input.GetAxis("Mouse X"), 0);

        float addedRotation = Input.GetAxis("Mouse Y") * xAxisSpeed * (inverted ? 1 : -1);
        float workingRotation = (xPivot.eulerAngles.x + addedRotation + 180) % 360;
        float clampedRotation = Mathf.Clamp(workingRotation, 180-xMaxUpDeg, 180+ xMaxDownDeg);

        xPivot.eulerAngles = new Vector3((clampedRotation + 180) % 360, xPivot.eulerAngles.y, xPivot.eulerAngles.z);

        // Jumping
        if (Input.GetButton("Jump") && controls.isGrounded)
        {
            fallSpeed = jumpVelocity;
        } else
        {
            fallSpeed = (Physics.gravity.y - addedFallVelocity) * Time.deltaTime;
        }
        
        // Movement
        float speed = Mathf.Clamp((controls.velocity.magnitude) * aceleration, initialMomentum, finalMomentum);
        Vector3 move = new Vector3(Input.GetAxis("Horizontal")*speed, fallSpeed + controls.velocity.y, Input.GetAxis("Vertical") * speed);
        controls.Move(transform.TransformDirection(move) * Time.deltaTime);
    }
}
