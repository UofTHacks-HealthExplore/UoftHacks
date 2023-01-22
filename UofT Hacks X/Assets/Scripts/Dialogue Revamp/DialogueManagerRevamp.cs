using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Febucci.UI;

public class DialogueManagerRevamp : MonoBehaviour
{
    // Objects
    public GameObject dialogueObject;
    public GameObject choiceObject;
    public GameObject FuckYou;

    // Text
    public GameObject dialogueText;
    public GameObject choiceText;

    // TextAnim Triggers
    private bool textCompleted;
    private bool textDisappeared;

    // States
    private bool dialogue;
    private bool choice = false;
    private bool exit = false;

    public void DialogueCompleted(){
        textCompleted = true;
    }
    public void DialogueDisappeared(){
        textDisappeared = true;
    }

    IEnumerator WaitDialogueAnims()
    {
        yield return new WaitUntil(() => textDisappeared == true);
        Next();
    }
    IEnumerator WaitChoiceAnims()
    {
        yield return new WaitUntil(() => textDisappeared == true);
        choiceObject.GetComponent<Animator>().Play("CloseChoice");
        Next();
    }
    IEnumerator WaitFuckYou()
    {
        yield return new WaitUntil(() => FuckYou.GetComponent<FuckYou>().updated == true);
        choice = false;
        dialogue = true;
        textDisappeared = true;
        dialogueObject.SetActive(true);
        //choiceObject.SetActive(false);
        FuckYou.GetComponent<FuckYou>().updated = false;
        StartDialogue();

    }

    public void StartInteraction(){

        dialogueObject.SetActive(true);
        StartDialogue();
        choiceObject.SetActive(false);
    }


    public void StartDialogue()
    {
        dialogueObject.GetComponent<Animator>().Play("OpenDialogue");
        dialogue = true;
    }
    public void StartChoice(){
        choiceObject.GetComponent<Animator>().Play("OpenChoice");
        choice = true;
    }

    public void Choose(string choice)
    {
        if (choice != "Quit")
        {
            FuckYou.GetComponent<FuckYou>().SendResponse(choice);
            choiceText.GetComponent<TextAnimatorPlayer>().StartDisappearingText();
            StartCoroutine(WaitChoiceAnims());
        } else
        {
            choiceText.GetComponent<TextAnimatorPlayer>().StartDisappearingText();
            StartCoroutine(WaitChoiceAnims());
            exit = true;
        }
    }


    public void Update(){
        if (dialogue && textCompleted && Keyboard.current.anyKey.wasPressedThisFrame){            
            dialogueObject.GetComponent<Animator>().Play("CloseDialogue");
            textDisappeared = false;
            textCompleted = false;
            dialogueText.GetComponent<TextAnimatorPlayer>().StartDisappearingText();
            StartCoroutine(WaitDialogueAnims());
        }

    }

    public void Next(){
        if (exit)
        {
            dialogue = false;
            choice = false;
            exit = false;
            dialogueObject.SetActive(false);
            //choiceObject.SetActive(false);
            // Enable player movement
        }
        if (dialogue)
        {
            dialogue = false;
            choice = true;
            textDisappeared = true;
            choiceObject.SetActive(true);
            dialogueObject.SetActive(false);
            StartChoice();
        }
        else if (choice)
        {
            StartCoroutine(WaitFuckYou());
        }
    }



    
}
