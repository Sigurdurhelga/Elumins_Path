using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterCrystalChildren : MonoBehaviour {

	private ShatteredCrystalScript parent;

	private void Start(){
		parent = transform.parent.GetComponent<ShatteredCrystalScript>();
	}

	private void OnTriggerEnter2D(Collider2D collider){
		if(collider.transform.tag == "Player"){
			parent.OnTriggerEnter2DChild(collider, gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collider){
		if(collider.transform.tag == "Player"){
			parent.OnTriggerExit2DChild(collider);
		}
	}
}
