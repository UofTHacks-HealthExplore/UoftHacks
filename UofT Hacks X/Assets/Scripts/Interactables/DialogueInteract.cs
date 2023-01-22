using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueInteract : MonoBehaviour
{

    public Animator indicator;
    // Start is called before the first frame update
    void Start()
    {
        CamInteract interactControl = GetComponent<CamInteract>();
        interactControl.OnStartHoverEvent += OnStartHover;
        interactControl.OnEndHoverEvent += OnEndHover;
        interactControl.OnInteractEvent += OnInteract;
    }

    void OnStartHover(object sender, EventArgs e)
    {
        if (!indicator.gameObject.activeInHierarchy) indicator.gameObject.SetActive(true);
        indicator.Play("Text Indicator Open");
    }

    void OnEndHover(object sender, EventArgs e)
    {
        if (indicator.gameObject.activeInHierarchy) indicator.gameObject.SetActive(false);
        indicator.Play("Text Indicator Close");
    }

    void OnInteract(object sender, EventArgs e)
    {
        DialogueManager.dialogueManager.StartDialogue();
        if (indicator.gameObject.activeInHierarchy) indicator.gameObject.SetActive(false);
        indicator.Play("Text Indicator Close"); 
    }
}
