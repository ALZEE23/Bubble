using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;
    private bool isLampOn = true;
    private bool isMapOn = false;

    public AudioSource lampOn;
    public AudioSource lampOff;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public GameObject lantern;
    public GameObject camera;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private bool cursorVisible = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        camera.gameObject.SetActive(false);

        readyToJump = true;

        
        SetCursorVisibility(false);
    }

    private void Update()
    {
        
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        
        rb.drag = grounded ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!cursorVisible)
            {
                
                SetCursorVisibility(true);
            }
            else
            {
                
                Application.Quit();
            }
        }

        
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLantern();
        }

        
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }

    private void ToggleLantern()
    {
        if (isLampOn)
        {
            lantern.SetActive(false);
            lampOff.Play();
        }
        else
        {
            lantern.SetActive(true);
            lampOn.Play();
        }
        isLampOn = !isLampOn;
    }

    private void ToggleMap()
    {
        if (isMapOn)
        {
            camera.SetActive(false);
        }
        else
        {
            camera.SetActive(true);
        }
        isMapOn = !isMapOn;
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    
    private void SetCursorVisibility(bool visible)
    {
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
        cursorVisible = visible;
    }
}
