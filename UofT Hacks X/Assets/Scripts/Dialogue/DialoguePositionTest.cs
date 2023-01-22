using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePositionTest : MonoBehaviour
{
    public Transform dialogueObject;

    void Start()
    {
        gameObject.transform.position = dialogueObject.position;
    }
}
