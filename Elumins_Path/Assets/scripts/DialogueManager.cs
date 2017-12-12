using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text dialogueText;
	public Animator animator;
	private Queue<string> sentences = new Queue<string>();
	private bool inConverstation = false;

	private void Update(){

		if(inConverstation && Input.GetKeyDown(KeyCode.Space)){
			displayeNextSentence();
		}

	}

	public void StartDialogue(Dialogue dialogue){

		sentences.Clear();

		animator.SetBool("isOpen", true);

		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
		}

		inConverstation = true;
		displayeNextSentence();
	}

	IEnumerator typeText(string text){
		foreach(char c in text){
			dialogueText.text += c;
			// wait for one frame between each character
			yield return null;
		}
	}

	public void displayeNextSentence(){
		if(sentences.Count == 0){
			endDialogue();
		}
		else{
			string sentence = sentences.Dequeue();
			dialogueText.text = "";
			StopAllCoroutines();
			StartCoroutine(typeText(sentence));
		}
	}

	public void endDialogue(){
		animator.SetBool("isOpen", false);
	}

}
