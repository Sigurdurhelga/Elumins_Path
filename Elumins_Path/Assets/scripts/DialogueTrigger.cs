using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	private DialogueManager dialogueManager;
	private bool doneTriggering = false;

	private void Start(){
		dialogueManager = GameObject.FindGameObjectWithTag("dialogueManager").GetComponent<DialogueManager>();
	}

	public void TriggerDialogue(){
		if(!doneTriggering)
			dialogueManager.StartDialogue(dialogue);

	}

	private void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Player"){
			TriggerDialogue();
			doneTriggering = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "Player"){
			dialogueManager.endDialogue();
		}
	}
	
}
