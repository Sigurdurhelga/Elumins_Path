using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {

	public Animator finalAnimator;
	public ParticleSystem finalParticles;

	IEnumerator startParticles(){
		yield return new WaitForSeconds(2f);
		finalParticles.Play();
		yield return new WaitForSeconds(30f);
		SceneManager.LoadScene("StartMenu");
	}

	// Use this for initialization
	void Start () {
		finalAnimator.SetBool("FinishedGame", true);
		finalParticles.Pause();
		StartCoroutine(startParticles());
	}
	
}
