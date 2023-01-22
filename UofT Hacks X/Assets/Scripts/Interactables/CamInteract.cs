using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamInteract : MonoBehaviour
{
    private CameraLog camLog;
    private bool onScreen;

    void Awake()
    {
        camLog = GameObject.Find("Player").GetComponent<CameraLog>();
        //camLog = FindObjectOfType<CameraLog>();

    }

    void Update()
    {
        CheckOnScreen();
    }

    void CheckOnScreen()
    {
        if (camLog.visibleObject == gameObject)
        {
            if (!onScreen)
            {
                OnStartHover();
                onScreen = true;
                return;
            }
            else
            {
                return;
            }
        }
        else
        {
            if (onScreen)
            {
                OnEndHover();
                onScreen = false;
                return;
            }
        }
    }

    public void OnStartHover()
    {
        print("Start hover on " + gameObject.name);        
    }

    public void OnEndHover()
    {
        print("End hover on " + gameObject.name);
    }

    public void OnInteract()
    {
        print("Interact with " + gameObject.name);
    }
}
