using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogue : MonoBehaviour
{
    NPCDialogue npcDialogue;
    /*An array of images used for NPCS who do not have proper dialogue*/
    public Image[] dialoguePopup;

    public enum State { Before, Greeting, Deciding, Talking }
    public State state;

    void Start()
    {
        npcDialogue = FindObjectOfType<NPCDialogue>();
        state = State.Before;
    }
    void Update()
    {
        UpdateStates();
    }

    /*This method checks if the players head collider enters the box which triggers the popup text to appear
     and enables it if true then checks if it exits and disables it if true*/

    void OnTriggerEnter(Collider other)
    {
        if (state == State.Before)
        {
            if (other.CompareTag("Player"))
            {
                state = State.Greeting;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            npcDialogue.TriggerDialogue();
            state = State.Talking;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = State.Before;
        }
    }
    void UpdateStates()
    {
        switch (state)
        {
            case State.Before:
                dialoguePopup[0].enabled = false;
                dialoguePopup[1].enabled = false;
                break;
            case State.Greeting:
                dialoguePopup[0].enabled = true;
                dialoguePopup[1].enabled = true;
                break;
            case State.Talking:
                dialoguePopup[0].enabled = false;
                dialoguePopup[1].enabled = false;
                break;
        }
    }
}
