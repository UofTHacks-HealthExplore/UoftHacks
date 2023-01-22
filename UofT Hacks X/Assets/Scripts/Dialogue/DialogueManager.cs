using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Febucci.UI;
using Cinemachine;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager dialogueManager;
    
    public PlayerController player;

    [Header("Dialogue Settings")]
    public bool textComplete;
    public bool textDisappeared;
    public int dialogueIndex;
    // Dialogue Box Object
    public Transform dialogueBox;
    // Output Text Object
    public GameObject dialogueText;
    // Choices Object
    public Transform choiceBoxes;
    // Choices Object
    public Transform choiceText;
    public Dialogue NPCTexts;

    public Transform dialoguePosition;

    [Header("Dialogue Choice Settings")]
    public bool selecteChoice = false;
    public int dialogueChoice;
    public GameObject choiceInput;

    [Header("Dialogue States")]
    public bool dialogueActive;
    public bool choiceActive;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves onto the next text
        if (Keyboard.current.anyKey.wasPressedThisFrame && (dialogueActive || choiceActive) && NPCTexts.updated)
        {
            textComplete = false;
            Next();
        }
    }

    public void StartDialogue()
    {
        if (dialogueActive || choiceActive) return;
        StartCoroutine(StartSequence());

        
        // Disable player movement
        player.lockMovement = true;

    }

    IEnumerator StartSequence()
    {
        // Open Dialogue box
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.GetComponent<Animator>().Play("Dialogue Box Open");
        yield return new WaitForSeconds(0.5f);


        // Enable Dialogue Text
        dialogueText.gameObject.SetActive(true);

        // Start dialogue   
        textComplete = false;
        dialogueActive = true;
    }

    // Text Animator Events
    public void CompletedText()
    {
        textComplete = true;
    }

    public void DisappearedText()
    {
        textDisappeared = true;
        Next();
    }

    public void Next()
    {
        if (choiceActive)
        {
            NextChoice();
        } else
        {
            // Play dialogue box open anim
            StartCoroutine(StartSequence());
        }   
    }

/**
    IEnumerator StartDialogue()
    {
        // Dialogue Box Open Anim (Text needs to be updated)
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.GetComponent<Animator>().Play("Dialogue Box Open");
        dialogueText.update
        yield return new WaitForSeconds(0.5f);
        dialogueText.gameObject.SetActive(true);

    }
**/
    public void NextDialogue()
    {
        if (!textComplete)
        {
            // If not done typing, skip typing
            textComplete = true;
            dialogueText.GetComponent<TextAnimatorPlayer>().SkipTypewriter();
        } else if (!textDisappeared)
        {
            // If not done disappearing, skip disappearing
            textDisappeared = true;
            dialogueText.GetComponent<TextAnimatorPlayer>().SkipDisappearingText();
        } else
        {
            // If done typing and disappearing, move onto next text
            textComplete = false;
            textDisappeared = false;
            dialogueText.gameObject.SetActive(false);
            dialogueText.gameObject.SetActive(true);

            choiceActive = true;
            
        }
    }

    public void NextChoice()
    {

    }


    public void IsChoice()
    {
        if (!dialogueText.showing && !dialogueText.opened)
        {
            StartCoroutine(dialogueText.Show());
            dialogueChoice = 0;
        }
        else if (dialogueText.showed && dialogueText.showing)
        {
            // print("yes");
            // TODO use better key system
            if (Keyboard.current[Key.W].wasPressedThisFrame || Keyboard.current[Key.S].wasPressedThisFrame)
            {

                print("w or s");
                if (Keyboard.current[Key.W].wasPressedThisFrame)
                {
                    dialogueChoice++;
                }
                else
                {
                    dialogueChoice--;
                }
                dialogueChoice = (int)Mathf.Repeat(dialogueChoice, dialogueText.nextOptions.Length);
            }
            else if (Keyboard.current[Key.E].wasPressedThisFrame)
            {
                // print("e");
                // Record text option
                dialogueText.StartCoroutine(dialogueText.Hide());
            }
        }
        else if (!dialogueText.showed && !dialogueText.showing)
        {
            dialogueText.opened = false;
            textComplete = false;
            textDisappeared = false;

            if (dialogueText.nextOptions.Length == 0)
            {
                EndDialogue();
            }
            else
            {
                dialogueText = dialogueText.nextOptions[dialogueChoice];
                dialogueText.gameObject.SetActive(true);
            }
        }
    }
    void EndDialogue()
    {

        Cursor.lockState = CursorLockMode.Confined;
        // Set variables
        dialogueActive = false;

        // Diable text
        dialogueText.gameObject.SetActive(false);


        dialogue.GetChild(1).GetComponent<Animator>().Play("Dialogue Box Close");

        // Enable player movement
        player.lockMovement = false;
        
        // dialogue.gameObject.SetActive(false);
        StartCoroutine(EndCycle());
    }

    IEnumerator EndCycle()
    {
        yield return new WaitForSeconds(0.5f);
        dialogue.gameObject.SetActive(false);
    }
}
