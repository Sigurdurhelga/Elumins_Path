using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredCrystalScript : MonoBehaviour {
	private GameObject player_ref;
	private GameObject shard_ref;
	private bool player_in_shard = false;
	private bool player_in_shell = false;
	private bool connected = false;

	IEnumerator followPlayer(GameObject shard){
		yield return new WaitForSeconds(0.2f);

		while(connected){
			shard.transform.position = Vector3.Lerp(shard.transform.position, player_ref.transform.position, 0.05f);
			yield return null;
		}
	}

	IEnumerator mergeWithShell(GameObject shard){

		float pos = 0;

		while(pos <= 1){
			shard.transform.position = Vector3.Lerp(shard.transform.position, transform.position, pos);
			pos += 0.01f;
			yield return null;
		}

	}

	private void Update(){
		if(connected){
			if(Input.GetKeyDown(KeyCode.Space)){
				connected = false;
				if(player_in_shell){
					StartCoroutine(mergeWithShell(shard_ref));
				}
			}
		}
		else if(player_in_shard && !connected && shard_ref){
			if(Input.GetKeyDown(KeyCode.Space)){
				connected = true;
				StartCoroutine(followPlayer(shard_ref));
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collider){
		if(collider.transform.tag == "Player"){
			player_in_shell = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collider){
		if(collider.transform.tag == "Player"){
			player_in_shell = false;
		}
	}

	public void OnTriggerEnter2DChild(Collider2D collider, GameObject shard){
		if(!player_ref){
			player_ref = collider.gameObject;
		}
		if(!connected)
			shard_ref = shard;
		player_in_shard = true;
	}

	public void OnTriggerExit2DChild(Collider2D collider){
		//shard_ref = null;
		player_in_shard = false;
	}

}
