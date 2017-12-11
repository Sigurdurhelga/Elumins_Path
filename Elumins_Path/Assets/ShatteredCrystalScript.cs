using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredCrystalScript : MonoBehaviour {
	public GameObject ShatterWall;
	private List<GameObject> wallShardGBs = new List<GameObject>();
	private GameObject player_ref;
	private GameObject shard_ref;
	private int connectedShards = 0;
	private bool player_in_shard = false;
	private bool player_in_shell = false;
	private bool connected = false;
	private List<string> doneNames = new List<string>();

	private void Start(){
		for(int i = 0; i < ShatterWall.transform.childCount; i++){
			wallShardGBs.Add(ShatterWall.transform.GetChild(i).gameObject);
		}
	}

	IEnumerator followPlayer(GameObject shard){
		yield return new WaitForSeconds(0.2f);

		while(connected){
			shard.transform.position = Vector3.Lerp(shard.transform.position, player_ref.transform.position, 0.05f);
			yield return null;
		}
	}

	IEnumerator mergeWithShell(GameObject shard){

		doneNames.Add(shard.name);

		float pos = 0;

		while(pos <= 1){
			shard.transform.position = Vector3.Lerp(shard.transform.position, transform.position, pos);
			pos += 0.01f;
			yield return null;
		}
		connectedShards += 1;

		if(connectedShards == 4){
			Rigidbody2D temprb;
			Vector2 leftForce = new Vector2(100, 0);
			Vector2 rightForce = new Vector2(-100, 0);
			foreach(GameObject wallShard in wallShardGBs){
				temprb = wallShard.GetComponent<Rigidbody2D>();
				temprb.bodyType = RigidbodyType2D.Dynamic;
			//	temprb.gravityScale = 1;
			}
			foreach(GameObject wallShard in wallShardGBs){
				temprb = wallShard.GetComponent<Rigidbody2D>();
				temprb.AddForce(new Vector2(Random.Range(-1000,1001), Random.Range(-500, 501)));
			}
			yield return new WaitForSeconds(4f);

			foreach(GameObject wallShard in wallShardGBs){
				Destroy(wallShard);
			}
			Destroy(ShatterWall);

		}

	}

	private void Update(){
		if(connected){
			if(Input.GetKeyDown(KeyCode.Space)){
				connected = false;
				if(player_in_shell && !doneNames.Contains(shard_ref.name)){
					StartCoroutine(mergeWithShell(shard_ref));
				}
			}
		}
		else if(player_in_shard && !connected && shard_ref){
			if(Input.GetKeyDown(KeyCode.Space) && !doneNames.Contains(shard_ref.name)){
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
