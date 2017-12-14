using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameHandler : MonoBehaviour {

	public Animator animator;

	private void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Player"){
			animator.SetBool("FinishedGame",true);
		}
	}
}
