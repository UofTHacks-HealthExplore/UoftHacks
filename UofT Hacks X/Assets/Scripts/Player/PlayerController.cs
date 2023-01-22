using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] CharacterController controller;

    // Movement
    [SerializeField] private float smoothSpeed = 0.2f;
    public float moveSpeed = 5f;
    Vector2 movementInput;
    Vector2 currentVelocity;
    Vector2 smoothInputVelocity;
    public bool lockMovement;

    // Rotation
    private Transform cam;

    // Gravity
    [SerializeField] private float gravity = -30f;
    Vector3 verticalVelocity;
    [SerializeField] private LayerMask groundLayer;
    [HideInInspector] public bool grounded;

    void OnMove(InputValue value)
    {
        
        print("bruh");
        movementInput = value.Get<Vector2>();
        // Rotate when moving
        float angle = Mathf.LerpAngle(transform.eulerAngles.y, cam.eulerAngles.y, Time.deltaTime);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
    }

    void Awake()
    {
        cam = GameObject.Find("PlayerCam").transform;

        // Mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        VertMove();
        Look();
    }

    void Move()
    {
        if (!lockMovement)
        {
            currentVelocity = Vector2.SmoothDamp(currentVelocity, movementInput, ref smoothInputVelocity, smoothSpeed);
            Vector3 velocity = transform.right * currentVelocity.x + transform.forward * currentVelocity.y;
            controller.Move(velocity * moveSpeed * Time.deltaTime);
        }
    }

    void VertMove()
    {
        grounded = Physics.Raycast(transform.position, -Vector3.up, controller.bounds.extents.y + 0.1f, groundLayer);

        if (grounded)
        {
            verticalVelocity.y = 0;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    void Look()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.rotation.eulerAngles.y, transform.eulerAngles.z);
    }
}
