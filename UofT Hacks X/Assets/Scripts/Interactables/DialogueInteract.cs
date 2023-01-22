using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        CamInteract interactControl = GetComponent<CamInteract>();
        interactControl.OnStartHoverEvent += OnStartHover;
        interactControl.OnEndHoverEvent += OnEndHover;
        interactControl.onInteractEvent += OnInteract;
    }

    void OnStartHover()
    {
        if (!indicator.gameObject.activeInHierarchy) indicator.gameObject.SetActive(true);
        indicator.Play("Text Indicator Open");
    }

    void OnEndHover()
    {
        indicator.Play("Text Indicator Close");
    }

    void OnInteract()
    {
        DialogueManager.dialogueManager.StartDialogue();
        if (indicator.gameObject.activeInHierarchy) indicator.Play("Text Indicator Close");
    }
}
