using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamInteract : MonoBehaviour
{
    private CameraLog camLog;
    private bool onScreen;


    // Events
    public event EventHandler OnStartHoverEvent;
    public event EventHandler OnEndHoverEvent;
    public event EventHandler onInteractEvent;

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
        OnStartHoverEvent?.invoke(this, EventArgs.Empty);     
    }

    public void OnEndHover()
    {
        OnEndHoverEvent?.invoke(this, EventArgs.Empty);
    }

    public void OnInteract()
    {
        onInteractEvent?.invoke(this, EventArgs.Empty);
    }
}
