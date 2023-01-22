using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButtons : MonoBehaviour, Interactables
{
    public float MaxRange { get { return maxRange; } }

    [SerializeField] private float maxRange = 2f;

    // Scale Variables
    private Vector3 initialBoxScale;
    public DialogueManagerRevamp dialogueManager;

    public void Awake()
    {
        initialBoxScale = transform.localScale;
    }


    public void OnStartHover()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, initialBoxScale * 1.1f, Time.deltaTime * 10f);
    }

    public void OnInteract()
    {
        dialogueManager.Choose(this.name);
    }

    public void OnEndHover()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, initialBoxScale, Time.deltaTime * 10f);
    }
}
