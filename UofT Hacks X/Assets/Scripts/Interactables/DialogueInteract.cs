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
        indicator.Play("Open");
    }

    void OnEndHover(object sender, EventArgs e)
    {
        indicator.Play("Close");
        StartCoroutine(CloseIndicator());
    }

    void OnInteract(object sender, EventArgs e)
    {
        GameObject.Find("DIALOGUE MANAGER").GetComponent<DialogueManagerRevamp>().StartInteraction();
        indicator.Play("Close"); 
        StartCoroutine(CloseIndicator());
    }

    IEnumerator CloseIndicator()
    {
        yield return new WaitForSeconds(0.6f);
        if (indicator.gameObject.activeInHierarchy) indicator.gameObject.SetActive(false);
    }
}
