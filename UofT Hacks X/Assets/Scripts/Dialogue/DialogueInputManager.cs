using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueInputManager : MonoBehaviour
{
    private DialogueManager dialogueManager;

    public Text dialogue;
    public Text Choice1;
    public Text Choice2;
    public Text Choice3;
    public Text Choice4;

    // NEEDS TO REFER TO THE INPUT TEXT
    public bool updated;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = gameObject.GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.readyToUpdate)
        {
            if (updated){
                dialogue.gameObject.SetActive(true);
                Choice1.gameObject.SetActive(true);
                Choice2.gameObject.SetActive(true);
                Choice3.gameObject.SetActive(true);
                Choice4.gameObject.SetActive(true);
                // INPUT TEXT
                dialogue.gameObject.SetActive(false);
                Choice1.gameObject.SetActive(false);
                Choice2.gameObject.SetActive(false);
                Choice3.gameObject.SetActive(false);
                Choice4.gameObject.SetActive(false);
                updated = false;
                dialogueManager.readyToUpdate = false;
                dialogueManager.Next();
            }
            
        }
    }
}
