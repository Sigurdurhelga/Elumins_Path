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

		/*if(inConverstation && Input.GetKeyDown(KeyCode.Space)){
			displayNextSentence();
		}*/

	}

	public void StartDialogue(Dialogue dialogue){
		
		sentences.Clear();

		animator.SetBool("isOpen", true);

		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
		}

		displayNextSentence();
	}

	IEnumerator typeText(string text){
		foreach(char c in text){
			dialogueText.text += c;
			yield return null;
		}
	}

	public void displayNextSentence(){
		if(sentences.Count == 0){
			endDialogue();
		}
		else{
			string sentence = sentences.Dequeue();
			dialogueText.text = "";
			StopAllCoroutines();
			StartCoroutine(typeText(sentence));
			if(!inConverstation){
				inConverstation = true;
			}
		}
	}

	public void endDialogue(){
		sentences.Clear();
		animator.SetBool("isOpen", false);
	}

}
