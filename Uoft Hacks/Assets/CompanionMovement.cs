using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CompanionMovement : MonoBehaviour
{
    // Rotation
    private Transform cam;
    [SerializeField] private float companionSpeed = 5f;
    private Vector2 movementInput;

    // Movement
    private Transform player;

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void Awake()
    {
        cam = GameObject.Find("PlayerCam").transform;
        player = GameObject.Find("Player").transform;

        // Mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Movement();
    }

    void Rotate()
    {
        if (movementInput != Vector2.zero)
        {
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, cam.eulerAngles.y, Time.deltaTime * companionSpeed);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        }
    }

    void Movement()
    {
        transform.position = player.position;
    }
}
