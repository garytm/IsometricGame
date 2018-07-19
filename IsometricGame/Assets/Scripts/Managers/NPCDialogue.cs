using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    bool inConversation;

    public Image[] dialoguePopup;

    void Start()
    {
        inConversation = false;
    }

    /*This method checks if the players head collider enters the box which triggers the popup text to appear
     and enables it if true then checks if it exits and disables it if true*/
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && inConversation == false)
        {
            dialoguePopup[0].enabled = true;   
        }
        if (Input.GetKey(KeyCode.E))
        {
            dialoguePopup[0].enabled = false;
            inConversation = true;
            dialoguePopup[1].enabled = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i <= dialoguePopup.Length; i++)
            {
                dialoguePopup[i].enabled = false;
            }
        }
    }
}
