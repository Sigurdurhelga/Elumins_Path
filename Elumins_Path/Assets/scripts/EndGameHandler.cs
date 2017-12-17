﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameHandler : MonoBehaviour {

	public Animator animator;

	IEnumerator changeToEndgame(){
		yield return new WaitForSeconds(7f);
		GameController.instance.SceneChange(14);
	}

	private void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Player"){
			animator.SetBool("FinishedGame",true);
			StartCoroutine(changeToEndgame());
		}
	}
}
