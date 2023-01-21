using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Rotation
    private Transform cam;

    void Awake()
    {
        cam = GameObject.Find("PlayerCam").transform;

        // Mouse
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    void Look()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.rotation.eulerAngles.y, transform.eulerAngles.z);
    }
}
