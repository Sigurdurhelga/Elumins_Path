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

	private void Start(){
		bookAnimator = GetComponentInChildren<Animator>();
		bookSound = GetComponent<AudioSource>();
		dialogueManager = GameObject.FindGameObjectWithTag("dialogueManager").GetComponent<DialogueManager>();
	}

	private void Update(){
		if(playerIn && Input.GetKeyDown(KeyCode.Space) && !doneTriggering){
			Debug.Log("starting dialogue");
			doneTriggering = true;
			dialogueManager.StartDialogue(dialogue);
		}
	}

	public void TriggerDialogue(){
		if(!doneTriggering)
			dialogueManager.StartDialogue(dialogue);

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
			bookAnimator.SetBool("isOpened",false);
			dialogueManager.endDialogue();
		}
	}
	
}
