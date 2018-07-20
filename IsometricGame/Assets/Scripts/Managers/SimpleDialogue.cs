using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogue : MonoBehaviour
{
    /*An array of images used for NPCS who do not have proper dialogue*/
    public Image[] dialoguePopup;

    public enum State { Before, Deciding, Talking, After }
    public State state;

    void Start()
    {
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
                state = State.Deciding;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (state == State.Deciding)
        {
            if (Input.GetKey(KeyCode.E))
            {
                state = State.Talking;
            }
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
            case State.Deciding:
                dialoguePopup[0].enabled = true;
                break;
            case State.Talking:
                dialoguePopup[0].enabled = false;
                dialoguePopup[1].enabled = true;
                break;
        }
    }
}
