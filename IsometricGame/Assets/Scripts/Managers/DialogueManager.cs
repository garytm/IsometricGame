using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Player player;
    CameraZoom cameraZoom;
    NPCManager npcManager;
    public Animator animator;
    public Text nameText;
    public Text dialogueText;
    /*A queue is similar to a list, but a little more restrictive*/
    Queue<string> sentences;

	void Start ()
    {
        player = FindObjectOfType<Player>();
        cameraZoom = FindObjectOfType<CameraZoom>();
        npcManager = FindObjectOfType<NPCManager>();
        /*Defining sentences as a new queue of type string*/
        sentences = new Queue<string>();
	}
    /*This method is used to start dialogue and takes a variable of type Dialogue allowing the two scripts to communicate*/
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        player.canMove = false;
        cameraZoom.isZoomed = true;
        npcManager.LookAtPlayer();
        nameText.text = dialogue.name;
        /*Clears any left over sentences from a previous conversation*/
        sentences.Clear();

        /*Looping through the sentences array then queueing up the current sentence in the queue*/
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        ShowNextSentence();
    }
    public void ShowNextSentence()
    {
        /*First check if there are any sentence left in the queue*/
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(WriteSentence(sentence));
    }

    IEnumerator WriteSentence (string sentence)
    {
        dialogueText.text = "";
        /*Looping through the sentences and returning each letter using .ToCharArray
         .ToCharArray loops through a string and converts it into an array of chars*/
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        player.canMove = true;
        player.target = null;
        cameraZoom.isZoomed = false;
        Debug.Log("End of conversation!");
    }
}
