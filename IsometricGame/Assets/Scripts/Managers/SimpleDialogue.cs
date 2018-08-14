using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogue : MonoBehaviour
{
    NPCDialogue npcDialogue;
    DialogueManager dialogueManager;
    CameraZoom cameraZoom;
    public bool inConversation;
    public Image ePopup;

    void Start()
    {
        npcDialogue = GetComponent<NPCDialogue>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        cameraZoom = FindObjectOfType<CameraZoom>();
        inConversation = false;
        ePopup.enabled = false;
    }
    /*This method checks if the players head collider enters the box which triggers the popup text to appear
     and enables it if true then checks if it exits and disables it if true*/

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ePopup.enabled = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (inConversation == false && Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp("joystick button 3"))
        {
            inConversation = true;
            cameraZoom.isZoomed = true;
            npcDialogue.TriggerDialogue();
            ePopup.enabled = false;
            if (Input.GetKey(KeyCode.E) || Input.GetKey("joystick button 3"))
            {
                dialogueManager.ShowNextSentence();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.EndDialogue();
            cameraZoom.isZoomed = false;
            inConversation = false;
            ePopup.enabled = false;
        }
    }
}
