using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	private AudioSource bookSound;
	private Animator bookAnimator;
	private DialogueManager dialogueManager;
	private bool playerIn = false;
	private bool doneTriggering = false;
	private bool inConversation = false;

	private void Start(){
		bookAnimator = GetComponentInChildren<Animator>();
		bookSound = GetComponent<AudioSource>();
		dialogueManager = GameObject.FindGameObjectWithTag("dialogueManager").GetComponent<DialogueManager>();
	}

	private void Update(){
		if(playerIn && Input.GetKeyDown(KeyCode.Space) && !doneTriggering && !inConversation){
			doneTriggering = true;
			dialogueManager.StartDialogue(dialogue);
			inConversation = true;
		} else if(playerIn && Input.GetKeyDown(KeyCode.Space) && inConversation){
			dialogueManager.displayNextSentence();
		}
	}

	private void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Player"){
			bookSound.Play();
			playerIn = true;
			bookAnimator.SetBool("isOpened",true);
		}
	}
	private void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "Player"){
			bookSound.Play();
			playerIn = false;
			doneTriggering = false;
			inConversation = false;
			bookAnimator.SetBool("isOpened",false);
			dialogueManager.endDialogue();
		}
	}
	
}
