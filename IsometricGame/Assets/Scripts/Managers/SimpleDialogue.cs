﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogue : MonoBehaviour
{
    Player player;
    NPCDialogue npcDialogue;
    DialogueManager dialogueManager;
    CameraZoom cameraZoom;
    public bool inConversation;
    public Image ePopup;

    float delay = 0.5f;
    float delayTimer;
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        cameraZoom = FindObjectOfType<CameraZoom>();
        player = FindObjectOfType<Player>();
        inConversation = false;
        ePopup.enabled = false;
        delayTimer = Time.time + delay;
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
        npcDialogue = GetComponent<NPCDialogue>();
        if (inConversation == false && Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp("joystick button 3"))
        {
            
            inConversation = true;
            cameraZoom.isZoomed = true;
            npcDialogue.TriggerDialogue();
            ePopup.enabled = false; 
        }
        if (Time.time > delayTimer)
        {
            if (inConversation == true && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick button 3"))
            {
                delayTimer = Time.time + delay;
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
