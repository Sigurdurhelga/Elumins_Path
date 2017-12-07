using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelmanager : MonoBehaviour {
	public int a;

	private List<GameObject> gems = new List<GameObject>();
	private ParticleSystem portal;
	private Light roomLight;

	int requiredGems;

	// Use this for initialization
	void Start () {
		portal = GetComponentInChildren<ParticleSystem>();
		roomLight = GetComponentInChildren<Light>();
		
		var emission = portal.emission;
		emission.enabled = false;

		foreach(GameObject gem in GameObject.FindGameObjectsWithTag("levelGem")){
			gems.Add(gem);
		}
		requiredGems = gems.Count;
	}

	public void gemActivated(){
		requiredGems -= 1;
		if(requiredGems <= 0){
			roomLight.enabled = true;
			var emission = portal.emission;
			emission.enabled = true;
		}
	}
	
}
