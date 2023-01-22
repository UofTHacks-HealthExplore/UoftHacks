using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueInputManager : MonoBehaviour
{
    private DialogueManager dialogueManager;
    public FuckYou f;

    public TMP_Text dialogue;
    public TMP_Text Choice1;
    public TMP_Text Choice2;
    public TMP_Text Choice3;
    public TMP_Text Choice4;

    // NEEDS TO REFER TO THE INPUT TEXT
    public bool updated;

    // Start is called before the first frame update
    void Start()
    {
        f = GameObject.Find("FuckYou").GetComponent<FuckYou>();
        dialogueManager = gameObject.GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.readyToUpdate)
        {
            if (f.updated){
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

                f.updated = false;
            }
            
        }
    }
}
