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
    public bool selectedChoice = false;
    public int dialogueChoice;
    public string choiceInput;
    public bool quitText;

    [Header("Choice Animations")]
    public bool choiceBoxAnim;
    public bool closeChoiceAnim;

    [Header("Dialogue States")]
    public bool dialogueActive;
    public bool choiceActive;
    public bool readyToUpdate;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves onto the next text
        if (Keyboard.current.anyKey.wasPressedThisFrame && (dialogueActive || choiceActive) && !readyToUpdate)
        {
            textComplete = false;
            Next();
        }
        if (choiceBoxAnim)
        {
            choiceBoxes.localScale = Vector3.Lerp(choiceBoxes.localScale, Vector3.one, Time.deltaTime * 10);
            if (choiceBoxes.localScale == Vector3.one)
            {
                choiceBoxAnim = false;
                choiceText.gameObject.SetActive(true);
                NextChoice();
            }
        } else if (closeChoiceAnim)
        {
            choiceBoxes.localScale = Vector3.Lerp(choiceBoxes.localScale, Vector3.zero, Time.deltaTime * 10);
            if (choiceBoxes.localScale == Vector3.zero)
            {
                closeChoiceAnim = false;
                choiceText.gameObject.SetActive(false);
                readyToUpdate = true;
            }
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

    public void StartChoices()
    {
        choiceBoxes.gameObject.SetActive(true);
        choiceBoxes.localScale = Vector3.zero;
        choiceBoxAnim = true;
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
            // dialogueText.GetComponent<TextAnimatorPlayer>().SkipDisappearingText();
        } else
        {
            // If done typing and disappearing, move onto next text
            textComplete = false;
            textDisappeared = false;
            dialogueText.gameObject.SetActive(false);
            dialogueText.gameObject.SetActive(true);

            dialogueActive = false;
            choiceActive = true;
            
        }
    }

    public void NextChoice()
    {
        if (selectedChoice)
        {
            if (choiceInput == "Quit")
            {
                EndDialogue();
            } else
            {
                // RETURN CHOICE INPUT HERE
                GameObject.Find("FuckYou").GetComponent<FuckYou>().SendResponse(choiceInput);
                choiceActive = false;
                dialogueActive = true;
                closeChoiceAnim = true;
            }
        }
    }

    void EndDialogue()
    {

        Cursor.lockState = CursorLockMode.Confined;
        // Set variables
        choiceActive = false;

        // Diable text
        dialogueText.gameObject.SetActive(false);

        // Enable player movement
        player.lockMovement = false;
        
        // dialogue.gameObject.SetActive(false);
        //StartCoroutine(EndCycle());
    }
/**
    IEnumerator EndCycle()
    {
        yield return new WaitForSeconds(0.5f);
        dialogue.gameObject.SetActive(false);
    }
**/
}
