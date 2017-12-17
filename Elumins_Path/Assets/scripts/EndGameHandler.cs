using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameHandler : MonoBehaviour {

	public Animator animator;
	public BoxCollider2D endWall;
	private bool endwallGone = false;

	private void Update(){
		if(!endwallGone){
			if(GameController.instance.GetCurrentLevel() >= 12){
				endWall.enabled = false;
				endwallGone = true;
			}
		}
	}

	IEnumerator changeToEndgame(){
		yield return new WaitForSeconds(7f);
		GameController.instance.SceneChange(15);
	}

	private void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Player"){
			animator.SetBool("FinishedGame",true);
			StartCoroutine(changeToEndgame());
		}
	}
}
