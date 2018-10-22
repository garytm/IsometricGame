using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogue : MonoBehaviour
{
    Player player;
    NPCDialogue npcDialogue;
    DialogueManager dialogueManager;
    public bool inConversation;
    public Image ePopup;

    float delay = 0.5f;
    float delayTimer;
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
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
        if (other.CompareTag("Player"))
        {
            npcDialogue = GetComponent<NPCDialogue>();
            if (inConversation == false && Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp("joystick button 3"))
            {
                player.target = GetComponent<Transform>();
                inConversation = true;
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
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.target = null;
            dialogueManager.EndDialogue();
            inConversation = false;
            ePopup.enabled = false;
        }
    }
}
